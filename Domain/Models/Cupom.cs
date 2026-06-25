namespace GMS_Backend.Domain.Models;

public class Cupom
{
    public Guid Id { get; set; }
    public required string Code { get; set; }
    public DateTime ExpiresAt { get; set; }
    public required int PercentualValue { get; set; }
    public required int MaxUsage { get; set; }
    public required int CurrentUsage { get; set; }
}