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
    public class StaffController : ControllerBase
    {
        private readonly IStaffSV _staff;
        public StaffController(IStaffSV staffSV)
        {
            _staff = staffSV;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByID(string id)
        {
            var obj = await _staff.GetByID(id);
            if (obj == null) return NotFound($"Staff {id} does not exist");
            return Ok(obj);
        }

        [HttpGet("gen_id")]
        public async Task<IActionResult> GenerateID()
        {
            string genId = await _staff.GenerateID();
            return Ok(new { id = genId });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _staff.GetAll());

        [HttpGet("role")]
        public async Task<IActionResult> GetAllRole() => Ok(await _staff.GetAllRole());

        [HttpPost("add")]
        public async Task<IActionResult> Add(Staff obj)
        {
            obj.Birth = obj.Birth.AddHours(7);
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = ModelState.Values.First() });
            }
            switch(await _staff.Create(obj))
            {
                case Result.SUCCESS: return Ok(new { message = $"Add staff {obj.ID} success!" });
                case Result.EXIST: return BadRequest(new { message = $"Staff {obj.ID} was exist" });
                default: return BadRequest(new { message = "Server fail to create. Please try again" });
            }
        }

        [HttpPost("update")]
        public async Task<IActionResult> Edit(Staff obj)
        {
            obj.Birth = obj.Birth.AddHours(7);
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = ModelState.Values.First() });
            }
            switch(await _staff.Update(obj))
            {
                case Result.SUCCESS: return Ok(new { message = $"Update staff {obj.ID} success!" });
                case Result.NOTFOUND: return BadRequest(new { message = $"Staff {obj.ID} was not exist" });
                default: return BadRequest(new { message = "Server fail to update. Please try again" });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(string id)
        {
            switch(await _staff.Delete(id))
            {
                case Result.SUCCESS: return Ok(new { message = $"Remove staff {id} success!" });
                case Result.NOTFOUND: return BadRequest(new { message = $"Staff {id} was not exist" });
                default: return BadRequest(new { message = "Server fail to delete. Please try again" });
            }
        }
    }
}
