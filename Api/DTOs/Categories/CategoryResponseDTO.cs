namespace GMS_Backend.Api.DTOs.Categories;

public class CategoryResponseDTO
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public ICollection<SubcategoryResponseDTO> Subcategories { get; set; } = [];
}