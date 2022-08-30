using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WWebApi.Models
{
    [Table("WeatherData")]
    public class WeatherData
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        [Required]
        [Range(-100.0, 100.0)]
        public double Temperature { get; set; }

        [Required]
        [Range(30, 60)]
        public double Humidity { get; set; }

        // Navigation
        [Required]
        public Guid SensorId { get; set; }

        public virtual Sensor? Sensor { get; set; }
    }
}
