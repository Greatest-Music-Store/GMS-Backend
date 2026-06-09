namespace GMS_Backend.DTOs.Categories;

using System.ComponentModel.DataAnnotations;

public class SubcategoriesCreationDTO
{
    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string Name { get; set; }

    [Required]
    public Guid CategoryId { get; set; }
}