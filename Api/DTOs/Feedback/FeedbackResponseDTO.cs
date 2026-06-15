namespace GMS_Backend.Api.DTOs.Feedback;

public class FeedbackResponseDTO
{
    public Guid Id { get; set;}
    public Guid UserId { get; set;}
    public string Description { get; set; } = string.Empty;
    public int Rating { get; set; }
    public Guid ProductId { get; set; }
}