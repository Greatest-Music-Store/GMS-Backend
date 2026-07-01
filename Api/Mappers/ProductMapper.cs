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
            Price = product.DiscountPercentage == 0 ? product.Price : product.Price - (product.Price * product.DiscountPercentage / 100),
            Rating = ProductService.GetAverageRating(product),
            ImageUrls = product.ImageUrls,
            Description = product.Description,
            Quantity = product.Quantity,
            DiscountPercentage = product.DiscountPercentage,
            CategoryName = product.Category.Name,
            SubcategoryName = product.Subcategory.Name,
            Feedbacks = product.Feedbacks?.Select(FeedbackMapper.ToDto).ToList() ?? [],
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
            ImageUrls = dto.ImageUrls,
            Description = dto.Description,
            Quantity = dto.Quantity,
            DiscountPercentage = dto.DiscountPercentage,
            CategoryId = dto.CategoryId,
            SubcategoryId = dto.SubCategoryId
        };
    }

    public static void UpdateToModel(Product product, ProductUpdateDTO dto)
    {
        if (!string.IsNullOrWhiteSpace(dto.Name))
            product.Name = dto.Name;

        if (!string.IsNullOrWhiteSpace(dto.Brand))
            product.Brand = dto.Brand;

        if (dto.Price.HasValue)
            product.Price = dto.Price.Value;

        if (dto.ImageUrls != null)
            product.ImageUrls = dto.ImageUrls;

        if (!string.IsNullOrWhiteSpace(dto.Description))
            product.Description = dto.Description;

        if (dto.Quantity.HasValue)
            product.Quantity = dto.Quantity.Value;

        if (dto.DiscountPercentage.HasValue)
            product.DiscountPercentage = dto.DiscountPercentage.Value;
    }
}
