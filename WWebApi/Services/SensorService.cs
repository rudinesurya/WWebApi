using Microsoft.EntityFrameworkCore;
using System;
using WWebApi.Data;
using WWebApi.Models;

namespace WWebApi.Services
{
    public interface ISensorService
    {
        IQueryable<Sensor> GetSensorsAsync();

        IQueryable<Sensor> GetSensorByIdAsync(Guid id);

        Task<Sensor> AddSensorAsync(Sensor sensor);
    }

    public class SensorService : ISensorService
    {
        private readonly AppDbContext DbContext;

        public SensorService(AppDbContext context)
        {
            DbContext = context;
        }

        public IQueryable<Sensor> GetSensorsAsync() => DbContext.Sensors.Include(s => s.WeatherData);

        public IQueryable<Sensor> GetSensorByIdAsync(Guid key) => DbContext.Sensors.Where(s => s.Id == key)
            .Include(s => s.WeatherData);

        public Task<Sensor> AddSensorAsync(Sensor sensor)
        {
            throw new NotImplementedException();
        }
    }
}
