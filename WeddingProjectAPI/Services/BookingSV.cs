using Microsoft.EntityFrameworkCore;
using ResortProjectAPI.Enum;
using ResortProjectAPI.ModelEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeddingProjectAPI.IServices;

namespace WeddingProjectAPI.Services
{
    public class BookingSV: IBookingSV
    {
        private readonly WeddingDBContext db;

        public BookingSV(WeddingDBContext db)
        {
            this.db = db;
        }

        public async Task<int> AddFood(Menu enti)
        {
            var booking = await GetByID(enti.BookingID);
            if (booking == null) return Result.NOTFOUND;
            if (await db.Foods.FirstOrDefaultAsync(x => x.ID == enti.FoodID) == null) return Result.NOTFOUNDFOOD;
            try
            {
                var obj = await db.Menus.Where(x => x.BookingID == enti.BookingID && x.FoodID == enti.FoodID).SingleOrDefaultAsync();
                if(obj == null)
                {
                    db.Menus.Add(enti);
                    await db.SaveChangesAsync();
                    return Result.SUCCESS;
                }
                else
                {
                    obj.Amount += enti.Amount;
                    await db.SaveChangesAsync();
                    return Result.SUCCESS;
                }
            }
            catch
            {
                return Result.FAIL;
            }
        }

        public async Task<int> AddService(ListServices enti)
        {
            var booking = await GetByID(enti.BookingID);
            if (booking == null) return Result.NOTFOUND;
            if (await db.Services.FirstOrDefaultAsync(x => x.ID == enti.ServiceID) == null) return Result.NOTFOUNDFOOD;
            try
            {
                var obj = await db.ListServices.Where(x => x.BookingID == enti.BookingID && x.ServiceID == enti.ServiceID).SingleOrDefaultAsync();
                if (obj == null)
                {
                    db.ListServices.Add(enti);
                    await db.SaveChangesAsync();
                    return Result.SUCCESS;
                }
                else
                {
                    obj.Amount += enti.Amount;
                    await db.SaveChangesAsync();
                    return Result.SUCCESS;
                }
            }
            catch
            {
                return Result.FAIL;
            }
        }

        public async Task<int> Create(Booking enti)
        {
            if (await GetByID(enti.ID) != null) return Result.EXIST;
            if (await db.Halls.FindAsync(enti.HallID) == null) return Result.NOTFOUNDHALL;
            if (await CanBooking(enti) == false) return Result.NOTBOOKING;
            try
            {
                db.Bookings.Add(enti);
                await db.SaveChangesAsync();
                return Result.SUCCESS;
            }
            catch
            {
                return Result.FAIL;
            }
        }

        public async Task<int> Delete(string ID)
        {
            var obj = await GetByID(ID);
            if (obj == null) return Result.NOTFOUND;
            if (DateTime.Compare(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), new DateTime(obj.Date.Year, obj.Date.Month, obj.Date.Day)) >= 0)
                return Result.NOTREMOVE;
            try
            {
                foreach(var sv in obj.ListServices)
                {
                    db.ListServices.Remove(sv);
                }
                foreach(var food in obj.Menus)
                {
                    db.Menus.Remove(food);
                }
                db.Bookings.Remove(obj);
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
            if ((await GetAll()).Any() == false) return "BK000000";
            string id = "BK";
            string objID = db.Bookings.Max(x => x.ID);
            int number = int.Parse(objID[2..objID.Length]) + 1;
            while ((id + number).Length < 8)
            {
                id += '0';
            }
            return id + number;
        }

        public async Task<IEnumerable<Booking>> GetAll()
        {
            return await db.Bookings
                .Include(x => x.ListServices)
                .Include(x => x.Menus)
                .Include(x => x.Hall)
                .Include(x => x.Bill).ThenInclude(b => b.TypeOfPayment)
                .Include(x => x.Shift)
                .ToListAsync();
        }

        public async Task<Booking> GetByID(string ID)
        {
            return await db.Bookings
                .Include(x => x.ListServices)
                .Include(x => x.Menus)
                .Include(x => x.Hall)
                .Include(x => x.Bill).ThenInclude(b => b.TypeOfPayment)
                .Include(x => x.Shift)
                .FirstOrDefaultAsync(x => x.ID == ID);
        }

        public async Task<int> RemoveFood(Menu enti)
        {
            var booking = await GetByID(enti.BookingID);
            if (booking == null) return Result.NOTFOUND;
            var food = await db.Foods.FirstOrDefaultAsync(x => x.ID == enti.FoodID);
            if (food == null) return Result.NOTFOUNDFOOD;
            try
            {
                var obj = await db.Menus.Where(x => x.BookingID == enti.BookingID && x.FoodID == enti.FoodID).SingleOrDefaultAsync();
                obj.Amount -= enti.Amount;
                await db.SaveChangesAsync();
                return Result.SUCCESS;
            }
            catch
            {
                return Result.FAIL;
            }
        }

        public async Task<int> RemoveService(ListServices enti)
        {
            var booking = await GetByID(enti.BookingID);
            if (booking == null) return Result.NOTFOUND;
            var food = await db.Services.FirstOrDefaultAsync(x => x.ID == enti.ServiceID);
            if (food == null) return Result.NOTFOUNDSV;
            try
            {
                var obj = await db.ListServices.Where(x => x.BookingID == enti.BookingID && x.ServiceID == enti.ServiceID).SingleOrDefaultAsync();
                obj.Amount -= enti.Amount;
                await db.SaveChangesAsync();
                return Result.SUCCESS;
            }
            catch
            {
                return Result.FAIL;
            }
        }

        public async Task<int> Update(Booking enti)
        {
            var obj = await GetByID(enti.ID);
            if (obj == null) return Result.NOTFOUND;
            if (await db.Halls.FindAsync(enti.HallID) == null) return Result.NOTFOUNDHALL;
            if (DateTime.Compare(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), new DateTime(obj.Date.Year, obj.Date.Month, obj.Date.Day)) >= 0)
                return Result.NOTEDIT;
            if (await CanBooking(enti) == false) return Result.NOTEDIT;
            try
            {
                obj.HallID = enti.HallID;
                obj.GroomName = enti.GroomName;
                obj.BrideName = enti.BrideName;
                obj.Date = enti.Date;
                obj.ShiftID = enti.ShiftID;
                obj.Price = enti.Price;
                obj.Deposit = enti.Deposit;
                obj.IsCancel = enti.IsCancel;
                await db.SaveChangesAsync();
                return Result.SUCCESS;
            }
            catch
            {
                return Result.FAIL;
            }
        }

        private async Task<bool> CanBooking(Booking enti)
        {
            var obj = await db.Bookings
                .Where(x => !x.IsCancel &&
                (x.HallID == enti.HallID && x.ShiftID == enti.ShiftID
                && DateTime.Compare(new DateTime(x.Date.Year, x.Date.Month, x.Date.Day), new DateTime(enti.Date.Year, enti.Date.Month, enti.Date.Day)) == 0))
                .SingleOrDefaultAsync();
            if (obj == null) return true;
            else return false;
        }
    }
}
