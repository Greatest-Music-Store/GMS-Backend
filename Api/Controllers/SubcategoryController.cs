using Microsoft.AspNetCore.Mvc;
using GMS_Backend.DTOs.Categories;
using GMS_Backend.Application.Services;
using GMS_Backend.Api.Mappers;
namespace GMS_Backend.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SubcategoryController : ControllerBase
{
    private readonly SubcategoryService _subcategoryService;

    public SubcategoryController(SubcategoryService subcategoryService)
    {
        _subcategoryService = subcategoryService;
    }

    [HttpPost]
    public async Task<ActionResult<SubcategoryResponseDTO>> Create(
        [FromBody] SubcategoryCreationDTO dto)
    {
        var subcategory = await _subcategoryService.CreateAsync(CategoriesMapper.ToSubcategoryModel(dto));

        return CreatedAtAction(
            nameof(GetById),
            new { id = subcategory.Id },
            CategoriesMapper.ToSubcategoryDto(subcategory)
        );
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<SubcategoryResponseDTO>> GetById(Guid id)
    {
        var subcategory = await _subcategoryService.GetByIdAsync(id);

        if (subcategory == null) return NotFound();

        return Ok(CategoriesMapper.ToSubcategoryDto(subcategory));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SubcategoryResponseDTO>>> GetByCategoryId(Guid id)
    {
        var categories = await _subcategoryService.GetByCategoryIdAsync(id);

        return Ok(categories.Select(CategoriesMapper.ToSubcategoryDto));
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<SubcategoryResponseDTO>> Delete(Guid id)
    {
        var subcategory = await _subcategoryService.GetByIdAsync(id);
        if (subcategory == null) return NotFound();

        await _subcategoryService.DeleteAsync(id);        

        return NoContent();
    }
}