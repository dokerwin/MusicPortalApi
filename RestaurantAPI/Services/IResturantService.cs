using RestaurantAPI.Models;

namespace RestaurantAPI.Services
{
    public interface IResturantService
    {
        public RestaurantDto GetById(int id);
        public RestaurantDto GetByName(string name);
        public IEnumerable<RestaurantDto> GetByCategory(string name);
        public IEnumerable<RestaurantDto> GetAllRestaurants();
        public int CreateRestaurant(ICreateRestaurantDto restaurant);
        public void DeleteRestaurant(int restaurantId);
        public void UpdateRestaurant(int id, UpdateRestaurantDto restaurantDto);
    }
}