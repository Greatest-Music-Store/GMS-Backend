using System.ComponentModel.DataAnnotations;

namespace GMS_Backend.Api.DTOs.Favorite;

public class FavoriteCreationDTO
{
    [Required]
    public Guid ProductId { get; set; }
}