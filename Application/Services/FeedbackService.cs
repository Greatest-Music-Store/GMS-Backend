using GMS_Backend.Services.Interfaces;
using GMS_Backend.Models;

namespace GMS_Backend.Application.Services;

public class FeedbackService
{
    private readonly IFeedbackRepository _repository;

    public FeedbackService(IFeedbackRepository repository)
    {
        _repository = repository;
    }

    public async Task<Feedback> CreateAsync(Feedback feedback)
    {
        await _repository.CreateAsync(feedback);

        return feedback;
    }

    public async Task<Feedback?> GetByIdAsync(Guid id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Feedback>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var feedback = await _repository.GetByIdAsync(id);

        if (feedback == null) throw new KeyNotFoundException();

        await _repository.DeleteAsync(feedback);
    }
}