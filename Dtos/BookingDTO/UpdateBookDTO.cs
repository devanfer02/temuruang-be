using System.ComponentModel.DataAnnotations;
using temuruang_be.Models;

namespace temuruang_be.Dtos.BookingDTO;

public class UpdateBookDTO 
{
    [Required]
    public required string Status { get; set; }

    public static Booking ToBooking(UpdateBookDTO dto)
    {
        return new Booking
        {
            Status = dto.Status,
        };
    }
}