using Microsoft.AspNetCore.Mvc;
using GMS_Backend.DTOs.Product;
using GMS_Backend.Application.Services;
using GMS_Backend.Mappers;
using GMS_Backend.Domain.Filters;
namespace GMS_Backend.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly ProductService _productService;

    public ProductController(ProductService productService)
    {
        _productService = productService;
    }

    [HttpPost]
    public async Task<ActionResult<ProductResponseDTO>> Create(
        [FromBody] ProductCreationDTO dto)
    {
        var product = await _productService.CreateAsync(ProductMapper.ToModel(dto));

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
    public async Task<ActionResult<IEnumerable<ProductResponseDTO>>> GetAll([FromQuery] ProductFilter filter)
    {
        var products = await _productService.GetAllAsync(filter);

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