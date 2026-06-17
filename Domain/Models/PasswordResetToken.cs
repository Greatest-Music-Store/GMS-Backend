namespace GMS_Backend.Domain.Models;
public class PasswordResetToken
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public User User { get; set; } = null!;

    public string Token { get; set; } = null!;

    public DateTime ExpiresAt { get; set; }

    public bool Used { get; set; }
}