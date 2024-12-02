using System.ComponentModel.DataAnnotations;
using temuruang_be.Models;

namespace temuruang_be.Dtos.UserDTO;

public class UpdateUserDTO 
{
    public Guid Id { get; set; }
    public string Fullname { get; set; }

    [EmailAddress]
    public string Email { get; set; }

    public string? Password { get; set; }

    public static User ToUser(UpdateUserDTO dto) 
    {
        return new User 
        {
            Id = dto.Id,
            Fullname = dto.Fullname,
            Password = "  ",
            Email = dto.Email
        };
    }

    public static User ToUser(UpdateUserDTO dto, User existingUser) 
    {
        return new User 
        {
            Id = dto.Id,
            Fullname = dto.Fullname,
            Password =  existingUser.Password,
            Email = dto.Email
        };
    }
}