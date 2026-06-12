namespace GMS_Backend.DTOs.Categories;

public class CategoryResponseDTO
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public ICollection<SubcategoryResponseDTO> Subcategories { get; set; } = [];
}