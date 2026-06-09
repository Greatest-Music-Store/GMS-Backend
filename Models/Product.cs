namespace GMS_Backend.Models;

public class Product
{
    public Guid ProductId { get; set; }
    public string Name { get; set; }
    public string Marca { get; set; }
    public decimal Price { get; set; }
    public string? Url { get; set;}
    public string Description { get; set; }
    public int Rating { get; set; }
    public int Quantity { get; set; }

    public ICollection<Feedback> Feedbacks { get; set; }
    public Guid CategoryId { get; set; }
    public Category Category { get; set; } = null!;
    public Guid SubCategoryId { get; set; }
    public Subcategory SubCategory { get; set; } = null!;
}