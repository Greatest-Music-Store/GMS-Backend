using GMS_Backend.Services.Interfaces;
using GMS_Backend.Models;

namespace GMS_Backend.Application.Services;

public class UserService
{
    private readonly IUserRepository _repository;

    public UserService(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<User> CreateAsync(User user)
    {
        await _repository.CreateAsync(user);

        return user;
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
}