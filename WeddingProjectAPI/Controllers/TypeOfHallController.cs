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
    public class TypeOfHallController : ControllerBase
    {
        private readonly ITypeOfHallSV _typeOfHall;

        public TypeOfHallController(ITypeOfHallSV typeOfHall)
        {
            _typeOfHall = typeOfHall;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByID(string id)
        {
            var obj = await _typeOfHall.GetByID(id);
            if (obj == null) return NotFound($"Type {id} does not exist");
            return Ok(obj);
        }

        [HttpGet("gen_id")]
        public async Task<IActionResult> GenerateID()
        {
            string genId = await _typeOfHall.GenerateID();
            return Ok(new { id = genId });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _typeOfHall.GetAll());

        [HttpPost("add")]
        public async Task<IActionResult> Add(TypeOfHall obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = ModelState.Values.First() });
            }
            switch (await _typeOfHall.Create(obj))
            {
                case Result.SUCCESS: return Ok(new { message = $"Add type {obj.ID} success!" });
                case Result.EXIST: return BadRequest(new { message = $"Type {obj.ID} was exist" });
                default: return BadRequest(new { message = "Server fail to create. Please try again" });
            }
        }

        [HttpPost("update")]
        public async Task<IActionResult> Edit(TypeOfHall obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = ModelState.Values.First() });
            }
            switch (await _typeOfHall.Update(obj))
            {
                case Result.SUCCESS: return Ok(new { message = $"Update type {obj.ID} success!" });
                case Result.NOTFOUND: return BadRequest(new { message = $"Type {obj.ID} was not exist" });
                default: return BadRequest(new { message = "Server fail to update. Please try again" });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(string id)
        {
            switch (await _typeOfHall.Delete(id))
            {
                case Result.SUCCESS: return Ok(new { message = $"Remove type {id} success!" });
                case Result.NOTFOUND: return BadRequest(new { message = $"Type {id} was not exist" });
                case Result.NOTREMOVE: return BadRequest(new { message = $"Can not remove type {id}. Please check this" });
                default: return BadRequest(new { message = "Server fail to delete. Please try again" });
            }
        }
    }
}
