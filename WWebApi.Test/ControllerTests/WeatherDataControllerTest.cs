using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WWebApi.Controllers;
using WWebApi.Models;
using WWebApi.Services;

namespace WWebApi.Test.ControllerTests
{
    public class WeatherDataControllerTest
    {
        [Fact]
        public async Task GetWeatherDataAsync_ReturnCollection()
        {
            // Arrange
            var service = new Mock<IWeatherDataService>();
            Random random = new Random();
            var weatherDataList = new List<WeatherData>() {
                new WeatherData(){
                    DateTime = DateTime.Now,
                    Temperature = random.Next(0, 40),
                    Humidity = random.Next(30, 60),
                    SensorId = Guid.NewGuid()
                },
                new WeatherData(){
                    DateTime = DateTime.Now.AddDays(-1),
                    Temperature = random.Next(0, 40),
                    Humidity = random.Next(30, 60),
                    SensorId = Guid.NewGuid()
                }
            };
            service.Setup(_ => _.GetWeatherData()).Returns(weatherDataList.AsQueryable());
            var sut = new WeatherDataController(null, service.Object);

            // Act
            var result = await sut.Get() as OkObjectResult;

            // Assert
            result.StatusCode.Should().Be(200);
            (result.Value as IList<WeatherData>).Count.Should().Be(weatherDataList.Count());
        }

        [Fact]
        public async Task GetWeatherDataAsync_ReturnEmptyCollection()
        {
            // Arrange
            var service = new Mock<IWeatherDataService>();
            var weatherDataList = new List<WeatherData>();
            service.Setup(_ => _.GetWeatherData()).Returns(weatherDataList.AsQueryable());
            var sut = new WeatherDataController(null, service.Object);

            // Act
            var result = await sut.Get() as OkObjectResult;

            // Assert
            result.StatusCode.Should().Be(200);
            (result.Value as IList<WeatherData>).Count.Should().Be(weatherDataList.Count());
        }

        [Fact]
        public async Task AddWeatherDataAsync_ReturnSuccess()
        {
            // Arrange
            var service = new Mock<IWeatherDataService>();
            var weatherDataList = new List<WeatherData>();
            Random random = new Random();
            var newWeatherData = new WeatherData()
            {
                DateTime = DateTime.Now,
                Temperature = random.Next(0, 40),
                Humidity = random.Next(30, 60),
                SensorId = Guid.NewGuid()
            };
            service.Setup(_ => _.AddWeatherDataAsync(newWeatherData)).ReturnsAsync(newWeatherData);
            var sut = new WeatherDataController(null, service.Object);

            // Act
            var result = await sut.Add(newWeatherData) as CreatedAtActionResult;

            // Assert
            result.StatusCode.Should().Be(201);
        }
    }
}
