namespace GMS_Backend.DTOs.Product;

using GMS_Backend.DTOs.Feedback;

public class ProductResponseDTO
{
    public Guid ProductId { get; set; }
    public string Name { get; set; }
    public string Brand { get; set; }
    public decimal Price { get; set; }
    public string? Url { get; set;}
    public string Description { get; set; }
    public double Rating { get; set; }
    public int Quantity { get; set; }
    public ICollection<FeedbackResponseDTO> Feedbacks { get; set; } = [];
    public Guid CategoryID { get; set; }
    public Guid SubCategoryId { get; set; }
}