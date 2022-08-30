using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WWebApi.Controllers;
using WWebApi.Models;
using WWebApi.Services;

namespace WWebApi.Test.ControllerTests
{
    public class SensorsControllerTest
    {
        [Fact]
        public async Task GetSensorsAsync_ReturnCollection()
        {
            // Arrange
            var service = new Mock<ISensorService>();
            var sensorList = new List<Sensor>() {
                new Sensor(){ 
                    Name = "1",
                    Country = "Country 1",
                    City = "City 1"
                },
                new Sensor(){
                    Name = "2",
                    Country = "Country 2",
                    City = "City 2"
                }
            };
            service.Setup(_ => _.GetSensors()).Returns(sensorList.AsQueryable());
            var sut = new SensorsController(null, service.Object);

            // Act
            var result = await sut.Get() as OkObjectResult;

            // Assert
            result.StatusCode.Should().Be(200);
            (result.Value as IList<Sensor>).Count.Should().Be(sensorList.Count());
        }

        [Fact]
        public async Task GetSensorsAsync_ReturnEmptyCollection()
        {
            // Arrange
            var service = new Mock<ISensorService>();
            var sensorList = new List<Sensor>();
            service.Setup(_ => _.GetSensors()).Returns(sensorList.AsQueryable());
            var sut = new SensorsController(null, service.Object);

            // Act
            var result = await sut.Get() as OkObjectResult;

            // Assert
            result.StatusCode.Should().Be(200);
            (result.Value as IList<Sensor>).Count.Should().Be(sensorList.Count());
        }

        [Fact]
        public async Task GetArbitrarySensorsAsync_ReturnCollection()
        {
            // Arrange
            var service = new Mock<ISensorService>();
            var sensorList = new List<Sensor>() {
                new Sensor(){
                    Name = "1",
                    Country = "Country 1",
                    City = "City 1"
                },
                new Sensor(){
                    Name = "2",
                    Country = "Country 2",
                    City = "City 2"
                }
            };
            service.Setup(_ => _.GetSensors()).Returns(sensorList.AsQueryable());
            var sut = new SensorsController(null, service.Object);

            // Act
            var result = await sut.Get(sensorNames: new string[] { "1" }) as OkObjectResult;

            // Assert
            result.StatusCode.Should().Be(200);
            (result.Value as IList<Sensor>).Count.Should().Be(1);
        }

        [Fact]
        public async Task GetNonExistingSensorsAsync_ReturnEmptyCollection()
        {
            // Arrange
            var service = new Mock<ISensorService>();
            var sensorList = new List<Sensor>() {
                new Sensor(){
                    Name = "1",
                    Country = "Country 1",
                    City = "City 1"
                }
            };
            service.Setup(_ => _.GetSensors()).Returns(sensorList.AsQueryable());
            var sut = new SensorsController(null, service.Object);

            // Act
            var result = await sut.Get(sensorNames: new string[] { "9" }) as OkObjectResult;

            // Assert
            result.StatusCode.Should().Be(200);
            (result.Value as IList<Sensor>).Count.Should().Be(0);
        }

        [Fact]
        public async Task AddSensorAsync_ReturnSuccess()
        {
            // Arrange
            var service = new Mock<ISensorService>();
            var sensorList = new List<Sensor>();
            var newSensor = new Sensor() {
                Name = "1",
                Country = "Country 1",
                City = "City 1"
            };
            service.Setup(_ => _.AddSensorAsync(newSensor)).ReturnsAsync(newSensor);
            var sut = new SensorsController(null, service.Object);

            // Act
            var result = await sut.Add(newSensor) as CreatedAtActionResult;

            // Assert
            result.StatusCode.Should().Be(201);
        }
    }
}
