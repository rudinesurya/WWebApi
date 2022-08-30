using Microsoft.EntityFrameworkCore;
using System;
using WWebApi.Data;
using WWebApi.Models;

namespace WWebApi.Services
{
    public interface ISensorService
    {
        IQueryable<Sensor> GetSensors();

        Task<Sensor> AddSensorAsync(Sensor sensor);
    }

    public class SensorService : ISensorService
    {
        private readonly AppDbContext DbContext;

        public SensorService(AppDbContext context)
        {
            DbContext = context;
        }

        public IQueryable<Sensor> GetSensors() => DbContext.Sensors.Include(s => s.WeatherData);

        public async Task<Sensor> AddSensorAsync(Sensor sensor)
        {
            var duplicate = await DbContext.Sensors.FirstOrDefaultAsync(s => s.Name == sensor.Name);
            if (duplicate != null)
            {
                throw new ArgumentException();
            }

            await DbContext.Sensors.AddAsync(sensor);
            await DbContext.SaveChangesAsync();
            return sensor;
        }
    }
}
