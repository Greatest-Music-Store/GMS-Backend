using Microsoft.AspNetCore.Mvc;
using GMS_Backend.DTOs.User;
using GMS_Backend.Application.Services;
using GMS_Backend.Api.Mappers;
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

    [HttpPost]
    public async Task<ActionResult<UserResponseDTO>> Create(
        [FromBody] UserCreationDTO dto)
    {
        var user = await _userService.CreateAsync(UserMapper.ToModel(dto));

        return CreatedAtAction(
            nameof(GetById),
            new { id = user.Id },
            UserMapper.ToDto(user)
        );
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
    public async Task<ActionResult<UserResponseDTO>> Delete(Guid id)
    {
        var user = await _userService.GetByIdAsync(id);
        if (user == null) return NotFound();

        await _userService.DeleteAsync(id);        

        return NoContent();
    }
}