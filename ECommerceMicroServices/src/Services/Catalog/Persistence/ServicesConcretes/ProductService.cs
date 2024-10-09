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

public class ProductService : IProductService
{
    private readonly IReadRepository<Product> _readRepository;
    private readonly IWriteRepository<Product> _writeRepository;
    private readonly IMapper _mapper;

    public ProductService(IReadRepository<Product> readRepository, IWriteRepository<Product> writeRepository, IMapper mapper)
    {
        _readRepository = readRepository;
        _writeRepository = writeRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductDTO>> GetAllProductsAsync()
    {
        var products = await _readRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<ProductDTO>>(products);
    }

    public async Task<ProductDTO> GetProductByIdAsync(int id)
    {
        var product = await _readRepository.GetByIdAsync(id);
        return _mapper.Map<ProductDTO>(product);
    }

    public async Task AddProductAsync(ProductDTO productDto)
    {
        var product = _mapper.Map<Product>(productDto);
        await _writeRepository.AddAsync(product);
    }

    public async Task UpdateProductAsync(ProductDTO productDto)
    {
        var product = _mapper.Map<Product>(productDto);
        await _writeRepository.UpdateAsync(product);
    }

    public async Task DeleteProductAsync(int id)
    {
        await _writeRepository.DeleteAsync(id);
    }
}
