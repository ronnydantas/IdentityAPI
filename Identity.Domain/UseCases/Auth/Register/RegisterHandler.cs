using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Identity.Domain.Services;

namespace IdentityAPI.UseCases.Auth;

public class RegisterHandler : IRequestHandler<RegisterCommand, Unit>
{
    private readonly IUserService _userService;

    public RegisterHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<Unit> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        await _userService.SignUp(request.Model);
        return Unit.Value;
    }
}
