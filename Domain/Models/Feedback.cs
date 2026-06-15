namespace GMS_Backend.Domain.Models;

public class Feedback
{
    public Guid Id { get; set;}
    public Guid UserId { get; set;}
    public required string Description { get; set; }
    public required int Rating { get; set; }
    public Guid ProductId { get; set; }
    public Product Product { get; set; } = null!;
    public User User { get; set; } = null!;
}