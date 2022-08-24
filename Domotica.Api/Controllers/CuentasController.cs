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
    public class CuentasController : ControllerBase
    {
        readonly ICuentaData Repo;
        public CuentasController(ICuentaData repo)
        {
            Repo = repo;
        }
       
        [HttpGet("{id}")]
        public async Task<Cuenta> Get(Guid id)
        {
            return await Repo.Get(id);
        }

        // POST api/<CuentasController>
        [HttpPost]
        public async Task<ActionResult<Cuenta>> Create(Cuenta cuenta)
        {
            var validator = new CuentaValidator();
            ValidationResult res = validator.Validate(cuenta);
             await Repo.Post(cuenta);
            return null;
        }

        // PUT api/<CuentasController>/5
        [HttpPost]
        public async Task<Cuenta> Update(Cuenta cuenta)
        {
            return await Repo.Update(cuenta);
        }
       
    }
}
