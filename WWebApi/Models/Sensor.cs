using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WWebApi.Models
{
    [Table("Sensors")]
    public class Sensor
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        [Required]
        [MaxLength(20)]
        public string Country { get; set; }

        [Required]
        [MaxLength(20)]
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
