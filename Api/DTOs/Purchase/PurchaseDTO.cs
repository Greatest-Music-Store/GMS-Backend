namespace GMS_Backend.Api.DTOs.Purchase;
public class PurchaseDTO
{
    public ICollection<Guid> ProductIds { get; set; } = [];
    public string? CupomCode { get; set; }
}