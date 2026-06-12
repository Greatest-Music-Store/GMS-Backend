using GMS_Backend.DTOs.Categories;

namespace GMS_Backend.Services.Interfaces;

public interface ICategoryService
{
    Task<CategoryResponseDTO> CreateAsync(CategoryCreationDTO dto);
    Task<CategoryResponseDTO?> GetByIdAsync(Guid id);
    Task<IEnumerable<CategoryResponseDTO>> GetAllAsync();
    Task DeleteAsync(Guid id);
}