using GMS_Backend.Domain.Models;
namespace GMS_Backend.Domain.Repositories;

public interface IFeedbackRepository
{
    Task CreateAsync(Feedback feedBack);
    Task<Feedback?> GetByIdAsync(Guid id);
    Task<IEnumerable<Feedback>> GetAllAsync();
    Task DeleteAsync(Feedback feedback);
}