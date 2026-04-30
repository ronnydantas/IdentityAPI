using Identity.Domain.DTOs;
using Identity.Domain.Interfaces.Event;
using Identity.Domain.Interfaces.User;
using MediatR;

namespace IdentityAPI.UseCases.Auth;

public class RegisterHandler : IRequestHandler<RegisterCommand, Unit>
{
    private readonly IUserService _userService;
    private readonly IEventPublishService _eventPublishService;

    public RegisterHandler(IUserService userService, IEventPublishService eventPublishService)
    {
        _userService = userService;
        _eventPublishService = eventPublishService;
    }

    public async Task<Unit> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        await _userService.SignUp(request.Model);

        var eventDTO = new EventDTO
        {
            UserName = request.Model.Username!,
            FullName = request.Model.FullName!,
            Email = request.Model.Email!,
        };
        await _eventPublishService.PublishAsync(eventDTO);

        return Unit.Value;
    }
}
