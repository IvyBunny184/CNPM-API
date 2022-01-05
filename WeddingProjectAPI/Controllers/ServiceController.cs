using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResortProjectAPI.Enum;
using ResortProjectAPI.IServices;
using ResortProjectAPI.ModelEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeddingProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceSV _service;

        public ServiceController(IServiceSV service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByID(string id)
        {
            var obj = await _service.GetByID(id);
            if (obj == null) return NotFound($"Type {id} does not exist");
            return Ok(obj);
        }

        [HttpGet("gen_id")]
        public async Task<IActionResult> GenerateID()
        {
            string genId = await _service.GenerateID();
            return Ok(new { id = genId });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAll());

        [HttpPost("add")]
        public async Task<IActionResult> Add(Service obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = ModelState.Values.First() });
            }
            switch (await _service.Create(obj))
            {
                case Result.SUCCESS: return Ok(new { message = $"Add service {obj.ID} success!" });
                case Result.EXIST: return BadRequest(new { message = $"Service {obj.ID} was exist" });
                default: return BadRequest(new { message = "Server fail to create. Please try again" });
            }
        }

        [HttpPost("update")]
        public async Task<IActionResult> Edit(Service obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = ModelState.Values.First() });
            }
            switch (await _service.Update(obj))
            {
                case Result.SUCCESS: return Ok(new { message = $"Update service {obj.ID} success!" });
                case Result.NOTFOUND: return BadRequest(new { message = $"Service {obj.ID} was not exist" });
                default: return BadRequest(new { message = "Server fail to update. Please try again" });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(string id)
        {
            switch (await _service.Delete(id))
            {
                case Result.SUCCESS: return Ok(new { message = $"Remove service {id} success!" });
                case Result.NOTFOUND: return BadRequest(new { message = $"Service {id} was not exist" });
                case Result.NOTREMOVE: return BadRequest(new { message = $"Can not remove service {id}. Please check this" });
                default: return BadRequest(new { message = "Server fail to delete. Please try again" });
            }
        }
    }
}
