namespace GMS_Backend.Services.Interfaces;

using GMS_Backend.DTOs.Favorite;

public interface IFavoriteService
{
    Task<FavoriteResponseDTO> CreateAsync(FavoriteCreationDTO dto, Guid userId);
    Task<FavoriteResponseDTO?> GetAsync(Guid userId, Guid productId);
    Task<IEnumerable<FavoriteResponseDTO>> GetByUserIdAsync(Guid userId);
    Task DeleteAsync(Guid userId, Guid productId);
}