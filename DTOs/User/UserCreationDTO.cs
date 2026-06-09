namespace GMS_Backend.DTOs.User;

using System.ComponentModel.DataAnnotations;

public class UserCreationDTO
{
    [Required]
    [StringLength(100, MinimumLength = 3)]
    public required string Name { get; set; }

    [Required]
    [EmailAddress]
    public required string Email { get; set; }

    [Required]
    [RegularExpression(@"^\d{11}$")]
    public required string Cpf { get; set; }

    [Required]
    [RegularExpression(@"^\d{10,13}$")]
    public required string Phone { get; set; }

    [Required]
    [MinLength(8)]
    public required string Password { get; set; }
}