namespace GMS_Backend.Api.DTOs.Categories;

using System.ComponentModel.DataAnnotations;

public class SubcategoryCreationDTO
{
    [Required]
    [StringLength(100, MinimumLength = 2)]
    public required string Name { get; set; }

    [Required]
    public Guid CategoryId { get; set; }
}