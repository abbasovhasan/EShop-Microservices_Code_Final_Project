using Application.DTOs;
namespace Application.ServicesAbstractions;

public interface ICategoryService
{
    Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync();
    Task<CategoryDTO> GetCategoryByIdAsync(int id);
    Task AddCategoryAsync(CategoryDTO categoryDto);
    Task UpdateCategoryAsync(CategoryDTO categoryDto);
    Task DeleteCategoryAsync(int id);
}
