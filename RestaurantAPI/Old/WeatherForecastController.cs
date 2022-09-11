using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace RestaurantAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
      private readonly IWeatherForecastService _weatherForecastService;

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherForecastService service )
        {
            _logger = logger;
            _weatherForecastService = service;  
        }

  
        [HttpGet("CityForecast")]
        public ActionResult<IEnumerable<WeatherForecast>> Get([FromQuery]string country, [FromQuery] string city)
        {
            ActionResult returnResult = NotFound($"City {city} and country {country} not found");
            var result = _weatherForecastService.Get(country, city);
            if(result.Count() > 0)
            {
                returnResult = Ok(result);
            }

            return returnResult;
        }

        [HttpGet("AllForecast")]
        public ActionResult<IEnumerable<WeatherForecast>> AllForecast()
        {
            ActionResult returnResult = NotFound();
            var result = _weatherForecastService.Get();
            if (result.Count() > 0)
            {
                returnResult = Ok(result);
            }

            return returnResult;
        }


        [HttpPost("ChangeForecast")]
        public ActionResult<string> Hello([FromBody]string weatherForecast)
        {
            var newWeather = JsonSerializer.Deserialize<WeatherForecast>(weatherForecast.ToString());

            if (newWeather != null)
            {
                _weatherForecastService.SetForecast(newWeather);
                return Ok($"Thank you! forecast {newWeather?.ToString()}");
            }

            return NotFound( $"Sorry but we can't store your changes");
        }

    }
}