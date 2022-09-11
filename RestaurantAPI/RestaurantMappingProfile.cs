using AutoMapper;
using RestaurantAPI.Models;
using RestaurantAPI.RestaurantRoot.Entities;

namespace RestaurantAPI
{
    public class RestaurantMappingProfile : Profile
    {
        public RestaurantMappingProfile()
        {
            CreateMap<Restaurant, RestaurantDto>()
                .ForMember(m => m.City, c => c.MapFrom(s => s.Address.City))
                .ForMember(m => m.Street, c => c.MapFrom(s => s.Address.Street))
                .ForMember(m => m.PostalCode, c => c.MapFrom(s => s.Address.PostalCode));

            CreateMap<Dish, DishDto>();

            CreateMap<CreateRestaurantDto, Restaurant>()
                .ForMember(m => m.Address, c => c.MapFrom(s => new Address()
                { City = s.City, Street = s.Street, PostalCode = s.PostalCode }));

            CreateMap<UpdateRestaurantDto, Restaurant>();

            CreateMap<RegistereUserDto, User>();

        }
    }
}
