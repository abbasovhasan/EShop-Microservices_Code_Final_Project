using Application.DTOs;
using Application.ServicesAbstractions;
using Microsoft.AspNetCore.Mvc;

namespace CatalogAPI.Controllers;
[ApiController]
[Route("api/[controller]")]
public class CatalogController : ControllerBase
{
    private readonly ICategoryService _categoryService;
    private readonly ILogger<CatalogController> _logger;

    public CatalogController(ICategoryService categoryService, ILogger<CatalogController> logger)
    {
        _categoryService = categoryService;
        _logger = logger;
    }

    [HttpGet("categories")]
    public async Task<IActionResult> GetAllCategories()
    {
        try
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while getting all categories: {ex.Message}");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("categories/{id}")]
    public async Task<IActionResult> GetCategoryById(int id)
    {
        try
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
            {
                _logger.LogWarning($"Category with ID {id} not found.");
                return NotFound();
            }
            return Ok(category);
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while getting the category with ID {id}: {ex.Message}");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost("categories")]
    public async Task<IActionResult> AddCategory([FromBody] CategoryDTO categoryDto)
    {
        if (!ModelState.IsValid)
        {
            _logger.LogWarning("Invalid model state for the AddCategory request.");
            return BadRequest(ModelState);
        }

        try
        {
            await _categoryService.AddCategoryAsync(categoryDto);
            return CreatedAtAction(nameof(GetCategoryById), new { id = categoryDto.Id }, categoryDto);
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while adding a category: {ex.Message}");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPut("categories/{id}")]
    public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryDTO categoryDto)
    {
        if (!ModelState.IsValid || id != categoryDto.Id)
        {
            _logger.LogWarning($"Invalid update attempt for category with ID {id}.");
            return BadRequest(ModelState);
        }

        try
        {
            await _categoryService.UpdateCategoryAsync(categoryDto);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while updating the category with ID {id}: {ex.Message}");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpDelete("categories/{id}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        try
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
            {
                _logger.LogWarning($"Category with ID {id} not found.");
                return NotFound();
            }
            await _categoryService.DeleteCategoryAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while deleting the category with ID {id}: {ex.Message}");
            return StatusCode(500, "Internal server error");
        }
    }
}