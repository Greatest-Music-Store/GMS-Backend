using GMS_Backend.DTOs.Categories;

namespace GMS_Backend.Services.Interfaces;

public interface ISubcategoryService
{
    Task<SubcategoryResponseDTO> CreateAsync(SubcategoryCreationDTO dto);
    Task<SubcategoryResponseDTO?> GetByIdAsync(Guid id);
    Task<IEnumerable<SubcategoryResponseDTO>> GetByCategoryIdAsync(Guid categoryId);
    Task DeleteAsync(Guid id);
}