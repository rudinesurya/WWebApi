using System.ComponentModel.DataAnnotations.Schema;

namespace WWebApi.Models
{
    [Table("Sensors")]
    public class Sensor
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public double AverageTemperature
        {
            get
            {
                return WeatherData != null ? WeatherData.Average(w => w.Temperature) : 0;
            }
        }

        public double AverageHumidity
        {
            get
            {
                return WeatherData != null ? WeatherData.Average(w => w.Humidity) : 0;
            }
        }

        // Navigation
        public List<WeatherData>? WeatherData { get; set; }
    }
}
