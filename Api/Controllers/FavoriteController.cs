using Microsoft.AspNetCore.Mvc;
using GMS_Backend.DTOs.Favorite;
using GMS_Backend.Application.Services;
using GMS_Backend.Api.Mappers;

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
    public async Task<ActionResult<FavoriteResponseDTO>> Create(
        [FromBody] FavoriteCreationDTO dto)
    {
        // temporario ate a autenticação
        Guid userId = Guid.Parse("1f67d165-38fe-4d11-814a-004bed73445a");

        var favorite = await _favoriteService.CreateAsync(FavoriteMapper.ToModel(dto, userId));

        return StatusCode(StatusCodes.Status201Created, FavoriteMapper.ToDto(favorite));
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