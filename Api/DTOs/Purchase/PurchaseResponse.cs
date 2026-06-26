namespace GMS_Backend.Api.DTOs.Purchase;
public class PurchaseResponseDTO
{
    public bool Success { get; set; }
    public string? Error { get; set; }
    public bool CupomApplied { get; set; }
    public int Discount { get; set; }
}