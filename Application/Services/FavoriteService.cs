using GMS_Backend.Domain.Repositories;
using GMS_Backend.Domain.Models;
using GMS_Backend.Application.Results;

namespace GMS_Backend.Application.Services;

public class FavoriteService
{
    private readonly IFavoriteRepository _repository;

    public FavoriteService(IFavoriteRepository repository)
    {
        _repository = repository;
    }

    public async Task<ToggleFavoriteResult> ToggleAsync(Favorite favorite)
    {
        var existing = await _repository.GetAsync(favorite.UserId, favorite.ProductId);
        if (existing != null)
        {
            await _repository.DeleteAsync(existing);
            return new ToggleFavoriteResult{ IsFavorite = false, Favorite = null };
        }
        await _repository.CreateAsync(favorite);

        var created = await _repository.GetAsync(favorite.UserId, favorite.ProductId);

        return new ToggleFavoriteResult{IsFavorite = true, Favorite = created};
    }

    public async Task<Favorite?> GetAsync(Guid userId, Guid productId)
    {
        return await _repository.GetAsync(userId, productId);
    }

    public async Task<IEnumerable<Favorite>> GetByUserIdAsync(Guid userId)
    {
        return await _repository.GetByUserIdAsync(userId);
    }
}