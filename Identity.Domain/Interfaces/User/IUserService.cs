using Identity.Domain.DTOs;
using Identity.Domain.Entities;

namespace Identity.Domain.Interfaces.User;

public interface IUserService
{
    Task<List<ApplicationUser>> ListUsers();
    Task<ApplicationUser> GetUserById(string userId);
    Task<int> UpdateUser(ApplicationUser user);
    Task<bool> DeleteUser(string userId);
    Task<ApplicationUser> GetCurrentUser();
    Task<ApplicationUser> SignUp(SignUpDTO signUpDTO);
    Task<SsoDTO> SignIn(SignInDTO signInDTO);
}