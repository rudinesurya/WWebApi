using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using WWebApi.Models;
using WWebApi.Services;

namespace WWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
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

        [HttpGet(Name = "GetSensors")]
        public async Task<IActionResult> Get()
        {
            return Ok(SensorService.GetSensorsAsync());
        }

        [HttpGet("{key}", Name = "GetSensorById")]
        public async Task<IActionResult> GetSensorById([FromRoute] Guid key)
        {
            var sensor = SensorService.GetSensorByIdAsync(key);

            if (sensor == null)
            {
                return NotFound();
            }

            return Ok(sensor);
        }

        [HttpPost(Name = "AddSensor")]
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