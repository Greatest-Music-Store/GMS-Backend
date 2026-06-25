namespace GMS_Backend.Api.DTOs.Product;

using GMS_Backend.Api.DTOs.Feedback;

public class ProductResponseDTO
{
    public Guid ProductId { get; set; }
    public required string Name { get; set; }
    public required string Brand { get; set; }
    public decimal Price { get; set; }
    public List<string> ImageUrls { get; set;} = [];
    public required string Description { get; set; }
    public double Rating { get; set; }
    public int Quantity { get; set; }
    public required int DiscountPercentage { get; set; }
    public required string CategoryName { get; set; }
    public required string SubcategoryName { get; set; }
    public ICollection<FeedbackResponseDTO> Feedbacks { get; set; } = [];
    public Guid CategoryID { get; set; }
    public Guid SubCategoryId { get; set; }
}