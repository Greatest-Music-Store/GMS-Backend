namespace GMS_Backend.Domain.Repositories;

using GMS_Backend.Domain.Models;

public interface IFavoriteRepository
{
    Task CreateAsync(Favorite favorite);
    Task<Favorite?> GetAsync(Guid userId, Guid productId);
    Task<IEnumerable<Favorite>> GetByUserIdAsync(Guid userId);
    Task DeleteAsync(Favorite favorite);
}