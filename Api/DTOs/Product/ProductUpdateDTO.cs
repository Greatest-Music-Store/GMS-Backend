namespace GMS_Backend.Api.DTOs.Product;


public class ProductUpdateDTO
{
    public string? Name { get; set; }
    public string? Brand { get; set; }
    public decimal? Price { get; set; }
    public List<string>? ImageUrls { get; set;} = [];
    public string? Description { get; set; }
    public int? Quantity { get; set; }
    public int? DiscountPercentage { get; set; }
}