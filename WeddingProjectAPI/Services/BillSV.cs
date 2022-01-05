using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeddingProjectAPI.IServices;
using ResortProjectAPI.ModelEF;
using Microsoft.EntityFrameworkCore;
using ResortProjectAPI.Enum;

namespace WeddingProjectAPI.Services
{
    public class BillSV : IBillSV
    {
        private readonly WeddingDBContext db;

        public BillSV(WeddingDBContext db)
        {
            this.db = db;
        }

        public async Task<float> CalFee(string bookingId)
        {
            float price = await CalPrice(bookingId);
            var booking = await db.Bookings.FindAsync(bookingId);
            if (booking == null) return -1;
            float fee = (float)((price - booking.Deposit) * Math.Truncate(GetNow().Subtract(booking.Date).TotalDays));
            return fee;
        }

        public async Task<float> CalPrice(string bookingId)
        {
            var booking = await db.Bookings
                .Include(x => x.ListServices)
                .Include(x => x.Menus)
                .FirstOrDefaultAsync(x => x.ID == bookingId);
            if (booking == null) return -1;
            float price = booking.Price;
            foreach (var sv in booking.ListServices)
                price += sv.Amount * sv.Price;
            foreach (var food in booking.Menus)
                price += food.Amount * food.Price;
            return price;
        }

        public async Task<int> Create(Bill obj)
        {
            if (await GetByID(obj.ID) != null) return Result.EXIST;
            var booking = await db.Bookings.FindAsync(obj.BookingID);
            if (booking == null) return Result.NOTFOUND;
            try
            {
                db.Bills.Add(obj);
                await db.SaveChangesAsync();
                return Result.SUCCESS;
            }
            catch
            {
                return Result.FAIL;
            }
        }

        public async Task<int> Delete(string id)
        {
            var obj = await db.Bills.FindAsync(id);
            if (obj == null) return Result.NOTFOUND;
            if (Math.Truncate(GetNow().Subtract(obj.DateOfPayment).TotalDays) > 0) return Result.NOTREMOVE;
            try
            {
                db.Bills.Remove(obj);
                await db.SaveChangesAsync();
                return Result.SUCCESS;
            }
            catch
            {
                return Result.FAIL;
            }
        }

        public async Task<string> GenerateID()
        {
            if ((await GetAll()).Any() == false) return "BL000000";
            string id = "BL";
            string objID = db.Bills.Max(x => x.ID);
            int number = int.Parse(objID[2..objID.Length]) + 1;
            while ((id + number).Length < 8)
            {
                id += '0';
            }
            return id + number;
        }

        public async Task<IEnumerable<Bill>> GetAll()
        {
            return await db.Bills
                .Include(x => x.TypeOfPayment)
                .Include(x => x.Booking)
                .ToListAsync();
        }

        public async Task<Bill> GetByID(string id)
        {
            return await db.Bills
                .Include(x => x.TypeOfPayment)
                .Include(x => x.Booking)
                .FirstOrDefaultAsync(x => x.ID == id);
        }

        public DateTime GetNow()
        {
            return new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
        }
    }
}
