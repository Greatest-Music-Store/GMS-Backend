namespace GMS_Backend.Domain.Models;

public class Subcategory
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public Guid CategoryId { get; set; }
    public Category Category { get; set; } = null!;
}