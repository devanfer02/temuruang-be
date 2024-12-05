using System.ComponentModel.DataAnnotations;
using temuruang_be.Models;

namespace temuruang_be.Dtos.BookingDTO;

public class CreateBookDTO
{
    [Required]
    public required int WorkspaceId { get; set; }
    public Guid UserId { get; set; }
    [Required]
    public required int DurationOfUse { get; set; }

    [Required]
    public required string PaymentMethod { get; set; }

    [Required]
    public required DateOnly BookedAt { get; set; }

    public static Booking ToBooking(CreateBookDTO dto)
    {
        return new Booking
        {
            Status = "Waiting",
            WorkspaceId = dto.WorkspaceId,
            UserId = dto.UserId,
            DurationOfUse = dto.DurationOfUse,
            PaymentMethod = dto.PaymentMethod,
            BookedAt = dto.BookedAt 
        };
    }
}