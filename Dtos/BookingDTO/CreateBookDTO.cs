using System.ComponentModel.DataAnnotations;
using temuruang_be.Models;

namespace temuruang_be.Dtos.BookingDTO;

public class CreateBookDTO 
{
    [Required]
    public required int WorkspaceId { get; set; }
    public Guid UserId { get; set; }

    public static Booking ToBooking(CreateBookDTO dto)
    {
        return new Booking
        {
            Status = "Waiting",
            WorkspaceId = dto.WorkspaceId,
            UserId = dto.UserId
        };
    }
}