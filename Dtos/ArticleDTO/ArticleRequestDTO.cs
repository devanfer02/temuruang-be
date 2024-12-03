using System.ComponentModel.DataAnnotations;
using temuruang_be.Models;

namespace temuruang_be.Dtos.ArticleDTO;

public class ArticleRequestDTO
{
    [Required]
    public required string Title { get; set; }

    [Required]
    public required string Description { get; set; }

    public static Article ToArticle(ArticleRequestDTO dto)
    {
        return new Article
        {
            Title = dto.Title,
            Description = dto.Description,
            PhotoLink = ""
        };
    }
}