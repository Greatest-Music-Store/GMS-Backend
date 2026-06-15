using GMS_Backend.Domain.Models;
using GMS_Backend.Domain.Repositories;
namespace GMS_Backend.Application.Services;

public class CategoryService
{
    private readonly ICategoryRepository _repository;

    public CategoryService(ICategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<Category> CreateAsync(Category category)
    {
        await _repository.CreateAsync(category);

        return category;
    }

    public async Task<Category?> GetByIdAsync(Guid id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var category = await _repository.GetByIdAsync(id); 

        if (category == null) throw new KeyNotFoundException();
        
        await _repository.DeleteAsync(category);
    }
}