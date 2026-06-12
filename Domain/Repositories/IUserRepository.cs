namespace GMS_Backend.Services.Interfaces;

using GMS_Backend.Models;

public interface IUserRepository
{
    Task CreateAsync(User user);
    Task<User?> GetByIdAsync(Guid id);
    Task<IEnumerable<User>> GetAllAsync();
    Task DeleteAsync(User user);
}