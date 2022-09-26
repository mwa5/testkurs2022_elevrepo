using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        readonly ILogger<WeatherForecastController> _logger;
        readonly IWeatherForecastService _weatherForecastService;
        readonly WeatherForecastConfiguration _weatherForecastConfiguration;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherForecastService weatherForecastService, WeatherForecastConfiguration weatherForecastConfiguration)
        {
            _logger = logger;
            _weatherForecastService = weatherForecastService;
            _weatherForecastConfiguration = weatherForecastConfiguration;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IActionResult Get()
        {
            try
            {
                var forecast = _weatherForecastService.GetForecast(_weatherForecastConfiguration.FilePath);

                return Ok(forecast);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to fetch weather forecast. Message: {ex.Message}");

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet(Name = "GetWeatherForecastHistorically")]
        public async Task<IActionResult> Get(DateTime date)
        {
            try
            {
                var forecast = await _weatherForecastService.GetForecastHistoricallyAsync(date);

                return Ok(forecast);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to fetch historically weather forecast. Message: {ex.Message}");

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

    }

}