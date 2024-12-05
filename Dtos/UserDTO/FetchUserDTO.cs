using temuruang_be.Models;

namespace temuruang_be.Dtos.UserDTO;

public class FetchUserDTO 
{
    public Guid Id { get; set; }

    public string Fullname { get; set; }

    public string Email { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public ICollection<Booking> Bookings { get; set; }

    public static User ToUser(FetchUserDTO dto)
    {
        return new User
        {
            Id = dto.Id,
            Fullname = dto.Fullname,
            Email = dto.Email,
        };
    }
}