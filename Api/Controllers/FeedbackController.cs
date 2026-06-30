using Microsoft.AspNetCore.Mvc;
using GMS_Backend.Application.Services;
using GMS_Backend.Api.Mappers;
using GMS_Backend.Api.DTOs.Feedback;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace GMS_Backend.Api.Controllers;


[ApiController]
[Route("api/[controller]")]
public class FeedbackController : ControllerBase
{
    private readonly FeedbackService _feedbackService;

    public FeedbackController(FeedbackService feedbackService)
    {
        _feedbackService = feedbackService;
    }

    [HttpPost]
    [Authorize(Policy = "ActiveUser")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [EndpointDescription("Requer autenticação JWT.")]
    public async Task<ActionResult<FeedbackResponseDTO>> Create([FromBody] FeedbackCreationDTO dto)
    {
        Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var feedback = await _feedbackService.CreateAsync(FeedbackMapper.ToModel(dto, userId));

        return CreatedAtAction(
            nameof(GetById),
            new { id = feedback.Id },
            FeedbackMapper.ToDto(feedback)
        );
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<FeedbackResponseDTO>> GetById(Guid id)
    {
        var feedback = await _feedbackService.GetByIdAsync(id);

        if (feedback == null) return NotFound();

        return Ok(FeedbackMapper.ToDto(feedback));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<FeedbackResponseDTO>>> GetAll()
    {
        var feedbacks = await _feedbackService.GetAllAsync();

        return Ok(feedbacks.Select(FeedbackMapper.ToDto));
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Admin", Policy = "ActiveUser")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [EndpointDescription("Requer autenticação JWT ADMIN.")]
    public async Task<ActionResult<FeedbackResponseDTO>> Delete(Guid id)
    {
        var feedback = await _feedbackService.GetByIdAsync(id);
        if (feedback == null) return NotFound();

        await _feedbackService.DeleteAsync(id);        

        return NoContent();
    }
}