using System.ComponentModel.DataAnnotations;

namespace Identity.Domain.DTOs;

public class SignInDTO
{
    [Required(ErrorMessage = "User Name is required")]
    public string Email { get; set; } = string.Empty;   

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; } = string.Empty;
}