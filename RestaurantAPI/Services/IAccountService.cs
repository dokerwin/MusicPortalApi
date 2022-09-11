using RestaurantAPI.Models;

namespace RestaurantAPI.Services
{
    public interface IAccountService
    {
        void RegisterUser(RegistereUserDto userDto);
        string GenerateJwt(LoginDto user);
    }
}