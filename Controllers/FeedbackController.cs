using Microsoft.AspNetCore.Mvc;
using GMS_Backend.DTOs.Feedback;
using GMS_Backend.Services.Interfaces;

namespace GMS_Backend.Controllers;


[ApiController]
[Route("api/[controller]")]
public class FeedbackController : ControllerBase
{
    private readonly IFeedbackService _feedbackService;

    public FeedbackController(IFeedbackService feedbackService)
    {
        _feedbackService = feedbackService;
    }

    [HttpPost]
    public async Task<ActionResult<FeedbackResponseDTO>> Create(
        [FromBody] FeedbackCreationDTO dto)
    {
        //mudar quanto tiver auth
        Guid userId = Guid.Parse("1f67d165-38fe-4d11-814a-004bed73445a");

        var feedback = await _feedbackService.CreateAsync(dto, userId);

        return CreatedAtAction(
            nameof(GetById),
            new { id = feedback.Id },
            feedback
        );
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<FeedbackResponseDTO>> GetById(Guid id)
    {
        var feedback = await _feedbackService.GetByIdAsync(id);

        if (feedback == null) return NotFound();

        return Ok(feedback);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<FeedbackResponseDTO>>> GetAll()
    {
        var feedbacks = await _feedbackService.GetAllAsync();

        return Ok(feedbacks);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<FeedbackResponseDTO>> Delete(Guid id)
    {
        var feedback = _feedbackService.GetByIdAsync(id);
        if (feedback == null) return NotFound();

        await _feedbackService.DeleteAsync(id);        

        return NoContent();
    }
}