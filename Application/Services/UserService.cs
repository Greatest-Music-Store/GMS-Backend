using GMS_Backend.Domain.Repositories;
using GMS_Backend.Domain.Models;

namespace GMS_Backend.Application.Services;

public class UserService
{
    private readonly IUserRepository _repository;

    public UserService(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var user = await _repository.GetByIdAsync(id);

        if (user == null) throw new KeyNotFoundException();

        await _repository.DeleteAsync(user);
    }

    public async Task<User> UpdateAsync(User user)
    {
        return await _repository.UpdateAsync(user);
    }
}