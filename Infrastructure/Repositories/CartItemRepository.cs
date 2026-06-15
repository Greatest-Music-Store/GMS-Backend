namespace GMS_Backend.Infrastructure.Repositories;

using Microsoft.EntityFrameworkCore;
using GMS_Backend.Domain.Repositories;
using GMS_Backend.Domain.Models;
using GMS_Backend.Infrastructure.Data;

public class CartItemRepository : ICartItemRepository
{
    private readonly AppDbContext _context;

    public CartItemRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(CartItem cartItem)
    {
        _context.CartItems.Add(cartItem);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(CartItem cartItem)
    {
        _context.CartItems.Remove(cartItem);

        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<CartItem>> GetByUserIdAsync(Guid userId)
    {
        return await _context.CartItems
            .Include(c => c.Product)
            .ThenInclude(p => p.Feedbacks)
            .Where(c => c.UserId == userId)
            .ToListAsync();
    }

    public async Task<CartItem?> GetAsync(Guid userId, Guid productId)
    {
        return await _context.CartItems
            .Include(c => c.Product)
            .ThenInclude(p => p.Feedbacks)
            .FirstOrDefaultAsync(c => c.UserId == userId && c.ProductId == productId);
    }
}