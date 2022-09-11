
namespace RestaurantAPI
{
    public interface IWeatherForecastService
    {
        IEnumerable<WeatherForecast> Get();
        IEnumerable<WeatherForecast> Get(string country, string city);
        void SetForecast(WeatherForecast newWeather);
        
    }
}