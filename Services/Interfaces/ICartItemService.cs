namespace GMS_Backend.Services.Interfaces;

using GMS_Backend.DTOs.CartItem;

public interface ICartItemService
{
    Task<CartItemResponseDTO> CreateAsync(CartItemCreationDTO dto, Guid userId);
    Task<CartItemResponseDTO?> GetAsync(Guid userId, Guid productId);
    Task<IEnumerable<CartItemResponseDTO>> GetByUserIdAsync(Guid userId);
    Task DeleteAsync(Guid userId, Guid productId);
}