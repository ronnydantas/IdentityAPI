using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Identity.Domain.Services;
using Identity.Domain.DTOs;

namespace Identity.Domain.UseCases.Auth.GetUser;

public class MeHandler : IRequestHandler<MeQuery, UserInfoDTO>
{
    private readonly IUserService _userService;

    public MeHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<UserInfoDTO> Handle(MeQuery request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetCurrentUser();

        return new UserInfoDTO
        {
            Id = user.Id,
            UserName = user.UserName ?? string.Empty,
            Email = user.Email ?? string.Empty
        };
    }
}
