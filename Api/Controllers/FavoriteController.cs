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
    [Authorize(Policy = "ActiveUser")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [EndpointDescription("Requer autenticação JWT.")]
    public async Task<ActionResult<ToggleFavoriteResponseDTO>> Create([FromBody] FavoriteCreationDTO dto)
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

    [HttpGet("{productId:guid}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [EndpointDescription("Requer autenticação JWT.")]
    public async Task<ActionResult<FavoriteResponseDTO>> GetFavorite(Guid productId)
    {
        Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var favorite = await _favoriteService.GetAsync(userId, productId);

        if (favorite == null) return NotFound();

        return Ok(FavoriteMapper.ToDto(favorite));
    }

    [HttpGet("user")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [EndpointDescription("Requer autenticação JWT.")]
    public async Task<ActionResult<IEnumerable<FavoriteResponseDTO>>> GetAllByUser()
    {
        Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var favorites = await _favoriteService.GetByUserIdAsync(userId);

        return Ok(favorites.Select(FavoriteMapper.ToDto));
    }
}