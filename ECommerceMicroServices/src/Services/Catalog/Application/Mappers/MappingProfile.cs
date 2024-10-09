using Application.DTOs;
using AutoMapper;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Product -> ProductDTO ve ters dönüşüm
        CreateMap<Product, ProductDTO>().ReverseMap();

        // Category -> CategoryDTO ve ters dönüşüm
        CreateMap<Category, CategoryDTO>().ReverseMap();
    }
}
