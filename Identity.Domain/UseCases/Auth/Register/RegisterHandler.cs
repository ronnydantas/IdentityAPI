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

        await _eventPublishService.PublishAsync(request.Model.Username!, request.Model.Email!);

        return Unit.Value;
    }
}
