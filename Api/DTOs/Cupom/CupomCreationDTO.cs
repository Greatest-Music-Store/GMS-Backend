namespace GMS_Backend.Api.DTOs.Cupom;

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class CupomCreationDTO
{
    [Required]
    [StringLength(100, MinimumLength = 2)]
    public required string Code { get; set; }

    [Required]
    [JsonConverter(typeof(DateFormatConverter))]
    public required DateTime ExpirationDate { get; set; }

    [Required]
    [Range(1, 100)]
    public int PercentualValue { get; set; }

    [Required]
    public int MaxUsage { get; set; }
}