namespace GMS_Backend.Services.Interfaces;

using GMS_Backend.DTOs.Product;

public interface IProductService
{
    Task<ProductResponseDTO> CreateAsync(ProductCreationDTO dto);
    Task<ProductResponseDTO?> GetByIdAsync(Guid id);
    Task<IEnumerable<ProductResponseDTO>> GetAllAsync(string? name, Guid? categoryId, Guid? subCategoryId, int? minPrice, int? maxPrice, string? sortBy, string? brand);
    Task DeleteAsync(Guid id);
}