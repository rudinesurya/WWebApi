using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WWebApi.Models;

namespace WWebApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Sensor> Sensors { get; set; }

        public DbSet<WeatherData> WeatherData { get; set; }
    }
}
