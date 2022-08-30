using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WWebApi.Models;
using WWebApi.Services;

namespace WWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherDataController : ControllerBase
    {
        private readonly ILogger<WeatherDataController> Logger;
        private readonly ISensorService SensorService;
        private readonly IWeatherDataService WeatherDataService;

        public WeatherDataController(ILogger<WeatherDataController> logger,
                                    ISensorService sensorService,
                                    IWeatherDataService weatherDataService)
        {
            Logger = logger;
            SensorService = sensorService;
            WeatherDataService = weatherDataService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await WeatherDataService.GetWeatherData().ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] WeatherData weatherData)
        {
            if (weatherData.Id == default)
                weatherData.Id = Guid.NewGuid();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await WeatherDataService.AddWeatherDataAsync(weatherData);

            return CreatedAtAction(nameof(Get), new { id = weatherData.Id }, weatherData);
        }
    }
}