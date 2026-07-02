using Microsoft.AspNetCore.Mvc;
using GMS_Backend.Application.Services;
using GMS_Backend.Domain.Filters;
using GMS_Backend.Api.Mappers;
using GMS_Backend.Api.DTOs.Product;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [EndpointDescription("Requer autenticação JWT ADMIN.")]
    public async Task<ActionResult<ProductResponseDTO>> Create(
        [FromBody] ProductCreationDTO dto)
    {
        var product = await _productService.CreateAsync(ProductMapper.ToModel(dto));
        
        return CreatedAtAction(
            nameof(GetById),
            new { id = product.ProductId },
            ProductMapper.ToDto(product)
        );
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ProductResponseDTO>> GetById(Guid id)
    {
        var product = await _productService.GetByIdAsync(id);

        if (product == null) return NotFound();

        return Ok(ProductMapper.ToDto(product));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductResponseDTO>>> GetAll([FromQuery] ProductFilter filter, [FromQuery] string? search)
    {
        var products = await _productService.GetAllAsync(filter, search);

        return Ok(products.Select(ProductMapper.ToDto));
    }

    [HttpPatch("{id:guid}")]
    [Authorize (Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [EndpointDescription("Requer autenticação JWT ADMIN")]
    public async Task<ActionResult<ProductResponseDTO>> Update([FromBody] ProductUpdateDTO dto, Guid id)
    {
        var product = await _productService.GetByIdAsync(id);
        if (product == null) return NotFound();

        ProductMapper.UpdateToModel(product, dto);

        await _productService.UpdateAsync(product);

        return Ok(ProductMapper.ToDto(product));
    }

    [HttpGet("offers")]
    public async Task<ActionResult<IEnumerable<ProductResponseDTO>>> GetOffers()
    {
        var products = await _productService.GetOffers();

        return Ok(products.Select(ProductMapper.ToDto));
    }

    [HttpGet("recommended")]
    public async Task<ActionResult<IEnumerable<ProductResponseDTO>>> GetRecommended()
    {
        var products = await _productService.GetRecommended();

        return Ok(products.Select(ProductMapper.ToDto));
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [EndpointDescription("Requer autenticação JWT ADMIN.")]
    public async Task<ActionResult<ProductResponseDTO>> Delete(Guid id)
    {
        var product = await _productService.GetByIdAsync(id);
        if (product == null) return NotFound();

        await _productService.DeleteAsync(id);        

        return NoContent();
    }
}