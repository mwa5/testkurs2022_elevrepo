using Dapper;
using System.Data.SqlClient;
using WebApi.Models;

namespace WebApi.Repositories;

public interface IWeatherForecastRepository
{
    Task<WeatherForecast> GetWeatherForecastAsync(DateTime date);
}

public class WeatherForecastRepository : IWeatherForecastRepository
{
    readonly WeatherForecastConfiguration _weatherForecastConfiguration;

    public WeatherForecastRepository(WeatherForecastConfiguration weatherForecastConfiguration)
    {
        _weatherForecastConfiguration = weatherForecastConfiguration;
    }

    async Task<SqlConnection> GetConnection()
    {
        var connection = new SqlConnection(_weatherForecastConfiguration.ConnectionString);

        await connection.OpenAsync();

        return connection;
    }

    public async Task<WeatherForecast> GetWeatherForecastAsync(DateTime date)
    {
        await using var connection = await GetConnection();

        return await connection.QueryFirstOrDefaultAsync<WeatherForecast>(
            "SELECT * FROM WeatherForecast WHERE Date = @date",
            new { date });
    }
}