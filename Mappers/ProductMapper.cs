namespace GMS_Backend.Mappers;
using GMS_Backend.Models;
using GMS_Backend.DTOs.Product;
public class ProductMapper
{
    public static ProductResponseDTO ToDto(Product product)
    {
        return new ProductResponseDTO
        {
            ProductId = product.ProductId,
            Name = product.Name,
            Marca = product.Marca,
            Price = product.Price,
            Url = product.Url,
            Description = product.Description,
            Quantity = product.Quantity
        };
    }

    public static Product ToModel(ProductCreationDTO dto)
    {
        return new Product
        {
            ProductId = Guid.NewGuid(),
            Name = dto.Name,
            Marca = dto.Marca,
            Price = dto.Price,
            Url = dto.Url,
            Description = dto.Description,
            Quantity = dto.Quantity,
            CategoryId = dto.CategoryId,
            SubCategoryId = dto.SubCategoryId
        };
    }
}
