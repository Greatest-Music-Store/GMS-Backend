using Microsoft.AspNetCore.Mvc;
using GMS_Backend.DTOs.CartItem;
using GMS_Backend.Services.Interfaces;

namespace GMS_Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CartItemController : ControllerBase
{
    private readonly ICartItemService _cartItemService;

    public CartItemController(ICartItemService cartItemService)
    {
        _cartItemService = cartItemService;
    }

    [HttpPost]
    public async Task<ActionResult<CartItemResponseDTO>> Create(
        [FromBody] CartItemCreationDTO dto)
    {
        // temporario ate a autenticação
        Guid userId = Guid.Parse("1f67d165-38fe-4d11-814a-004bed73445a");

        var cartItem = await _cartItemService.CreateAsync(dto, userId);

        return Ok(cartItem);
    }

    [HttpGet("{userId:guid}/{productId:guid}")]
    public async Task<ActionResult<CartItemResponseDTO>> GetCartItem(
        Guid userId,
        Guid productId)
    {
        var cartItem = await _cartItemService.GetAsync(userId, productId);

        if (cartItem == null) return NotFound();

        return Ok(cartItem);
    }

    [HttpGet("user/{userId:guid}")]
    public async Task<ActionResult<IEnumerable<CartItemResponseDTO>>>
        GetByUser(Guid userId)
    {
        var cartItems = await _cartItemService.GetByUserIdAsync(userId);

        return Ok(cartItems);
    }

    [HttpDelete("{userId:guid}/{productId:guid}")]
    public async Task<IActionResult> Delete(
        Guid userId,
        Guid productId)
    {
        await _cartItemService.DeleteAsync(userId, productId);

        return NoContent();
    }
}