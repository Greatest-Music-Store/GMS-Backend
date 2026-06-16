namespace GMS_Backend.Api.DTOs.Auth;
public class LoginDTO
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}