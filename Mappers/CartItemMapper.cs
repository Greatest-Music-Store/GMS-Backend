namespace GMS_Backend.Mappers;
using GMS_Backend.Models;
using GMS_Backend.DTOs.CartItem;
public class CartItemMapper
{
    public static CartItemResponseDTO ToDto(CartItem cartItem)
    {
        return new CartItemResponseDTO
        {
            Product = ProductMapper.ToDto(cartItem.Product),
            Quantity = cartItem.Quantity
        };
    }

    public static CartItem ToModel(CartItemCreationDTO dto, Guid userId)
    {
        return new CartItem
        {
            UserId = userId,
            ProductId = dto.ProductId,
            Quantity = dto.Quantity
        };
    }
}
