using GMS_Backend.Domain.Repositories;
using GMS_Backend.Domain.Models;

namespace GMS_Backend.Application.Services;

public class FavoriteService
{
    private readonly IFavoriteRepository _repository;

    public FavoriteService(IFavoriteRepository repository)
    {
        _repository = repository;
    }

    public async Task<Favorite> CreateAsync(Favorite favorite)
    {
        await _repository.CreateAsync(favorite);

        return favorite;
    }

    public async Task<Favorite?> GetAsync(Guid userId, Guid productId)
    {
        return await _repository.GetAsync(userId, productId);
    }

    public async Task<IEnumerable<Favorite>> GetByUserIdAsync(Guid userId)
    {
        return await _repository.GetByUserIdAsync(userId);
    }

    public async Task DeleteAsync(Guid userId, Guid productId)
    {
        var favorite = await _repository.GetAsync(userId, productId); 

        if (favorite == null) throw new KeyNotFoundException();
        
        await _repository.DeleteAsync(favorite);
    }
}