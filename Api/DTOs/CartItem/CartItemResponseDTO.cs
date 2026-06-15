using GMS_Backend.Api.DTOs.Product;
namespace GMS_Backend.Api.DTOs.CartItem;

public class CartItemResponseDTO
{
    public ProductResponseDTO Product { get; set; } = null!;

    public int Quantity { get; set; }
}