using GMS_Backend.Api.DTOs.User;
using GMS_Backend.Api.Mappers;
using GMS_Backend.Application.Auth;
using Microsoft.AspNetCore.Mvc;
namespace GMS_Backend.Api.Auth;
[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(
        AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserResponseDTO>> Register(UserCreationDTO dto)
    {
        var user = UserMapper.ToModel(dto);

        var createdUser =
            await _authService.RegisterAsync(user, dto.Password);

        return Ok(UserMapper.ToDto(createdUser));
    }
}