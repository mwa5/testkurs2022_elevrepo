using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.Models;
using WebApi.Repositories;
using WebApi.Services;
using Xunit;
using FluentAssertions;
using FluentAssertions.Execution;

namespace WebApi.Tests;

public class WeatherForecastServiceTests
{
    #region Exempel 1 - Arrange/Act/Assert

    [Fact]
    public void When_forecast_data_fails()
    {
        //Arrange
        const string filePath = @"TestFiles\weather.data";

        //Act 
        var weatherForecast = new WeatherForecastService(null).GetForecast(filePath);

        //Assert
        Assert.NotNull(weatherForecast);
    }

    [Fact]
    public void When_GetForecast_throws_exception()
    {
        //Arrange
        string? filePath = null;

        //Act 
        var exception = Record.Exception(() => new WeatherForecastService(null).GetForecast(filePath));

        //Assert
        Assert.NotNull(exception);
    }

    #endregion

    #region Exempel 2 - Parameteriserade tester i xUnit

    [Fact]
    public void When_AddForecastData_sums_data_successfully()
    {
        //Arrange
        var weatherForecastData1 = new WeatherForecast { Data = 2 };
        var weatherForecastData2 = new WeatherForecast { Data = 3 };

        //Act 
        var result = new WeatherForecastService(null).AddForecastData(weatherForecastData1, weatherForecastData2);

        //Assert
        Assert.Equal(5, result);
    }


    [Theory]
    [InlineData(2, 3, 5)]
    [InlineData(1, 2, 3)]
    [InlineData(1, 1, 2)]
    public void When_AddForecastData_sums_data_successfully_inlineParam(int data1, int data2, int expected)
    {
        //Arrange
        var weatherForecastData1 = new WeatherForecast { Data = data1 };
        var weatherForecastData2 = new WeatherForecast { Data = data2 };

        //Act 
        var result = new WeatherForecastService(null).AddForecastData(weatherForecastData1, weatherForecastData2);

        //Assert
        Assert.Equal(expected, result);
    }


    [Theory]
    [ClassData(typeof(WeatherForecastData))]
    public void When_AddForecastData_sums_data_with_classdata(int data1, int data2, int expected)
    {
        //Arrange
        var weatherForecastData1 = new WeatherForecast { Data = data1 };
        var weatherForecastData2 = new WeatherForecast { Data = data2 };

        //Act 
        var result = new WeatherForecastService(null).AddForecastData(weatherForecastData1, weatherForecastData2);

        //Assert
        Assert.Equal(expected, result);
    }

    public class WeatherForecastData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { 2, 3, 5 };
            yield return new object[] { 1, 2, 3 };
            yield return new object[] { 1, 1, 2 };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    [Theory]
    [MemberData(nameof(GetWeatherForecastData), 3)]
    public void When_AddForecastData_sums_data_with_memberdata(int data1, int data2, int expected)
    {
        //Arrange
        var weatherForecastData1 = new WeatherForecast { Data = data1 };
        var weatherForecastData2 = new WeatherForecast { Data = data2 };

        //Act 
        var result = new WeatherForecastService(null).AddForecastData(weatherForecastData1, weatherForecastData2);

        //Assert
        Assert.Equal(expected, result);
    }

    public static IEnumerable<object[]> GetWeatherForecastData(int numTests)
    {
        var allData = new List<object[]>
        {
            new object[] { 2, 3, 5 },
            new object[] { 1, 2, 3 },
            new object[] { 1, 1, 2 }
        };

        return allData.Take(numTests);
    }

    #endregion

    #region Exempel 3 - Mockning/stubbning

    // En stub är bara en ersättning för ett beroende. Vi kan testa vår kod utan behöva bry oss om beroendet. Se Logger eller Configuration nedan.
    [Fact]
    public void When_WeatherForecastController_uses_stub_service()
    {
        //Arrange
        var loggerMock = new Mock<ILogger<WeatherForecastController>>();
        var configurationMock = new Mock<WeatherForecastConfiguration>();

        var weatherForecastServiceMock = new Mock<IWeatherForecastService>();
        weatherForecastServiceMock.Setup(x => x.GetForecast(It.IsAny<string>())).Returns(new WeatherForecast { Data = 100 });

        var weatherForcecastController = new WeatherForecastController(loggerMock.Object, weatherForecastServiceMock.Object, configurationMock.Object);

        //Act 
        IActionResult actionResult = weatherForcecastController.Get();
        var result = actionResult as OkObjectResult;
        var weatherForecastResult = result?.Value as WeatherForecast;

        //Assert
        Assert.Equal(100, weatherForecastResult.Data);
    }


    // Vi vill inte gå till databasen direkt (blir ett integrationstest, eller den här delen kanske inte är färdig men behöver ändå ge tillbaka ett värde - PoC)
    class FakeRepository : IWeatherForecastRepository
    {
        public Task<WeatherForecast> GetWeatherForecastAsync(DateTime date) => Task.FromResult(new WeatherForecast { Data = 100 });
    }

    [Fact]
    public async Task When_WeatherForecastService_uses_a_fake_repository()
    {
        //Arrange
        var weatherForecastService = new WeatherForecastService(new FakeRepository());

        //Act 
        var weatherForecast = await weatherForecastService.GetForecastHistoricallyAsync(DateTime.Now);

        //Assert
        Assert.NotNull(weatherForecast);
        Assert.Equal(100, weatherForecast.Data);
    }

    // Mock:a ett exception
    [Fact]
    public void When_WeatherForecastController_returns_internalservererror()
    {
        //Arrange
        var loggerMock = new Mock<ILogger<WeatherForecastController>>();
        var configurationMock = new Mock<WeatherForecastConfiguration>();

        var weatherForecastServiceMock = new Mock<IWeatherForecastService>();
        weatherForecastServiceMock.Setup(x => x.GetForecast(It.IsAny<string>())).Throws<ArgumentNullException>();

        var weatherForcecastController = new WeatherForecastController(loggerMock.Object, weatherForecastServiceMock.Object, configurationMock.Object);

        //Act 
        IActionResult actionResult = weatherForcecastController.Get();
        var result = actionResult as StatusCodeResult;

        //Assert
        Assert.NotNull(result);
        Assert.Equal(500, result.StatusCode);
    }

    // Mocka att metoden genererar ett Ok svar
    [Fact]
    public void When_WeatherForecastController_returns_Ok()
    {
        //Arrange
        var loggerMock = new Mock<ILogger<WeatherForecastController>>();
        var configurationMock = new Mock<WeatherForecastConfiguration>();

        var weatherForecastServiceMock = new Mock<IWeatherForecastService>();
        weatherForecastServiceMock.Setup(x => x.GetForecast(It.IsAny<string>())).Returns(new WeatherForecast());

        var weatherForcecastController = new WeatherForecastController(loggerMock.Object, weatherForecastServiceMock.Object, configurationMock.Object);

        //Act 
        IActionResult actionResult = weatherForcecastController.Get();
        var result = actionResult as OkObjectResult;

        //Assert
        weatherForecastServiceMock.Verify(w => w.GetForecast(It.IsAny<string>()), Times.Once);
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
    }

    #endregion

    #region Fluent Assertions

    [Fact]
    public void When_customMessages_is_displayed()
    {
        //Arrange
        var loggerMock = new Mock<ILogger<WeatherForecastController>>();
        var configurationMock = new Mock<WeatherForecastConfiguration>();

        var weatherForecastServiceMock = new Mock<IWeatherForecastService>();
        weatherForecastServiceMock.Setup(x => x.GetForecast(It.IsAny<string>())).Returns(new WeatherForecast { Data = 100 });

        var weatherForcecastController = new WeatherForecastController(loggerMock.Object, weatherForecastServiceMock.Object, configurationMock.Object);

        //Act 
        IActionResult actionResult = weatherForcecastController.Get();
        var result = actionResult as OkObjectResult;
        var weatherForecastResult = result?.Value as WeatherForecast; //if weatherForceastResult was sut it would be harder to read, naming convention is important

        //Assert
        //Assert.Equal(100, weatherForecastResult.Data);
        weatherForecastResult.Data.Should().Be(200, because: "the weather was sunny (100) and rainy (100)");
    }

    [Fact]
    public void When_fluentassertion_test_how_test_is_organized()
    {
        typeof(WeatherForecastService).
            Should()
            .Implement<IWeatherForecastService>();
    }

    [Fact]
    public void When_exception_is_caught_with_fluentAssertions()
    {
        //Arrange
        var weatherForecastService = new WeatherForecastService(new FakeRepository());

        //Act 
        Func<WeatherForecast> act = () => weatherForecastService.GetForecast(null);
        
        //Assert
        act.Should()
            .Throw<ArgumentNullException>()
            .WithMessage("*What are you doing!?*");
    }
    #endregion

    #region AssertionScope


    [Fact]
    public void AssertionScopes()
    {
        //Arrange
        var weatherForecastService = new WeatherForecastService(new FakeRepository());

        //Act 
        var data = weatherForecastService.GetForecasts();
        
        //Assert
        //using var scope = new AssertionScope();
        data.Should().NotBeNullOrEmpty();
        data.Should().OnlyHaveUniqueItems();
        data.Should().HaveCount(4);
        data.Should().BeInAscendingOrder(x => x.Data);



        //data.Should().NotBeNullOrEmpty().And.OnlyHaveUniqueItems().And.HaveCount(4).And.BeInAscendingOrder(x => x.Data);
    }

    #endregion
}