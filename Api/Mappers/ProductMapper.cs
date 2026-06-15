namespace GMS_Backend.Api.Mappers;
using GMS_Backend.Domain.Models;
using GMS_Backend.Application.Services;
using GMS_Backend.Api.DTOs.Product;

public class ProductMapper
{
    public static ProductResponseDTO ToDto(Product product)
    {
        return new ProductResponseDTO
        {
            ProductId = product.ProductId,
            Name = product.Name,
            Brand = product.Brand,
            Price = product.Price,
            Rating = ProductService.GetAverageRating(product),
            Url = product.Url,
            Description = product.Description,
            Quantity = product.Quantity,
            CategoryName = product.Category.Name,
            SubcategoryName = product.Subcategory.Name,
            Feedbacks = product.Feedbacks.Select(FeedbackMapper.ToDto).ToList(),
            SubCategoryId = product.SubcategoryId,
            CategoryID = product.CategoryId
        };
    }

    public static Product ToModel(ProductCreationDTO dto)
    {
        return new Product
        {
            ProductId = Guid.NewGuid(),
            Name = dto.Name,
            Brand = dto.Brand,
            Price = dto.Price,
            Url = dto.Url,
            Description = dto.Description,
            Quantity = dto.Quantity,
            CategoryId = dto.CategoryId,
            SubcategoryId = dto.SubCategoryId
        };
    }
}
