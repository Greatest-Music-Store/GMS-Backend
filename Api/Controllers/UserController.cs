using Microsoft.AspNetCore.Mvc;
using GMS_Backend.Application.Services;
using GMS_Backend.Api.Mappers;
using GMS_Backend.Api.DTOs.User;
using Microsoft.AspNetCore.Authorization;
namespace GMS_Backend.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }
    
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<UserResponseDTO>> GetById(Guid id)
    {
        var user = await _userService.GetByIdAsync(id);

        if (user == null) return NotFound();

        return Ok(UserMapper.ToDto(user));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserResponseDTO>>> GetAll()
    {
        var users = await _userService.GetAllAsync();

        return Ok(users.Select(UserMapper.ToDto));
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<UserResponseDTO>> Delete(Guid id)
    {
        var user = await _userService.GetByIdAsync(id);
        if (user == null) return NotFound();

        await _userService.DeleteAsync(id);        

        return NoContent();
    }
}