using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using WebApi.Models;
using WebApi.Repositories;

namespace WebApi.Services
{

    public interface IWeatherForecastService
    {
        WeatherForecast GetForecast(string? filePath);
        IEnumerable<WeatherForecast> GetForecasts();
        int AddForecastData(WeatherForecast data1, WeatherForecast data2);
        Task<WeatherForecast> GetForecastHistoricallyAsync(DateTime date);
    }

    public class WeatherForecastService : IWeatherForecastService
    {
        readonly IWeatherForecastRepository _repository;

        public WeatherForecastService(IWeatherForecastRepository repository) => _repository = repository;

        public WeatherForecast GetForecast(string? filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath)) throw new ArgumentNullException(nameof(filePath), "What are you doing!?");

            var weatherForecast = File.ReadAllText(filePath);

            return JsonSerializer.Deserialize<WeatherForecast>(weatherForecast) ?? throw new ArgumentNullException();
        }

        public IEnumerable<WeatherForecast> GetForecasts() => new List<WeatherForecast>
        {
            new WeatherForecast() { Data = 100, Date = new DateTime(2022, 10, 10), Summary = "A rainy day", TemperatureC = 14 },
            new WeatherForecast() { Data = 48, Date = new DateTime(2022, 08, 10), Summary = "A cloudy day", TemperatureC = 17 },
            new WeatherForecast() { Data = 100, Date = new DateTime(2022, 09, 10), Summary = "A sunny day", TemperatureC = 20 },
            new WeatherForecast() { Data = 99, Date = new DateTime(2022, 08, 10), Summary = "A rainy day", TemperatureC = 14 },
        }.OrderBy(x => x.Data)
            .ToList();

        public int AddForecastData(WeatherForecast data1, WeatherForecast data2) => AddData(data1, data2);

        public async Task<WeatherForecast> GetForecastHistoricallyAsync(DateTime date) => await _repository.GetWeatherForecastAsync(date);

        static int AddData(WeatherForecast data1, WeatherForecast data2) => data1.Data + data2.Data;
    }
}
