namespace GMS_Backend.Application.Results;

using GMS_Backend.Domain.Models;

public class ToggleFavoriteResult
{
    public bool IsFavorite { get; set; }
    public Favorite? Favorite { get; set; }
}