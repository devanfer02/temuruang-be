using System.ComponentModel.DataAnnotations;

namespace temuruang_be.Dtos.AuthDTO;

public class LoginDTO
{
    [Required]
    public required string Email { get; set; }

    [Required]
    public required string Password { get; set; }
}