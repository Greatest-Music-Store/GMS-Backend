using Microsoft.AspNetCore.Mvc;
using GMS_Backend.DTOs.Product;
using GMS_Backend.Services.Interfaces;
namespace GMS_Backend.Controllers;


[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpPost]
    public async Task<ActionResult<ProductResponseDTO>> Create(
        [FromBody] ProductCreationDTO dto)
    {
        var product = await _productService.CreateAsync(dto);

        return CreatedAtAction(
            nameof(GetById),
            new { id = product.ProductId },
            product
        );
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ProductResponseDTO>> GetById(Guid id)
    {
        var product = await _productService.GetByIdAsync(id);

        if (product == null) return NotFound();

        return Ok(product);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductResponseDTO>>> GetAll(
        [FromQuery] string? name,
        [FromQuery] Guid? categoryId,
        [FromQuery] Guid? subCategoryId,
        [FromQuery] int? minPrice,
        [FromQuery] int? maxPrice,
        [FromQuery] string? sortBy,
        [FromQuery] string? brand
    )
    {
        var products = await _productService.GetAllAsync(
            name,
            categoryId,
            subCategoryId,
            minPrice,
            maxPrice,
            sortBy,
            brand
        );

        return Ok(products);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<ProductResponseDTO>> Delete(Guid id)
    {
        var product = _productService.GetByIdAsync(id);
        if (product == null) return NotFound();

        await _productService.DeleteAsync(id);        

        return NoContent();
    }
}