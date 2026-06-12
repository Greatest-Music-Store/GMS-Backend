using GMS_Backend.Models;

namespace GMS_Backend.Services.Interfaces;
public interface IFeedbackRepository
{
    Task CreateAsync(Feedback feedBack);
    Task<Feedback?> GetByIdAsync(Guid id);
    Task<IEnumerable<Feedback>> GetAllAsync();
    Task DeleteAsync(Feedback feedback);
}