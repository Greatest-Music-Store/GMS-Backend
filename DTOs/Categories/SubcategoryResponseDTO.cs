namespace GMS_Backend.DTOs.Categories;

public class SubcategoryResponseDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid CategoryId { get; set; }
}