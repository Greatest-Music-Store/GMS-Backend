namespace GMS_Backend.Models;

public class Subcategory
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public Guid CategoryId { get; set; }
    public Category Category { get; set; } = null!;
}