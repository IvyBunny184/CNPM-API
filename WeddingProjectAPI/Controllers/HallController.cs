using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResortProjectAPI.Enum;
using ResortProjectAPI.ModelEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeddingProjectAPI.IServices;

namespace WeddingProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HallController : ControllerBase
    {
        private readonly IHallSV _hall;

        public HallController(IHallSV hall)
        {
            _hall = hall;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByID(string id)
        {
            var obj = await _hall.GetByID(id);
            if (obj == null) return NotFound($"Hall {id} does not exist");
            return Ok(obj);
        }

        [HttpGet("gen_id")]
        public async Task<IActionResult> GenerateID()
        {
            string genId = await _hall.GenerateID();
            return Ok(new { id = genId });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _hall.GetAll());

        [HttpPost("add")]
        public async Task<IActionResult> Add(Hall obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = ModelState.Values.First() });
            }
            switch (await _hall.Create(obj))
            {
                case Result.SUCCESS: return Ok(new { message = $"Add hall {obj.ID} success!" });
                case Result.EXIST: return BadRequest(new { message = $"Hall {obj.ID} was exist" });
                case Result.IGNOREPRICE: return BadRequest(new { message = $"Price of hall must be greater than min price of type" });
                default: return BadRequest(new { message = "Server fail to create. Please try again" });
            }
        }

        [HttpPost("update")]
        public async Task<IActionResult> Edit(Hall obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = ModelState.Values.First() });
            }
            switch (await _hall.Update(obj))
            {
                case Result.SUCCESS: return Ok(new { message = $"Update hall {obj.ID} success!" });
                case Result.NOTFOUND: return BadRequest(new { message = $"Hall {obj.ID} was not exist" });
                case Result.IGNOREPRICE: return BadRequest(new { message = $"Price of hall must be greater than min price of type" });
                default: return BadRequest(new { message = "Server fail to update. Please try again" });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(string id)
        {
            switch (await _hall.Delete(id))
            {
                case Result.SUCCESS: return Ok(new { message = $"Remove hall {id} success!" });
                case Result.NOTFOUND: return BadRequest(new { message = $"Hall {id} was not exist" });
                case Result.NOTREMOVE: return BadRequest(new { message = $"Can not remove hall {id}. Please check this" });
                default: return BadRequest(new { message = "Server fail to delete. Please try again" });
            }
        }

        [HttpPost("img")]
        public async Task<IActionResult> AddImg(ImageOfHall obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = ModelState.Values.First() });
            }
            switch (await _hall.AddImg(obj))
            {
                case Result.SUCCESS: return Ok(new { message = $"Add image for hall {obj.HallID} success!" });
                case Result.NOTFOUND: return BadRequest(new { message = $"Hall {obj.HallID} was not exist" });
                case Result.EXIST: return BadRequest(new { message = $"Image was exist" });
                default: return BadRequest(new { message = "Server fail to update. Please try again" });
            }
        }

        [HttpDelete("img")]
        public async Task<IActionResult> RemoveImg(string url)
        {
            switch(await _hall.RemoveImg(url))
            {
                case Result.SUCCESS: return Ok(new { message = $"Remove image success!" });
                case Result.NOTFOUND: return BadRequest(new { message = $"Image was not exist" });
                default: return BadRequest(new { message = "Server fail to remove. Please try again" });
            }
        }
    }
}
