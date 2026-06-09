namespace GMS_Backend.DTOs.User;

using GMS_Backend.DTOs.Product;

public class UserResponseDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string? Cep { get; set; } = string.Empty;

    public ICollection<ProductResponseDTO> Favorites { get; set; }
    public ICollection<ProductResponseDTO> Chart { get; set; }
}