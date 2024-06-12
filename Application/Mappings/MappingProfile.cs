using Application.Features.Category.Command.Create;
using Application.Features.Category.Command.Delete;
using Application.Features.Category.Command.Update;
using Application.Features.Product.Command;
using Application.Features.SignUp;
using Application.Models;
using AutoMapper;
using Domain.DTO;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<SignUpModel, SignUpCommand>().ReverseMap();
            CreateMap<ApplicationUser, SignUpCommand>().ReverseMap();
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<CreateCategoryCommand, CategoryDto>().ReverseMap();
            CreateMap<UpdateCategoryCommand, CategoryDto>().ReverseMap();
            CreateMap<CreateProductCommand, ProductDto>().ReverseMap();
            CreateMap<UpdateProductCommand, ProductDto>().ReverseMap();

        }
    }

}
