using Application.Dishes.Dtos;
using AutoMapper;
using Domain.models;

namespace Application.Dishes
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Create.Command, Dish>();
            CreateMap<Edit.Command, Dish>();
            CreateMap<Dish, DishDto>();
        }
    }
}