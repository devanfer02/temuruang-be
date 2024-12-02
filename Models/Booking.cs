using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace temuruang_be.Models;

[Table("bookings")]
public class Booking 
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("User")]
    public Guid UserId { get; set; }

    [ForeignKey("Workspace")]
    public int WorkspaceId { get; set; }

    [Required]
    [MaxLength(50)]
    public required string Status { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public User User { get; set; }
    public Workspace Workspace { get; set; }
}