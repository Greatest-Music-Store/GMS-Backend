using Microsoft.AspNetCore.Mvc;
using GMS_Backend.Application.Services;
using GMS_Backend.Api.Mappers;
using GMS_Backend.Api.DTOs.Categories;
using Microsoft.AspNetCore.Authorization;
using GMS_Backend.Api.DTOs.Cupom;
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
    public async Task<ActionResult<IEnumerable<CupomResponseDTO>>> GetCupons()
    {
        var cupons = await _cupomService.GetCupons();

        return Ok(cupons.Select(CupomMapper.ToDto));
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<CupomResponseDTO>> UseCupom([FromBody] CupomUsageDTO dto)
    {
        var cupom = await _cupomService.Validate(dto.Code);

        if (cupom == null)
        {
            return BadRequest("Cupom inválido.");
        }

        return Ok(new CupomResponseDTO
        {
            Id = cupom.Id,
            Code = cupom.Code,

        });
    }
}