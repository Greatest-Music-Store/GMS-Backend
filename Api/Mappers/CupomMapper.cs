namespace GMS_Backend.Api.Mappers;

using GMS_Backend.Api.DTOs.Cupom;
using GMS_Backend.Domain.Models;

public class CupomMapper
{
    public static CupomResponseDTO ToDto(Cupom cupom)
    {
        return new CupomResponseDTO
        {
            Id = cupom.Id,
            Code = cupom.Code,
            ExpirationDate = cupom.ExpiresAt,
            PercentualValue = cupom.PercentualValue,
        };
    }

    public static Cupom ToModel(CupomCreationDTO dto)
    {
        return new Cupom
        {
            Id = Guid.NewGuid(),
            Code = dto.Code,
            ExpiresAt = dto.ExpirationDate,
            PercentualValue = dto.PercentualValue,
            MaxUsage = dto.MaxUsage,
            CurrentUsage = 0
        };
    }
}
