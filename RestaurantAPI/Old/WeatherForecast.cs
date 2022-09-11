using System.Text;

namespace RestaurantAPI
{
    public class WeatherForecast
    {
        public string City { get; set; }
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string? Summary { get; set; }

        public override string ToString()
        {
            return string.Format("Forecast for City {0}: Temperatire in Celciums is {1} ", City, TemperatureC);
        }
    }
}