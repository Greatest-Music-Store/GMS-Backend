using GMS_Backend.DTOs.Product;

namespace GMS_Backend.DTOs.Favorite;

public class FavoriteResponseDTO
{
    public ProductResponseDTO Product { get; set; } = null!;
}