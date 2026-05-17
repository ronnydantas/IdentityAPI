using Identity.Domain.DTOs;
using Identity.Domain.Interfaces.Event;
using Identity.Domain.Interfaces.User;
using MediatR;

namespace IdentityAPI.UseCases.Auth;

public class RegisterHandler : IRequestHandler<RegisterCommand, UserCreatedEvent>
{
    private readonly IUserService _userService;
    private readonly IKafkaProducer _kafkaProducer;
    public RegisterHandler(IUserService userService, IKafkaProducer kafkaProducer)
    {
        _userService = userService;
        _kafkaProducer = kafkaProducer;
    }

    public async Task<UserCreatedEvent> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var user = await _userService.SignUp(request.Model);

        var eventDTO = new UserCreatedEvent
        {
            Id = user.Id,
            UserName = user.UserName!,
            FullName = user.FullName!,
            Email = user.Email!,
        };

        await _kafkaProducer.PublishUserCreated(eventDTO);

        return eventDTO;
    }
}
