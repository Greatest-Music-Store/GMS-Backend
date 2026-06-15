using GMS_Backend.Domain.Models;
using GMS_Backend.Domain.Repositories;
namespace GMS_Backend.Application.Services;
public class CartItemService
{
    private readonly ICartItemRepository _repository;

    public CartItemService(ICartItemRepository repository)
    {
        _repository = repository;
    }

    public async Task<CartItem> CreateAsync(CartItem cartItem)
    {
        var existing = await _repository.GetAsync(cartItem.UserId, cartItem.ProductId);
            
        if (existing != null)
        {
            existing.Quantity += cartItem.Quantity; 
        }

        await _repository.CreateAsync(cartItem);
        return cartItem;
    }

    public async Task<CartItem> DeleteAsync(CartItem cartItem)
    {
        await _repository.DeleteAsync(cartItem);

        return cartItem;
    }

    public async Task<IEnumerable<CartItem>> GetByUserIdAsync(Guid userId)
    {
        return await _repository.GetByUserIdAsync(userId);
    }

    public async Task<CartItem?> GetAsync(Guid userId, Guid productId)
    {
        return await _repository.GetAsync(userId, productId);
    }


}