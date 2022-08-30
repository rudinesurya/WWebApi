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
    }
}
