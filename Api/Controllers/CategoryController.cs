using Microsoft.AspNetCore.Mvc;
using GMS_Backend.DTOs.Categories;
using GMS_Backend.Application.Services;
using GMS_Backend.Mappers;

namespace GMS_Backend.Api.Controllers;


[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly CategoryService _categoryService;

    public CategoryController(CategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpPost]
    public async Task<ActionResult<CategoryResponseDTO>> Create(
        [FromBody] CategoryCreationDTO dto)
    {
        var category = await _categoryService.CreateAsync(CategoriesMapper.ToModel(dto));

        return CreatedAtAction(
            nameof(GetById),
            new { id = category.Id },
            category
        );
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CategoryResponseDTO>> GetById(Guid id)
    {
        var category = await _categoryService.GetByIdAsync(id);

        if (category == null) return NotFound();

        return Ok(category);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryResponseDTO>>> GetAll()
    {
        var categories = await _categoryService.GetAllAsync();

        return Ok(categories);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<CategoryResponseDTO>> Delete(Guid id)
    {
        var category = _categoryService.GetByIdAsync(id);
        if (category == null) return NotFound();

        await _categoryService.DeleteAsync(id);        

        return NoContent();
    }
}