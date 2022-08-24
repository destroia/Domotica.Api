using Data.Intarfaces;
using Microsoft.AspNetCore.Mvc;
using Models;
using ModelsNotDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Domotica.Api.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class DispositivosController : ControllerBase
    {
        readonly IDispositivoData Repo;
        public DispositivosController(IDispositivoData repo)
        {
            Repo = repo;
        }
        // GET: api/<DispositivosController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<DispositivosController>/5
        [HttpPost]
        public async Task<ActionResult<List<Dispositivo>>> GetMacs (List<MacAddress> macAddress)
        {
             var res = await Repo.FindAsync(macAddress);
            if (res.Count != 0)
            {
                return Ok(new { StatusCode = 200, msg = "Surccess", res = res });
            }
            return Ok(new { StatusCode = 202, msg = "NotFound" });
        }

        // POST api/<DispositivosController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<DispositivosController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<DispositivosController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
