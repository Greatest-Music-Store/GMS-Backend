using System.ComponentModel.DataAnnotations;

namespace GMS_Backend.Api.DTOs.CartItem;

public class CartItemUpdateDTO
{
    [Required]
    public Guid ProductId { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }
}