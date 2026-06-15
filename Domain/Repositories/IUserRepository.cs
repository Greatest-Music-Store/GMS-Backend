namespace GMS_Backend.Domain.Repositories;

using GMS_Backend.Domain.Models;

public interface IUserRepository
{
    Task CreateAsync(User user);
    Task<User?> GetByIdAsync(Guid id);
    Task<IEnumerable<User>> GetAllAsync();
    Task DeleteAsync(User user);
    Task<User?> GetByEmailAsync(string email);
}