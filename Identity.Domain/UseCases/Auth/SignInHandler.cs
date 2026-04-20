using Identity.Domain.DTOs;
using Identity.Domain.Services;
using MediatR;

namespace Identity.Domain.UseCases.Auth;

public class SignInHandler : IRequestHandler<SignInQuery, SsoDTO>
{
    private readonly IUserService _userService;

    public SignInHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<SsoDTO> Handle(SignInQuery request, CancellationToken cancellationToken)
    {
        return await _userService.SignIn(request.SignInDTO);
    }
}
