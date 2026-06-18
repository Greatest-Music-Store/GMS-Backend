namespace GMS_Backend.Domain.Models;

public class Product
{
    public Guid ProductId { get; set; }
    public required string Name { get; set; }
    public required string Brand { get; set; }
    public decimal Price { get; set; }
    public List<string> ImageUrls { get; set;} = [];
    public required string Description { get; set; }
    public int Quantity { get; set; }

    public ICollection<Feedback> Feedbacks { get; set; } = [];
    public Guid CategoryId { get; set; }
    public Category Category { get; set; } = null!;
    public Guid SubcategoryId { get; set; }
    public Subcategory Subcategory { get; set; } = null!;
}