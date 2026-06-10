using Microsoft.EntityFrameworkCore;
using GMS_Backend.Data;
using GMS_Backend.DTOs.CartItem;
using GMS_Backend.Mappers;
using GMS_Backend.Models;
using GMS_Backend.Services.Interfaces;

namespace GMS_Backend.Services;

public class CartItemService : ICartItemService
{
    private readonly AppDbContext _context;

    public CartItemService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<CartItemResponseDTO> CreateAsync(CartItemCreationDTO dto, Guid userId)
    {
        var item = await _context.CartItems
            .FirstOrDefaultAsync(c =>
                c.UserId == userId &&
                c.ProductId == dto.ProductId);

        if (item != null)
        {
            item.Quantity += dto.Quantity;

            await _context.SaveChangesAsync();

            await _context.Entry(item)
                .Reference(c => c.Product)
                .LoadAsync();

            return CartItemMapper.ToDto(item);
        }

        var cartItem = CartItemMapper.ToModel(dto, userId);

        _context.CartItems.Add(cartItem);

        await _context.SaveChangesAsync();

        await _context.Entry(cartItem)
            .Reference(c => c.Product)
            .LoadAsync();

        return CartItemMapper.ToDto(cartItem);
    }

    public async Task<CartItemResponseDTO?> GetAsync(Guid userId, Guid productId)
    {
        var item = await _context.CartItems
            .Include(c => c.Product)
            .FirstOrDefaultAsync(c =>
                c.UserId == userId &&
                c.ProductId == productId);

        if (item == null)
            return null;

        return CartItemMapper.ToDto(item);
    }

    public async Task<IEnumerable<CartItemResponseDTO>> GetByUserIdAsync(Guid userId)
    {
        var items = await _context.CartItems
            .Include(c => c.Product)
            .Where(c => c.UserId == userId)
            .ToListAsync();

        return items
            .Select(CartItemMapper.ToDto)
            .ToList();
    }

    public async Task DeleteAsync(Guid userId, Guid productId)
    {
        var item = await _context.CartItems
            .FirstOrDefaultAsync(c =>
                c.UserId == userId &&
                c.ProductId == productId);

        if (item == null)
            throw new KeyNotFoundException(
                "Item não encontrado no carrinho.");

        _context.CartItems.Remove(item);

        await _context.SaveChangesAsync();
    }
}