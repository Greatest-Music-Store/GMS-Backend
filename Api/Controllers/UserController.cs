using Microsoft.AspNetCore.Mvc;
using GMS_Backend.Application.Services;
using GMS_Backend.Api.Mappers;
using GMS_Backend.Api.DTOs.User;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using GMS_Backend.Api.DTOs.Purchase;
namespace GMS_Backend.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }
    
    [HttpGet("{id:guid}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [EndpointDescription("Requer autenticação JWT ADMIN.")]
    public async Task<ActionResult<UserResponseDTO>> GetById(Guid id)
    {
        var user = await _userService.GetByIdAsync(id);

        if (user == null) return NotFound();

        return Ok(UserMapper.ToDto(user));
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [EndpointDescription("Requer autenticação JWT ADMIN.")]
    public async Task<ActionResult<IEnumerable<UserResponseDTO>>> GetAll()
    {
        var users = await _userService.GetAllAsync();

        return Ok(users.Select(UserMapper.ToDto));
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [EndpointDescription("Requer autenticação JWT ADMIN.")]
    public async Task<ActionResult<UserResponseDTO>> Delete(Guid id)
    {
        var user = await _userService.GetByIdAsync(id);
        if (user == null) return NotFound();

        await _userService.DeleteAsync(id);        

        return NoContent();
    }

    [HttpPatch]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [EndpointDescription("Requer autenticação JWT.")]
    public async Task<ActionResult<UserResponseDTO>> Update([FromBody] UserUpdateDTO dto)
    {
        Guid id = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var user = await _userService.GetByIdAsync(id);
        if (user == null) return NotFound();

        UserMapper.UpdateToModel(user, dto);

        await _userService.UpdateAsync(user);

        return Ok(UserMapper.ToDto(user));
    }

    [HttpPatch("{userId:guid}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [EndpointDescription("Requer autenticação JWT.")]
    public async Task<ActionResult<UserResponseDTO>> UpdateByAdmin([FromBody] UserUpdateDTO dto, Guid userId)
    {
        var user = await _userService.GetByIdAsync(userId);
        if (user == null) return NotFound();

        UserMapper.UpdateToModel(user, dto);

        await _userService.UpdateAsync(user);

        return Ok(UserMapper.ToDto(user));
    }

    [HttpGet("purchased")]
    [Authorize]
    public async Task<ActionResult<PurchasedProductsDTO>> GetPurchasedProducts()
    {
        Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var products = await _userService.GetPurchasedProducts(userId);

        return PurchaseMapper.ProductToPurchasedProductsDTO(products);
    }
}