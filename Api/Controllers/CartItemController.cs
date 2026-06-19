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
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [EndpointDescription("Requer autenticação JWT.")]
    public async Task<ActionResult<CartItemResponseDTO>> Create([FromBody] CartItemCreationDTO dto)
    {   
        Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        try
        {
            var cartItem = await _cartItemService.CreateAsync(CartItemMapper.ToModel(dto, userId));
            return StatusCode(StatusCodes.Status201Created, CartItemMapper.ToDto(cartItem));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("{productId:guid}")]
    public async Task<ActionResult<CartItemResponseDTO>> GetCartItem(Guid productId)
    {
        Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var cartItem = await _cartItemService.GetAsync(userId, productId);

        if (cartItem == null) return NotFound();

        return Ok(CartItemMapper.ToDto(cartItem));
    }

    [HttpGet("user")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [EndpointDescription("Requer autenticação JWT.")]
    public async Task<ActionResult<IEnumerable<CartItemResponseDTO>>> GetAllByUser()
    {
        Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var cartItems = await _cartItemService.GetByUserIdAsync(userId);

        return Ok(cartItems.Select(CartItemMapper.ToDto));
    }

    [HttpDelete("{productId:guid}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [EndpointDescription("Requer autenticação JWT.")]
    public async Task<IActionResult> Delete(Guid productId)
    {
        Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var cartItem = await _cartItemService.GetAsync(userId, productId);
        if (cartItem == null)
        {
            return NotFound();
        }
        await _cartItemService.DeleteAsync(cartItem);

        return NoContent();
    }
}