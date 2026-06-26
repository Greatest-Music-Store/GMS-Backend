using Microsoft.AspNetCore.Mvc;
using GMS_Backend.Application.Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using GMS_Backend.Api.DTOs.Purchase;
using GMS_Backend.Api.Mappers;
namespace GMS_Backend.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PurchaseController : ControllerBase
{
    private readonly SimulatePurchaseService _purchaseService;

    public PurchaseController(SimulatePurchaseService purchaseService)
    {
        _purchaseService = purchaseService;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult> Purchase([FromBody] PurchaseDTO dto)
    {
        Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var result = await _purchaseService.SimulatePurchase(userId, dto.ProductsIds, dto.CupomCode);
        
        if (!result.Success)
            return BadRequest(PurchaseMapper.ToDto(result));

        return Ok(PurchaseMapper.ToDto(result));
    }
}