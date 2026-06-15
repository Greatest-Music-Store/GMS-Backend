namespace GMS_Backend.Api.Mappers;
using GMS_Backend.Domain.Models;
using GMS_Backend.Api.DTOs.CartItem;

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
