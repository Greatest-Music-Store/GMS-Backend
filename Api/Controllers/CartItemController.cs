using Microsoft.AspNetCore.Mvc;
using GMS_Backend.DTOs.CartItem;
using GMS_Backend.Application.Services;
using GMS_Backend.Api.Mappers;

namespace GMS_Backend.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CartItemController : ControllerBase
{
    private readonly CartItemService _cartItemService;

    public CartItemController(CartItemService cartItemService)
    {
        _cartItemService = cartItemService;
    }

    [HttpPost]
    public async Task<ActionResult<CartItemResponseDTO>> Create([FromBody] CartItemCreationDTO dto)
    {
        // temporario ate a autenticação
        Guid userId = Guid.Parse("1f67d165-38fe-4d11-814a-004bed73445a");

        var cartItem = await _cartItemService.CreateAsync(CartItemMapper.ToModel(dto, userId));

        return StatusCode(StatusCodes.Status201Created, CartItemMapper.ToDto(cartItem));
    }

    [HttpGet("{userId:guid}/{productId:guid}")]
    public async Task<ActionResult<CartItemResponseDTO>> GetCartItem(Guid userId, Guid productId)
    {
        var cartItem = await _cartItemService.GetAsync(userId, productId);

        if (cartItem == null) return NotFound();

        return Ok(CartItemMapper.ToDto(cartItem));
    }

    [HttpGet("user/{userId:guid}")]
    public async Task<ActionResult<IEnumerable<CartItemResponseDTO>>> GetByUser(Guid userId)
    {
        var cartItems = await _cartItemService.GetByUserIdAsync(userId);

        return Ok(cartItems.Select(CartItemMapper.ToDto));
    }

    [HttpDelete("{userId:guid}/{productId:guid}")]
    public async Task<IActionResult> Delete(Guid userId, Guid productId)
    {
        var cartItem = await _cartItemService.GetAsync(userId, productId);
        if (cartItem == null)
        {
            return NotFound();
        }
        await _cartItemService.DeleteAsync(cartItem);

        return NoContent();
    }
}