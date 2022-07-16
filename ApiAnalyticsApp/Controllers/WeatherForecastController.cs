using ApiAnalyticsApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiAnalyticsApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiVersion("1.0")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly INodeService nodeService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, INodeService nodeService)
        {
            _logger = logger;
            this.nodeService = nodeService;
        }

        [HttpGet]
        public async Task Get()
        {
            await nodeService.TestAsync();
        }
    }
}
