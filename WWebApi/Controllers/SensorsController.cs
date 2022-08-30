using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WWebApi.Models;
using WWebApi.Services;

namespace WWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SensorsController : ControllerBase
    {
        private readonly ILogger<SensorsController> Logger;
        private readonly ISensorService SensorService;

        public SensorsController(ILogger<SensorsController> logger,
                                    ISensorService sensorService)
        {
            Logger = logger;
            SensorService = sensorService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] IEnumerable<string>? sensorNames = null, 
                                                [FromQuery] DateTime? startDate = null, 
                                                [FromQuery] DateTime? endDate = null)
        {
            var sensors = SensorService.GetSensors();
            
            // Set the query to point to specific sensors. Default will get all sensors.
            if (sensorNames != null && sensorNames.Count() > 0)
            {
                sensors = sensors.Where(s => sensorNames.Contains(s.Name));
            }

            // Check if both the startDate and endDate have been set.
            // Default will return the latest data
            if (startDate != null && endDate != null)
            {
                sensors = sensors.Select(s => new Sensor()
                {
                    Id = s.Id,
                    Name = s.Name,
                    Country = s.Country,
                    City = s.City,
                    WeatherData = s.WeatherData.Where(wd => wd.DateTime >= startDate! && wd.DateTime <= endDate!)
                });
            }
            else
            {
                sensors = sensors.Select(s => new Sensor()
                {
                    Id = s.Id,
                    Name = s.Name,
                    Country = s.Country,
                    City = s.City,
                    WeatherData = s.WeatherData.OrderByDescending(wd => wd.DateTime).Take(1)
                });
            }

            return Ok(sensors.ToList());
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Sensor sensor)
        {
            if (sensor.Id == default)
                sensor.Id = Guid.NewGuid();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await SensorService.AddSensorAsync(sensor);

            return CreatedAtAction(nameof(Get), new { id = sensor.Id }, sensor);
        }
    }
}