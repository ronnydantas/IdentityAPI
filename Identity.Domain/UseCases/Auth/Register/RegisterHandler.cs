using Identity.Domain.DTOs;
using Identity.Domain.Interfaces.Event;
using Identity.Domain.Interfaces.User;
using MediatR;

namespace IdentityAPI.UseCases.Auth;

public class RegisterHandler : IRequestHandler<RegisterCommand, EventDTO>
{
    private readonly IUserService _userService;
    private readonly IEventPublishService _eventPublishService;

    public RegisterHandler(IUserService userService, IEventPublishService eventPublishService)
    {
        _userService = userService;
        _eventPublishService = eventPublishService;
    }

    public async Task<EventDTO> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var user = await _userService.SignUp(request.Model);

        var eventDTO = new EventDTO
        {
            Id = user.Id,
            UserName = user.UserName!,
            FullName = user.FullName!,
            Email = user.Email!,
        };
        await _eventPublishService.PublishAsync(eventDTO);

        return eventDTO;
    }
}
