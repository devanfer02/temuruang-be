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
    public string Name { get; set; }

    public string Description { get; set; }

    [Required]
    [MaxLength(255)]
    public string Location { get; set; }

    [Required]
    [MaxLength(50)]
    public string Type { get; set; }

    [Required]
    public int Price { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public ICollection<Booking> Bookings { get; set; }
}