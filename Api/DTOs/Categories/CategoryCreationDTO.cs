namespace GMS_Backend.DTOs.Categories;

using System.ComponentModel.DataAnnotations;

public class CategoryCreationDTO
{
    [Required]
    [StringLength(100, MinimumLength = 2)]
    public required string Name { get; set; }
}
