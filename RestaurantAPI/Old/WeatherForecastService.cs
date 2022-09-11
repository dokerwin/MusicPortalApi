namespace RestaurantAPI
{
    public class WeatherForecastService : IWeatherForecastService
    {

        List<WeatherForecast> _changedForecasts = new List<WeatherForecast>();

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly",
            "Cool", "Mild", "Warm", "Balmy",
            "Hot", "Sweltering", "Scorching"
        };

        readonly List<Country> _countries = new()
        {
            new Country()
            {
                Name = "Ukraine",
                Cities = new List<string> { "Kiev", "Cherskassy", "Uman" }
            },
            new Country()
            {
                Name = "Poland",
                Cities = new List<string> { "Warasaw", "Krakow", "Gdansk" }
            }
        };

        public IEnumerable<WeatherForecast> Get(string country, string city)
        {
            if (_countries.Where(c => c.Name == country && c.Cities.Contains(city)).ToList().Count > 0)
            {
                return new List<WeatherForecast>()
                {
                    new WeatherForecast()
                    {
                        City = city,
                        TemperatureC = new Random().Next(1, 40),
                        Date = DateTime.Now,
                        Summary = Summaries[new Random().Next(Summaries.Length)]
                    }
                };
            }
            return new List<WeatherForecast>();
        }

        public void SetForecast(WeatherForecast newWeather)
        {
           _changedForecasts.Add(newWeather);
        }

        public IEnumerable<WeatherForecast> Get()
        {
            var result = new List<WeatherForecast>();

            foreach (var country in _countries)
            {
                foreach (var city in country.Cities)
                {
                    result.Add(new WeatherForecast()
                    {
                        City = city,
                        TemperatureC = new Random().Next(1, 40),
                        Date = DateTime.Now,
                        Summary = Summaries[new Random().Next(Summaries.Length)]
                    });
                }
            }

            return result;
        }
    }
}
