using Microsoft.AspNetCore.Mvc;
using GMS_Backend.Application.Services;
using GMS_Backend.Api.Mappers;
using GMS_Backend.Api.DTOs.Favorite;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace GMS_Backend.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FavoriteController : ControllerBase
{
    private readonly FavoriteService _favoriteService;

    public FavoriteController(FavoriteService favoriteService)
    {
        _favoriteService = favoriteService;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<ToggleFavoriteResponseDTO>> Create(
        [FromBody] FavoriteCreationDTO dto)
    {
        Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var result = await _favoriteService.ToggleAsync(FavoriteMapper.ToModel(dto, userId));

        return Ok(new ToggleFavoriteResponseDTO
        {
            IsFavorite = result.IsFavorite,
            Favorite = result.Favorite == null
                ? null
                : FavoriteMapper.ToDto(result.Favorite)
        });
    }

    [HttpGet("{userId:guid}/{productId:guid}")]
    public async Task<ActionResult<FavoriteResponseDTO>> GetFavorite(Guid userId, Guid productId)
    {
        var favorite = await _favoriteService.GetAsync(userId, productId);

        if (favorite == null) return NotFound();

        return Ok(FavoriteMapper.ToDto(favorite));
    }

    [HttpGet("user/{userId:guid}")]
    public async Task<ActionResult<IEnumerable<FavoriteResponseDTO>>> GetByUser(Guid userId)
    {
        var favorites = await _favoriteService.GetByUserIdAsync(userId);

        return Ok(favorites.Select(FavoriteMapper.ToDto));
    }

    [HttpDelete("{userId:guid}/{productId:guid}")]
    [Authorize]
    public async Task<IActionResult> Delete(Guid userId, Guid productId)
    {
        try
        {
            await _favoriteService.DeleteAsync(userId, productId);
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(e.Message);            
        }

        return NoContent();
    }
}