using Microsoft.AspNetCore.Mvc;

namespace SampleJson.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private List<string> Summaries = new()
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("getAll")]
        public IEnumerable<WeatherForecast> GetAll()
        {
            return GetWeatherForecastList();
        }

        [HttpGet]
        [Route("get")]
        public WeatherForecast Get(int id)
        {
            var response = new WeatherForecast();
            response.Summary = Summaries[id];

            return response;
        }

        [HttpPost]
        [Route("create")]
        public IEnumerable<WeatherForecast> Create([FromBody] WeatherForecast data)
        {
            Summaries.Add(data.Summary);

            return GetWeatherForecastList();
        }

        [HttpPut]
        [Route("update")]
        public IEnumerable<WeatherForecast> Update(int id, [FromBody] WeatherForecast data)
        {
            Summaries[id] = data.Summary;

            return GetWeatherForecastList();
        }

        [HttpDelete]
        [Route("delete")]
        public IEnumerable<WeatherForecast> Delete(int id)
        {
            Summaries.Remove(Summaries[id]);

            return GetWeatherForecastList();
        }

        internal IEnumerable<WeatherForecast> GetWeatherForecastList()
        {
            List<WeatherForecast> weatherForecasts = new List<WeatherForecast>();
            Random random = new Random();
            for (int i = 0; i < Summaries.Count; i++)
            {
                WeatherForecast weatherForecast = new WeatherForecast();
                weatherForecast.Date = DateTime.Now.AddDays(i);
                weatherForecast.TemperatureC = random.Next(-30, 40);
                weatherForecast.Summary = Summaries[i];

                weatherForecasts.Add(weatherForecast);
            }

            return weatherForecasts;
        }
    }
}