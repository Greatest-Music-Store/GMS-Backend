namespace GMS_Backend.Mappers;
using GMS_Backend.Models;
using GMS_Backend.DTOs.Favorite;
public class FavoriteMapper
{
    public static FavoriteResponseDTO ToDto(Favorite favorite)
    {
        return new FavoriteResponseDTO
        {
            Product = ProductMapper.ToDto(favorite.Product)
        };
    }

    public static Favorite ToModel(FavoriteCreationDTO dto, Guid userId)
    {
        return new Favorite
        {
            UserId = userId,
            ProductId = dto.ProductId
        };
    }
}
