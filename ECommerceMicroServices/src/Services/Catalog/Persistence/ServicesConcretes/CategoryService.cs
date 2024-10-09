using Application.DTOs;
using Application.ServicesAbstractions;
using AutoMapper;
using Domain.Models;
using Domain.RepositoriesAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.ServicesConcretes;

public class CategoryService : ICategoryService
{
    private readonly IReadRepository<Category> _readRepository;
    private readonly IWriteRepository<Category> _writeRepository;
    private readonly IMapper _mapper;

    public CategoryService(IReadRepository<Category> readRepository, IWriteRepository<Category> writeRepository, IMapper mapper)
    {
        _readRepository = readRepository;
        _writeRepository = writeRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync()
    {
        var categories = await _readRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<CategoryDTO>>(categories);
    }

    public async Task<CategoryDTO> GetCategoryByIdAsync(int id)
    {
        var category = await _readRepository.GetByIdAsync(id);
        return _mapper.Map<CategoryDTO>(category);
    }

    public async Task AddCategoryAsync(CategoryDTO categoryDto)
    {
        var category = _mapper.Map<Category>(categoryDto);
        await _writeRepository.AddAsync(category);
    }

    public async Task UpdateCategoryAsync(CategoryDTO categoryDto)
    {
        var category = _mapper.Map<Category>(categoryDto);
        await _writeRepository.UpdateAsync(category);
    }

    public async Task DeleteCategoryAsync(int id)
    {
        await _writeRepository.DeleteAsync(id);
    }
}
