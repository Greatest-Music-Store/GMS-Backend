using Microsoft.AspNetCore.Mvc;
using GMS_Backend.Services.Interfaces;
using GMS_Backend.DTOs.Categories;

namespace GMS_Backend.Controllers;


[ApiController]
[Route("api/[controller]")]
public class SubcategoryController : ControllerBase
{
    private readonly ISubcategoryService _subcategoryService;

    public SubcategoryController(ISubcategoryService subcategoryService)
    {
        _subcategoryService = subcategoryService;
    }

    [HttpPost]
    public async Task<ActionResult<SubcategoryResponseDTO>> Create(
        [FromBody] SubcategoryCreationDTO dto)
    {
        var subcategory = await _subcategoryService.CreateAsync(dto);

        return CreatedAtAction(
            nameof(GetById),
            new { id = subcategory.Id },
            subcategory
        );
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<SubcategoryResponseDTO>> GetById(Guid id)
    {
        var subcategory = await _subcategoryService.GetByIdAsync(id);

        if (subcategory == null) return NotFound();

        return Ok(subcategory);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SubcategoryResponseDTO>>> GetByCategoryId(Guid id)
    {
        var categories = await _subcategoryService.GetByCategoryIdAsync(id);

        return Ok(categories);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<SubcategoryResponseDTO>> Delete(Guid id)
    {
        var subcategory = _subcategoryService.GetByIdAsync(id);
        if (subcategory == null) return NotFound();

        await _subcategoryService.DeleteAsync(id);        

        return NoContent();
    }
}