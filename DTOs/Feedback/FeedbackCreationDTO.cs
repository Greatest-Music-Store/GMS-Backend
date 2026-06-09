namespace GMS_Backend.DTOs.Feedback;

using System.ComponentModel.DataAnnotations;

public class FeedbackCreationDTO
{
    [Required]
    [StringLength(1000, MinimumLength = 5)]
    public required string Description { get; set; }

    [Required]
    [Range(1, 5)]
    public int Rating { get; set; }

    [Required]
    public Guid ProductId { get; set; }
}
