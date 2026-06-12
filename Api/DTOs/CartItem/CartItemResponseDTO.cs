using GMS_Backend.DTOs.Product;

namespace GMS_Backend.DTOs.CartItem;

public class CartItemResponseDTO
{
    public ProductResponseDTO Product { get; set; } = null!;

    public int Quantity { get; set; }
}