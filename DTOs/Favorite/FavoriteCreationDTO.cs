using System.ComponentModel.DataAnnotations;

namespace GMS_Backend.DTOs.Favorite;

public class FavoriteCreationDTO
{
    [Required]
    public Guid ProductId { get; set; }
}