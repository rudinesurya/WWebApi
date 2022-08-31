using WWebApi.Models;

namespace WWebApi.Data
{
    public class AppDbContextSeed
    {
        public static void Seed(AppDbContext dbContext)
        {
            if (dbContext.Database.EnsureCreated())
            {
                // Seed Sensors
                var sensor1 = new Sensor()
                {
                    Name = "1",
                    Country = "Ireland",
                    City = "Dublin",
                    WeatherData = GenerateRandomWeatherData()
                };
                var sensor2 = new Sensor()
                {
                    Name = "2",
                    Country = "North America",
                    City = "Miami",
                    WeatherData = GenerateRandomWeatherData()
                };

                dbContext.Sensors.AddRange(new List<Sensor>() {
                    sensor1,
                    sensor2
                });

                dbContext.SaveChangesAsync();
            }
        }

        private static List<WeatherData> GenerateRandomWeatherData()
        {
            Random random = new Random();
            var result = new List<WeatherData>();
            for (int i = 0; i < 7; ++i)
            {
                var newWeatherData = new WeatherData()
                {
                    DateTime = DateTime.Now.AddDays(-i),
                    Temperature = random.Next(30, 40),
                    Humidity = random.Next(30, 60)
                };

                result.Add(newWeatherData);
            }

            return result;
        }
    }
}
