namespace GMS_Backend.Services.Interfaces;

using GMS_Backend.Models;

public interface ICartItemRepository
{
    Task CreateAsync(CartItem cartItem);
    Task<CartItem?> GetAsync(Guid userId, Guid productId);
    Task<IEnumerable<CartItem>> GetByUserIdAsync(Guid userId);
    Task DeleteAsync(CartItem cartItem);
}