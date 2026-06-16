namespace GMS_Backend.Api.DTOs.Favorite;
public class ToggleFavoriteResponseDTO
{
    public bool IsFavorite { get; set; }

    public FavoriteResponseDTO? Favorite { get; set; }
}