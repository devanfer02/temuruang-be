using System.ComponentModel.DataAnnotations;
using temuruang_be.Models;

namespace temuruang_be.Dtos.UserDTO;

public class CreateUserDTO 
{

    [Required]
    public required string Fullname { get; set; }

    [Required]
    [EmailAddress]
    public required string Email { get; set; }

    [Required]
    [MinLength(6)]
    public required string Password { get; set; }

    public static User ToUser(CreateUserDTO dto) 
    {
        return new User 
        {
            Fullname = dto.Fullname,
            Password = dto.Password,
            Email = dto.Email
        };
    }
}