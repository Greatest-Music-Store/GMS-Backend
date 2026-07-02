using GMS_Backend.Domain.Models;

namespace GMS_Backend.Application.Results;
public class CupomValidationResult
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public Cupom? Cupom { get; set; }
}