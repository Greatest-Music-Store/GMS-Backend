namespace GMS_Backend.Domain.Repositories;

using GMS_Backend.Domain.Models;

public interface ICartItemRepository
{
    Task CreateAsync(CartItem cartItem);
    Task<CartItem?> GetAsync(Guid userId, Guid productId);
    Task<IEnumerable<CartItem>> GetByUserIdAsync(Guid userId);
    Task DeleteAsync(CartItem cartItem);
}