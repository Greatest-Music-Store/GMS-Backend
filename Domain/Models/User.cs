using GMS_Backend.Domain.Enums;

namespace GMS_Backend.Domain.Models;

public class User
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Phone { get; set; }
    public required string Cpf { get; set; }
    public string? Cep { get; set; }
    public string PasswordHash { get; set; } = string.Empty;
    public UserRole Role { get; set; } = UserRole.Client;

    public ICollection<Favorite> Favorites { get; set; } = [];
    public ICollection<CartItem> CartItems { get; set; } = [];
    public ICollection<PasswordResetToken> PasswordResetTokens{ get; set; } = [];
}