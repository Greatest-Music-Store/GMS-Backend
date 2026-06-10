using Microsoft.AspNetCore.Mvc;
using GMS_Backend.DTOs.Favorite;
using GMS_Backend.Services.Interfaces;

namespace GMS_Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FavoriteController : ControllerBase
{
    private readonly IFavoriteService _favoriteService;

    public FavoriteController(IFavoriteService favoriteService)
    {
        _favoriteService = favoriteService;
    }

    [HttpPost]
    public async Task<ActionResult<FavoriteResponseDTO>> Create(
        [FromBody] FavoriteCreationDTO dto)
    {
        
        // temporario ate a autenticação
        Guid userId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        var favorite = await _favoriteService.CreateAsync(dto, userId);

        return Ok(favorite);
    }

    [HttpGet("{userId:guid}/{productId:guid}")]
    public async Task<ActionResult<FavoriteResponseDTO>> GetFavorite(
        Guid userId,
        Guid productId)
    {
        var favorite = await _favoriteService.GetAsync(userId, productId);

        if (favorite == null) return NotFound();

        return Ok(favorite);
    }

    [HttpGet("user/{userId:guid}")]
    public async Task<ActionResult<IEnumerable<FavoriteResponseDTO>>>
        GetByUser(Guid userId)
    {
        var favorites = await _favoriteService.GetByUserIdAsync(userId);

        return Ok(favorites);
    }

    [HttpDelete("{userId:guid}/{productId:guid}")]
    public async Task<IActionResult> Delete(
        Guid userId,
        Guid productId)
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