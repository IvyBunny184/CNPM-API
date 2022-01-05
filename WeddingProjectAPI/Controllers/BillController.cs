using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeddingProjectAPI.IServices;
using ResortProjectAPI.ModelEF;
using ResortProjectAPI.Enum;

namespace WeddingProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillController : ControllerBase
    {
        private readonly IBillSV _bill;

        public BillController(IBillSV bill)
        {
            _bill = bill;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByID(string id)
        {
            var obj = await _bill.GetByID(id);
            if (obj == null) return NotFound($"Bill {id} does not exist");
            return Ok(obj);
        }

        [HttpGet("gen_id")]
        public async Task<IActionResult> GenerateID()
        {
            string genId = await _bill.GenerateID();
            return Ok(new { id = genId });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _bill.GetAll());

        [HttpPost]
        public async Task<IActionResult> Add(Bill obj)
        {
            obj.DateOfPayment = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            obj.Fee = await _bill.CalFee(obj.BookingID);
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = ModelState.Values.First() });
            }
            switch(await _bill.Create(obj))
            {
                case Result.SUCCESS: return Ok(new { message = $"Add bill {obj.ID} success!" });
                case Result.EXIST: return BadRequest(new { message = $"Bill {obj.ID} was exist" });
                case Result.NOTFOUND: return BadRequest(new { message = $"Can not find booking {obj.BookingID} in bill" });
                default: return BadRequest(new { message = "Server fail to create. Please try again" });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Remove(string id)
        {
            switch(await _bill.Delete(id))
            {
                case Result.SUCCESS: return Ok(new { message = $"Remove bill {id} success!" });
                case Result.NOTFOUND: return BadRequest(new { message = $"Bill {id} was not exist" });
                case Result.NOTREMOVE: return BadRequest(new { message = $"Can not remove bill {id}. Please check this information" });
                default: return BadRequest(new { message = "Server fail to delete. Please try again" });
            }
        }

        [HttpGet("fee/{id}")]
        public async Task<IActionResult> CalFee(string id)
        {
            var fee = await _bill.CalFee(id);
            if (fee < 0) return NotFound(new { message = $"Booking {id} not exist" });
            return Ok(new { fee = fee });
        }

        [HttpGet("price/{id}")]
        public async Task<IActionResult> CalPrice(string id)
        {
            var price = await _bill.CalPrice(id);
            if (price < 0) return NotFound(new { message = $"Booking {id} not exist" });
            return Ok(new { price = price });
        }
    }
}
