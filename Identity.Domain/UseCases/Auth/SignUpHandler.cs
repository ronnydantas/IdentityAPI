using Identity.Domain.Services;
using MediatR;

namespace Identity.Domain.UseCases.Auth;

public class SignUpHandler : IRequestHandler<SignUpCommand, bool>
{
    private readonly IUserService _userService;

    public SignUpHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<bool> Handle(SignUpCommand request, CancellationToken cancellationToken)
    {
        return await _userService.SignUp(request.SignUpDTO);
    }
}
