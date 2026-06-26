namespace GMS_Backend.Api.DTOs.Purchase;
public class PurchaseDTO
{
    public ICollection<Guid> ProductsIds { get; set; } = [];
    public string? CupomCode { get; set; }
}