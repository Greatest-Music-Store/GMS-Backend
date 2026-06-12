using Microsoft.EntityFrameworkCore;
using GMS_Backend.Data;
using GMS_Backend.DTOs.Favorite;
using GMS_Backend.Mappers;
using GMS_Backend.Models;
using GMS_Backend.Services.Interfaces;

namespace GMS_Backend.Services;

public class FavoriteService : IFavoriteService
{
    private readonly AppDbContext _context;

    public FavoriteService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<FavoriteResponseDTO> CreateAsync(FavoriteCreationDTO dto, Guid userId)
    {
        var exists = await _context.Favorites.AnyAsync(f =>
            f.UserId == userId &&
            f.ProductId == dto.ProductId);

        if (exists)
            throw new InvalidOperationException(
                "Produto já está nos favoritos.");

        var favorite = FavoriteMapper.ToModel(dto, userId);

        _context.Favorites.Add(favorite);

        await _context.SaveChangesAsync();

        await _context.Entry(favorite)
            .Reference(f => f.Product)
            .LoadAsync();

        return FavoriteMapper.ToDto(favorite);
    }

    public async Task<FavoriteResponseDTO?> GetAsync(Guid userId, Guid productId)
    {
        var favorite = await _context.Favorites
            .Include(f => f.Product)
            .FirstOrDefaultAsync(f =>
                f.UserId == userId &&
                f.ProductId == productId);

        if (favorite == null)
            return null;

        return FavoriteMapper.ToDto(favorite);
    }

    public async Task<IEnumerable<FavoriteResponseDTO>> GetByUserIdAsync(Guid userId)
    {
        var favorites = await _context.Favorites
            .Include(f => f.Product)
            .Where(f => f.UserId == userId)
            .ToListAsync();

        return favorites
            .Select(FavoriteMapper.ToDto)
            .ToList();
    }

    public async Task DeleteAsync(Guid userId, Guid productId)
    {
        var favorite = await _context.Favorites
            .FirstOrDefaultAsync(f =>
                f.UserId == userId &&
                f.ProductId == productId);

        if (favorite == null)
            throw new KeyNotFoundException(
                "Favorito não encontrado.");

        _context.Favorites.Remove(favorite);

        await _context.SaveChangesAsync();
    }
}