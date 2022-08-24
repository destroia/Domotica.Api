using Data.Intarfaces;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Validator;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Domotica.Api.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class LoginsController : ControllerBase
    {
        readonly ILoginData Repo;
        public LoginsController(ILoginData repo)
        {
            Repo = repo;
        }
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

       

        [HttpPost]
        public async Task<ActionResult<Cuenta>> Login(Login login)
        {
            var validator = new LoginValidator();
            ValidationResult resValid = validator.Validate(login);
            if (resValid.IsValid)
            {
                Cuenta res = await Repo.Login(login);
                if (res != null)
                {
                    return Ok(new { StatusCode = 200, msg = "login Success", res = res });
                }

                return Ok(new { StatusCode = 202, msg = "login error" ,res = res });
            }
            return Ok(new { resValid.Errors });
           
        }

        // PUT api/<LoginsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<LoginsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
