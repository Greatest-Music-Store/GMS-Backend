namespace GMS_Backend.DTOs.Product;

using System.ComponentModel.DataAnnotations;

public class ProductCreationDTO
{
    [Required]
    [StringLength(100, MinimumLength = 2)]
    public required string Name { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 2)]
    public required string Marca { get; set; }

    [Required]
    [Range(0.01, 999999.99)]
    public decimal Price { get; set; }

    [Url]
    public string? Url { get; set; }

    [Required]
    [StringLength(2000, MinimumLength = 10)]
    public required string Description { get; set; }

    [Required]
    [Range(0, int.MaxValue)]
    public int Quantity { get; set; }

    [Required]
    public Guid CategoryId { get; set; }

    [Required]
    public Guid SubCategoryId { get; set; }
}