using Microsoft.EntityFrameworkCore;
using System;
using WWebApi.Data;
using WWebApi.Models;

namespace WWebApi.Services
{
    public interface IWeatherDataService
    {
        IQueryable<WeatherData> GetWeatherData();

        Task<WeatherData> AddWeatherDataAsync(WeatherData weatherData);
    }

    public class WeatherDataService : IWeatherDataService
    {
        private readonly AppDbContext DbContext;

        public WeatherDataService(AppDbContext context)
        {
            DbContext = context;
        }

        public IQueryable<WeatherData> GetWeatherData() => DbContext.WeatherData;

        public async Task<WeatherData> AddWeatherDataAsync(WeatherData weatherData)
        {
            var sensor = await DbContext.Sensors.FirstOrDefaultAsync(s => s.Id == weatherData.SensorId);
            if (sensor == null)
            {
                throw new ArgumentException();
            }

            await DbContext.WeatherData.AddAsync(weatherData);
            await DbContext.SaveChangesAsync();
            return weatherData;
        }
    }
}
