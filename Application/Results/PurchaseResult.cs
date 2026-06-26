namespace GMS_Backend.Application.Results;
public class PurchaseResult
{
    public bool Success { get; set; }
    public string? Error { get; set; }

    public bool CupomApplied { get; set; }
    public int Discount { get; set; }

    public static PurchaseResult Ok(bool cupomApplied = false, int discount = 0)
        => new() { Success = true, CupomApplied = cupomApplied, Discount = discount };

    public static PurchaseResult Fail(string error)
        => new() { Success = false, Error = error };
}