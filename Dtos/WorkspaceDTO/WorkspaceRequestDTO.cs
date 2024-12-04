using System.ComponentModel.DataAnnotations;
using temuruang_be.Models;

namespace temuruang_be.Dtos.WorkspaceDTO;

public class WorkspaceRequestDTO
{
    [Required]
    public required string Name { get; set; }

    [Required]
    public required string Description { get; set; }
    [Required]
    public required string Location { get; set; }
    [Required]
    public required string Type { get; set; }
    [Required]
    public required long Price { get; set; }
    [Required]
    public required int Capacity { get; set; }

    public static Workspace ToWorkspace(WorkspaceRequestDTO dto)
    {
        return new Workspace
        {
            Name = dto.Name,
            Description = dto.Description,
            Location = dto.Location,
            Type = dto.Type,
            Price = dto.Price,
            Capacity = dto.Capacity,
            ImageLink = ""
        };
    }
}