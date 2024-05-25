using global_exception_midleware.Model;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace global_exception_midleware.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController(ILogger<WeatherForecastController> logger) : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger = logger;

        private static readonly string[] Summaries =
        [
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        ];

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {

            _logger.LogInformation("========================");
            _logger.LogInformation("    Starting process    ");
            _logger.LogInformation("========================");

            _logger.LogInformation("Request: {Req}", JsonSerializer.Serialize(Summaries));

            _logger.LogInformation("========================");

            int.Parse(Summaries[0]); //forcing exception to see working middleware

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            }).ToArray();
        }
    }
}
