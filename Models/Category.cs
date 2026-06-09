namespace GMS_Backend.Models;

public class Category
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public ICollection<Subcategory> Subcategories { get; set; } = [];
}