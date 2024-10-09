using Application.DTOs;
using Application.ServicesAbstractions;
using Microsoft.AspNetCore.Mvc;

namespace CatalogAPI.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly ILogger<ProductController> _logger;

    public ProductController(IProductService productService, ILogger<ProductController> logger)
    {
        _productService = productService;
        _logger = logger;
    }

    [HttpGet("products")]
    public async Task<IActionResult> GetAllProducts()
    {
        try
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while getting all products: {ex.Message}");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("products/{id}")]
    public async Task<IActionResult> GetProductById(int id)
    {
        try
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                _logger.LogWarning($"Product with ID {id} not found.");
                return NotFound();
            }
            return Ok(product);
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while getting the product with ID {id}: {ex.Message}");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost("products")]
    public async Task<IActionResult> AddProduct([FromBody] ProductDTO productDto)
    {
        if (!ModelState.IsValid)
        {
            _logger.LogWarning("Invalid model state for the AddProduct request.");
            return BadRequest(ModelState);
        }

        try
        {
            await _productService.AddProductAsync(productDto);
            return CreatedAtAction(nameof(GetProductById), new { id = productDto.Id }, productDto);
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while adding a product: {ex.Message}");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPut("products/{id}")]
    public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductDTO productDto)
    {
        if (!ModelState.IsValid || id != productDto.Id)
        {
            _logger.LogWarning($"Invalid update attempt for product with ID {id}.");
            return BadRequest(ModelState);
        }

        try
        {
            await _productService.UpdateProductAsync(productDto);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while updating the product with ID {id}: {ex.Message}");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpDelete("products/{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        try
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                _logger.LogWarning($"Product with ID {id} not found.");
                return NotFound();
            }
            await _productService.DeleteProductAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while deleting the product with ID {id}: {ex.Message}");
            return StatusCode(500, "Internal server error");
        }
    }
}
