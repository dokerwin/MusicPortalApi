using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Exceptions;
using RestaurantAPI.Models;

namespace RestaurantAPI.Services
{
    public class RestaurantService : IResturantService
    {
        private readonly RestaurantDbContextcs _restaurantDb;
        private readonly IMapper _mapper;
        private readonly ILogger<RestaurantService> _logger;

        public RestaurantService(RestaurantDbContextcs restaurantDbContextcs, IMapper mapper, ILogger<RestaurantService> logger)
        {
            _restaurantDb = restaurantDbContextcs;
            _mapper = mapper;
            _logger = logger;
        }

        public IEnumerable<RestaurantDto> GetAllRestaurants()
        {
            var restaurant = _restaurantDb.Restaurants
               .Include(r => r.Address)
               .Include(r => r.Dishes)
               .ToList();

            return _mapper.Map<List<RestaurantDto>>(restaurant);
        }

        public IEnumerable<RestaurantDto> GetByCategory(string name)
        {
            var restaurant = _restaurantDb.Restaurants
                .Include(r => r.Address)
                .Include(r => r.Dishes)
                .Where(c => c.Category == name).ToList();

            return _mapper.Map<List<RestaurantDto>>(restaurant);
        }

        public RestaurantDto GetById(int id)
        {
            var restaurant = _restaurantDb.Restaurants.Include(r => r.Address).Include(r => r.Dishes).FirstOrDefault(i => i.Id == id);
            if(restaurant is null)
            {
                throw new NotFoundException("Restaurant not found");
            }

            return _mapper.Map<RestaurantDto>(restaurant);
        }

        public RestaurantDto GetByName(string name)
        {
            return _mapper.Map<RestaurantDto>(_restaurantDb.Restaurants.Where(i => i.Name == name).ToList());
        }

        public int CreateRestaurant(ICreateRestaurantDto restaurantDto)
        {
            _logger.LogError("Zooopa");
            var restaurant = _mapper.Map<Restaurant>(restaurantDto);

            if (restaurant is not null)
            {
                _restaurantDb.Add(restaurant);
                _restaurantDb.SaveChanges();
            }
            return restaurant.Id;
        }

        public void DeleteRestaurant(int restaurantId)
        {
            var restaurantToDelete = _restaurantDb.Restaurants.FirstOrDefault(p => p.Id == restaurantId);
            if(restaurantToDelete is null)
            {
                throw new NotFoundException("Restaurant not found");
            }
            _restaurantDb.Restaurants.Remove(restaurantToDelete);
            _restaurantDb.SaveChanges();
        }

        public void UpdateRestaurant(int id, UpdateRestaurantDto restaurantDto)
        {
            var restaurantToUpdate = _restaurantDb.Restaurants.FirstOrDefault(p => p.Id == id);
            if (restaurantToUpdate is null)
            {
                throw new NotFoundException("Restaurant not found");
            }
            restaurantToUpdate.Name = restaurantDto.Name;
            restaurantToUpdate.Description = restaurantDto.Description;
            restaurantToUpdate.HasDelivery = restaurantDto.HasDelivery;

            _restaurantDb.Restaurants.Update(restaurantToUpdate);
            _restaurantDb.SaveChanges();
        }
    }
}
