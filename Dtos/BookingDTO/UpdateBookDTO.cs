using System.ComponentModel.DataAnnotations;
using temuruang_be.Models;

namespace temuruang_be.Dtos.BookingDTO;

public class UpdateBookDTO
{
    [Required]
    public required string Status { get; set; }
    [Required]
    public required int DurationOfUse { get; set; }

    [Required]
    public required string PaymentMethod { get; set; }

    [Required]
    public required DateOnly BookedAt { get; set; }

    public static Booking ToBooking(UpdateBookDTO dto)
    {
        return new Booking
        {
            Status = dto.Status,
            DurationOfUse = dto.DurationOfUse,
            PaymentMethod = dto.PaymentMethod,
            BookedAt = dto.BookedAt
        };
    }
}