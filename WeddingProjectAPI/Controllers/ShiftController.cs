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
    public class ShiftController : ControllerBase
    {
        private readonly IShiftSV _shift;

        public ShiftController(IShiftSV shift)
        {
            _shift = shift;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByID(string id)
        {
            var obj = await _shift.GetByID(id);
            if (obj == null) return NotFound($"Shift {id} does not exist");
            return Ok(obj);
        }

        [HttpGet("gen_id")]
        public async Task<IActionResult> GenerateID()
        {
            string genId = await _shift.GenerateID();
            return Ok(new { id = genId });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _shift.GetAll());

        [HttpPost("add")]
        public async Task<IActionResult> Add(Shift obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = ModelState.Values.First() });
            }
            switch (await _shift.Create(obj))
            {
                case Result.SUCCESS: return Ok(new { message = $"Add shift {obj.ID} success!" });
                case Result.EXIST: return BadRequest(new { message = $"Shift {obj.ID} was exist" });
                default: return BadRequest(new { message = "Server fail to create. Please try again" });
            }
        }

        [HttpPost("update")]
        public async Task<IActionResult> Edit(Shift obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = ModelState.Values.First() });
            }
            switch (await _shift.Update(obj))
            {
                case Result.SUCCESS: return Ok(new { message = $"Update shift {obj.ID} success!" });
                case Result.NOTFOUND: return BadRequest(new { message = $"Shift {obj.ID} was not exist" });
                default: return BadRequest(new { message = "Server fail to update. Please try again" });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(string id)
        {
            switch (await _shift.Delete(id))
            {
                case Result.SUCCESS: return Ok(new { message = $"Remove shift {id} success!" });
                case Result.NOTFOUND: return BadRequest(new { message = $"Shift {id} was not exist" });
                case Result.NOTREMOVE: return BadRequest(new { message = $"Can not remove shift {id}. Please check this" });
                default: return BadRequest(new { message = "Server fail to delete. Please try again" });
            }
        }
    }
}
