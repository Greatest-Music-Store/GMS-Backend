using Microsoft.AspNetCore.Mvc;
using GMS_Backend.Application.Services;
using GMS_Backend.Api.Mappers;
using Microsoft.AspNetCore.Authorization;
using GMS_Backend.Api.DTOs.Cupom;
using System.Security.Claims;
using GMS_Backend.Application.Results;
namespace GMS_Backend.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CupomController : ControllerBase
{
    private readonly CupomService _cupomService;

    public CupomController(CupomService cupomService)
    {
        _cupomService = cupomService;
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [EndpointDescription("Requer autenticação JWT ADMIN.")]
    public async Task<ActionResult<CupomResponseDTO>> Create([FromBody] CupomCreationDTO dto)
    {
        var cupom = await _cupomService.Create(CupomMapper.ToModel(dto));

        return StatusCode(StatusCodes.Status201Created, CupomMapper.ToDto(cupom));
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<IEnumerable<CupomResponseDTO>>> GetCupons()
    {
        var cupons = await _cupomService.GetCupons();

        return Ok(cupons.Select(CupomMapper.ToDto));
    }

    [HttpPost("validate")]
    [Authorize(Policy = "ActiveUser")]
    public async Task<ActionResult<CupomValidationResult>> ValidateCupom([FromBody] CupomUsageDTO dto)
    {
        Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var result = await _cupomService.Validate(dto.Code, userId);

        return Ok(result);
    }
}