using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Identity.Domain.DTOs;
using Identity.Domain.Interfaces.User;

namespace IdentityAPI.UseCases.Auth;

public class LoginHandler : IRequestHandler<LoginQuery, SsoDTO>
{
    private readonly IUserService _userService;

    public LoginHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<SsoDTO> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        return await _userService.SignIn(request.Model);
    }
}
