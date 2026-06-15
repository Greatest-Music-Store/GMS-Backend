using Microsoft.AspNetCore.Mvc;
using GMS_Backend.Application.Services;
using GMS_Backend.Api.Mappers;
using GMS_Backend.Api.DTOs.Feedback;

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
    public async Task<ActionResult<FeedbackResponseDTO>> Create(
        [FromBody] FeedbackCreationDTO dto)
    {
        Guid userId = Guid.Parse("1f67d165-38fe-4d11-814a-004bed73445a");
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
    public async Task<ActionResult<FeedbackResponseDTO>> Delete(Guid id)
    {
        var feedback = await _feedbackService.GetByIdAsync(id);
        if (feedback == null) return NotFound();

        await _feedbackService.DeleteAsync(id);        

        return NoContent();
    }
}