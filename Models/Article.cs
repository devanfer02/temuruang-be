using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace temuruang_be.Models;

[Table("articles")]
public class Article 
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("User")]
    public Guid UserId { get; set; }

    [Required]
    [MaxLength(255)]
    public required string Title { get; set; }

    [Required]
    public required string Description { get; set; }
    
    [Url]
    [Required]
    public required string PhotoLink { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    // Navigation properties
    public User User { get; set; }
}