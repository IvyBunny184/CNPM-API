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
    public class BookingController : ControllerBase
    {
        private readonly IBookingSV _booking;

        public BookingController(IBookingSV booking)
        {
            _booking = booking;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByID(string id)
        {
            var obj = await _booking.GetByID(id);
            if (obj == null) return NotFound($"Booking {id} does not exist");
            return Ok(obj);
        }

        [HttpGet("gen_id")]
        public async Task<IActionResult> GenerateID()
        {
            string genId = await _booking.GenerateID();
            return Ok(new { id = genId });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _booking.GetAll());

        [HttpPost("add")]
        public async Task<IActionResult> Add(Booking obj)
        {
            obj.Date = obj.Date.AddHours(7);
            obj.Date = new DateTime(obj.Date.Year, obj.Date.Month, obj.Date.Day);
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = ModelState.Values.First() });
            }
            switch (await _booking.Create(obj))
            {
                case Result.SUCCESS: return Ok(new { message = $"Add booking {obj.ID} success!" });
                case Result.EXIST: return BadRequest(new { message = $"Booking {obj.ID} was exist" });
                case Result.NOTFOUNDHALL: return NotFound(new { message = $"Hall {obj.HallID} was not exist" });
                case Result.NOTBOOKING: return BadRequest(new { message = $"Can not create, check information of booking" });
                default: return BadRequest(new { message = "Server fail to create. Please try again" });
            }
        }

        [HttpPost("update")]
        public async Task<IActionResult> Edit(Booking obj)
        {
            obj.Date = obj.Date.AddHours(7);
            obj.Date = new DateTime(obj.Date.Year, obj.Date.Month, obj.Date.Day);
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = ModelState.Values.First() });
            }
            switch (await _booking.Update(obj))
            {
                case Result.SUCCESS: return Ok(new { message = $"Update booking {obj.ID} success!" });
                case Result.NOTFOUND: return BadRequest(new { message = $"Booking {obj.ID} was not exist" });
                case Result.NOTFOUNDHALL: return NotFound(new { message = $"Hall {obj.HallID} was not exist" });
                case Result.NOTEDIT: return BadRequest(new { message = $"Booking {obj.ID} can not edit, check information of this" });
                default: return BadRequest(new { message = "Server fail to update. Please try again" });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(string id)
        {
            switch (await _booking.Delete(id))
            {
                case Result.SUCCESS: return Ok(new { message = $"Remove booking {id} success!" });
                case Result.NOTFOUND: return BadRequest(new { message = $"Booking {id} was not exist" });
                case Result.NOTREMOVE: return BadRequest(new { message = $"Can not remove booking {id}. Please check this" });
                default: return BadRequest(new { message = "Server fail to delete. Please try again" });
            }
        }

        [HttpPost("food")]
        public async Task<IActionResult> AddFood(Menu obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = ModelState.Values.First() });
            }
            switch(await _booking.AddFood(obj))
            {
                case Result.SUCCESS: return Ok(new { message = $"Add food {obj.FoodID} to booking {obj.BookingID} success!" });
                case Result.NOTFOUND: return BadRequest(new { message = $"Booking {obj.BookingID} was not exist" });
                case Result.NOTFOUNDFOOD: return BadRequest(new { message = $"Food {obj.FoodID} was not exist" });
                default: return BadRequest(new { message = "Server fail to add. Please try again" });
            }
        }

        [HttpPost("service")]
        public async Task<IActionResult> AddService(ListServices obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = ModelState.Values.First() });
            }
            switch (await _booking.AddService(obj))
            {
                case Result.SUCCESS: return Ok(new { message = $"Add service {obj.ServiceID} to booking {obj.BookingID} success!" });
                case Result.NOTFOUND: return BadRequest(new { message = $"Booking {obj.BookingID} was not exist" });
                case Result.NOTFOUNDFOOD: return BadRequest(new { message = $"Service {obj.ServiceID} was not exist" });
                default: return BadRequest(new { message = "Server fail to add. Please try again" });
            }
        }

        [HttpDelete("food")]
        public async Task<IActionResult> RemoveFood(Menu obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = ModelState.Values.First() });
            }
            switch (await _booking.RemoveFood(obj))
            {
                case Result.SUCCESS: return Ok(new { message = $"Remove food {obj.FoodID} from booking {obj.BookingID} success!" });
                case Result.NOTFOUND: return BadRequest(new { message = $"Booking {obj.BookingID} was not exist" });
                case Result.NOTFOUNDFOOD: return BadRequest(new { message = $"Food {obj.FoodID} was not exist" });
                default: return BadRequest(new { message = "Server fail to remove. Please try again" });
            }
        }

        [HttpDelete("service")]
        public async Task<IActionResult> RemoveService(ListServices obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = ModelState.Values.First() });
            }
            switch (await _booking.RemoveService(obj))
            {
                case Result.SUCCESS: return Ok(new { message = $"Remove service {obj.ServiceID} from booking {obj.BookingID} success!" });
                case Result.NOTFOUND: return BadRequest(new { message = $"Booking {obj.BookingID} was not exist" });
                case Result.NOTFOUNDFOOD: return BadRequest(new { message = $"Service {obj.ServiceID} was not exist" });
                default: return BadRequest(new { message = "Server fail to remove. Please try again" });
            }
        }
    }
}
