using GMS_Backend.Domain.Models;
using GMS_Backend.Infrastructure.Repositories;

namespace GMS_Backend.Application.Services;

public class SubcategoryService
{
    private readonly SubcategoryRepository _repository;

    public SubcategoryService(SubcategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<Subcategory> CreateAsync(Subcategory subcategory)
    {
        await _repository.CreateAsync(subcategory);

        return subcategory;
    }

    public async Task<Subcategory?> GetByIdAsync(Guid id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Subcategory>> GetByCategoryIdAsync(Guid categoryId)
    {
        return await _repository.GetByCategoryIdAsync(categoryId);
    }

    public async Task DeleteAsync(Guid id)
    {
        var subcategory = await _repository.GetByIdAsync(id);

        if (subcategory == null) throw new KeyNotFoundException();

        await _repository.DeleteAsync(subcategory);
    }
}