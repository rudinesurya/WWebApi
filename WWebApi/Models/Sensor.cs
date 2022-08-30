using System.Collections.ObjectModel;
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
                return WeatherData.Count() > 0 ? WeatherData.Average(w => w.Temperature) : 0;
            }
        }

        public double AverageHumidity
        {
            get
            {
                return WeatherData.Count() > 0 ? WeatherData.Average(w => w.Humidity) : 0;
            }
        }

        // Navigation
        public virtual IEnumerable<WeatherData> WeatherData { get; set; }

        public Sensor()
        {
            WeatherData = new Collection<WeatherData>();
        }
    }
}
