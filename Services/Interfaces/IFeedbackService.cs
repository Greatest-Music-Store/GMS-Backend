namespace GMS_Backend.Services.Interfaces;

using GMS_Backend.DTOs.Feedback;

public interface IFeedbackService
{
    Task<FeedbackResponseDTO> CreateAsync(FeedbackCreationDTO dto);
    Task<FeedbackResponseDTO?> GetByIdAsync(Guid id);
    Task<IEnumerable<FeedbackResponseDTO>> GetAllAsync();
    Task DeleteAsync(Guid id);
}