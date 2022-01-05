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
    public class FoodController : ControllerBase
    {
        private readonly IFoodSV _food;

        public FoodController(IFoodSV food)
        {
            _food = food;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByID(string id)
        {
            var obj = await _food.GetByID(id);
            if (obj == null) return NotFound($"Food {id} does not exist");
            return Ok(obj);
        }

        [HttpGet("gen_id")]
        public async Task<IActionResult> GenerateID()
        {
            string genId = await _food.GenerateID();
            return Ok(new { id = genId });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _food.GetAll());

        [HttpPost("add")]
        public async Task<IActionResult> Add(Food obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = ModelState.Values.First() });
            }
            switch (await _food.Create(obj))
            {
                case Result.SUCCESS: return Ok(new { message = $"Add food {obj.ID} success!" });
                case Result.EXIST: return BadRequest(new { message = $"Food {obj.ID} was exist" });
                default: return BadRequest(new { message = "Server fail to create. Please try again" });
            }
        }

        [HttpPost("update")]
        public async Task<IActionResult> Edit(Food obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = ModelState.Values.First() });
            }
            switch (await _food.Update(obj))
            {
                case Result.SUCCESS: return Ok(new { message = $"Update food {obj.ID} success!" });
                case Result.NOTFOUND: return BadRequest(new { message = $"Food {obj.ID} was not exist" });
                default: return BadRequest(new { message = "Server fail to update. Please try again" });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(string id)
        {
            switch (await _food.Delete(id))
            {
                case Result.SUCCESS: return Ok(new { message = $"Remove food {id} success!" });
                case Result.NOTFOUND: return BadRequest(new { message = $"Food {id} was not exist" });
                case Result.NOTREMOVE: return BadRequest(new { message = $"Can not remove food {id}. Please check this" });
                default: return BadRequest(new { message = "Server fail to delete. Please try again" });
            }
        }
    }
}
