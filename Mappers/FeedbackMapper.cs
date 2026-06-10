namespace GMS_Backend.Mappers;
using GMS_Backend.Models;
using GMS_Backend.DTOs.Feedback;
public class FeedbackMapper
{
    public static FeedbackResponseDTO ToDto(Feedback feedback)
    {
        return new FeedbackResponseDTO
        {
            Description = feedback.Description,
            Rating = feedback.Rating,
            ProductId = feedback.ProductId
        };
    }

    public static Feedback ToModel(FeedbackCreationDTO dto, Guid userId)
    {
        return new Feedback
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Description = dto.Description,
            Rating = dto.Rating,
            ProductId = dto.ProductId
        };
    }
}
