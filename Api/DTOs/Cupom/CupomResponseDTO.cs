namespace GMS_Backend.Api.DTOs.Cupom;
public class CupomResponseDTO
{
    public required Guid Id { get; set; }
    public required string Code { get; set; }
    public required DateTime ExpirationDate { get; set; }
    public int PercentualValue { get; set; }
}