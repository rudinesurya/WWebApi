using System.ComponentModel.DataAnnotations.Schema;

namespace WWebApi.Models
{
    [Table("WeatherData")]
    public class WeatherData
    {
        public Guid Id { get; set; }

        public DateTime DateTime { get; set; }

        public double Temperature { get; set; }

        public double Humidity { get; set; }

        // Navigation
        public Guid SensorId { get; set; }

        public virtual Sensor? Sensor { get; set; }
    }
}
