namespace GMS_Backend.Infrastructure.Repositories;

using GMS_Backend.Data;
using GMS_Backend.Domain.Models;
using GMS_Backend.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

public class FavoriteRepository : IFavoriteRepository
{
    private readonly AppDbContext _context;

    public FavoriteRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(Favorite Favorite)
    {
        _context.Favorites.Add(Favorite);

        await _context.SaveChangesAsync();
    }

    public async Task<Favorite?> GetAsync(Guid userId, Guid productId)
    {
        return await _context.Favorites
            .FirstOrDefaultAsync(p => p.UserId == userId && p.ProductId == productId );
    }

    public async Task<IEnumerable<Favorite>> GetByUserIdAsync(Guid userId)
    {
        return await _context.Favorites
            .Include(f => f.Product)
            .Where(c => c.UserId == userId)
            .ToListAsync();

    }

    public async Task DeleteAsync(Favorite favorite)
    {
        _context.Favorites.Remove(favorite);

        await  _context.SaveChangesAsync();
    }
}