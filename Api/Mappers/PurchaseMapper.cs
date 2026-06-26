namespace GMS_Backend.Api.Mappers;

using GMS_Backend.Api.DTOs.Cupom;
using GMS_Backend.Api.DTOs.Purchase;
using GMS_Backend.Application.Results;
using GMS_Backend.Domain.Models;

public class PurchaseMapper
{
    public static PurchaseResponseDTO ToDto(PurchaseResult result)
{
    return new PurchaseResponseDTO
    {
        Success = result.Success,
        Error = result.Error,
        CupomApplied = result.CupomApplied,
        Discount = result.Discount
    };
}
}
