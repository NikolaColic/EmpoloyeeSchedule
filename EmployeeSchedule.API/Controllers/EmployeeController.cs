using EmployeeSchedule.Data.Entities;
using EmployeeSchedule.Data.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeeSchedule.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IGenericService<Employee> _service;
        public EmployeeController(IGenericService<Employee> service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAll()
        {
            var result = await _service.GetAll();

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> Get(int id)
        {
            var result = await _service.GetById(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<bool>> Post([FromBody] Employee entity)
        {
            var result = await _service.Insert(entity);

            if (!result)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> Put(int id, [FromBody] Employee entity)
        {
            entity.Id = id;
            var result = await _service.Update(entity);

            if (!result)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var result = await _service.Delete(id);

            if (!result)
            {
                return BadRequest();
            }

            return NoContent();
        }
    }
}
