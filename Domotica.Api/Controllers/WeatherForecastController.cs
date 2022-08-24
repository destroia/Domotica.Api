using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
using Mqtt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domotica.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[Action]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{id}")]
        public Cuenta Get(string id)
        {
            var d = new Cuenta()
            {
                Id = Guid.NewGuid(),
                Celular = "231231231",
                Email = "asdasdasdasdas",
                Pais = "Colombia",
                Password = "asdasd"
            };
            return d;

        }
        [HttpPost]
        public ActionResult<Cuenta> Post(Login login)
        {
            return Ok(new
            {
                StatusCode = 200,
                res = new Cuenta()
                {
                    Id = Guid.NewGuid(),
                    Celular = "231231231",
                    Email = "davidstiven123@gmail.com",
                    Pais = "Colombia",
                    Password = "12345678s"
                }
            });
        }
        [HttpGet]
        public async Task<object> Subscribe()
        {

            return  await MsgMqttNet.Subscribe("eia8Okjh7oMoC1m/LoginDevice");
            
        }
        [HttpGet]
        public async Task<object> UnSubscribe()
        {


            return await MsgMqttNet.UnsubscribeAsync(new List<string>() { "eia8Okjh7oMoC1m/Output" });

        }
        [HttpGet]
        public async Task<ActionResult> Publish()
        {
           

            int res = await MsgMqttNet.Publish("eia8Okjh7oMoC1m/LoginDevice", "d2");

            if (res == 1)
            {
                return Ok(new { StatusCode = 201, msg = "TopicVacio" });
            }
            if (res == 2)
            {
                return Ok(new { StatusCode = 202, msg = "NoHayConeccionAElSevidorMqtt" });
            }
            if (res == 3)
            {
                return Ok(new { StatusCode = 200, msg = "HaSidoEnviadoLaPublicacion" });
            }
            return BadRequest(new { StatusCode = 400, msg = "NoSeEnvioLaPublicacion" });
        }
    }
}
