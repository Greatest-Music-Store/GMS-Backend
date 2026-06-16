using Microsoft.AspNetCore.Mvc;
using GMS_Backend.Application.Services;
using GMS_Backend.Api.Mappers;
using GMS_Backend.Api.DTOs.CartItem;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

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
    [Authorize]
    public async Task<ActionResult<CartItemResponseDTO>> Create([FromBody] CartItemCreationDTO dto)
    {   
        Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

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
    [Authorize]
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