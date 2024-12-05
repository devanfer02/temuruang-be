using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using temuruang_be.Dtos.UserDTO;

namespace temuruang_be.Models;

[Table("users")]
public class User 
{
    [Key]
    [Column("id", TypeName = "uuid")]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(255)]
    public required string Fullname { get; set; }

    [Required]
    [EmailAddress]
    [MaxLength(255)]
    public required string Email { get; set; }

    [MinLength(6)]
    public string Password { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public ICollection<Booking> Bookings { get; set; }

    // mapper
    public static FetchUserDTO ToFetchUserDTO(User user) 
    {
        return new FetchUserDTO
        {
            Id = user.Id,
            Fullname = user.Fullname,
            Email = user.Email,
            CreatedAt = user.CreatedAt,
            UpdatedAt = user.UpdatedAt,
            Bookings = user.Bookings
        };
    }
}