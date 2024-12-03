using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace temuruang_be.Models;

[Table("workspaces")]
public class Workspace 
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(255)]
    public required string Name { get; set; }

    [Required]
    public required string Description { get; set; }

    [Required]
    [MaxLength(255)]
    public required string Location { get; set; }

    [Required]
    [MaxLength(50)]
    public required string Type { get; set; }

    [Required]
    public required long Price { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public ICollection<Booking> Bookings { get; set; }
}