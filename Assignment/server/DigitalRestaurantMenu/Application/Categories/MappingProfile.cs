using Application.Categories.Dtos;
using AutoMapper;
using Domain.models;
using static Application.Categories.Create;

namespace Application.Categories
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Command, Category>();
            CreateMap<Category, CategoryDto>();
        }
    }
}