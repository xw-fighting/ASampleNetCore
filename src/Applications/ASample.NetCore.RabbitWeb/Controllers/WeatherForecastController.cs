using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASample.NetCore.RabbitMq.Publish;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ASample.NetCore.RabbitWeb.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class WeatherForecastController : ControllerBase
    {
        public IASampleRabbitMqClient ASampleRabbitMqClient { get; }
        public Action<string> action;

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IASampleRabbitMqClient aSampleRabbitMqClient)
        {
            _logger = logger;
            ASampleRabbitMqClient = aSampleRabbitMqClient;
        }

        [HttpGet("send")]
        public void Send()
        {
            ASampleRabbitMqClient.Send("nihao");
        }

        [HttpGet("receive")]
        public void Receive()
        {
            action += ReceiveMsg;
            ASampleRabbitMqClient.Receive(action);
        }

        private void ReceiveMsg(string msg)
        {
            
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
