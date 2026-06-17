namespace GMS_Backend.Api.DTOs.Auth;
public class ResetPasswordDTO
{
    public required string Token { get; set; }
    public required string NewPassword { get; set; }
}