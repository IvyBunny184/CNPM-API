using Microsoft.EntityFrameworkCore;
using ResortProjectAPI.Enum;
using ResortProjectAPI.IServices;
using ResortProjectAPI.ModelEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResortProjectAPI.Services
{
    public class ShiftSV:IShiftSV
    {
        private readonly WeddingDBContext db;

        public ShiftSV(WeddingDBContext db)
        {
            this.db = db;
        }

        public async Task<int> Create(Shift shift)
        {
            if (await GetByID(shift.ID) != null) return Result.EXIST;
            try
            {
                db.Shifts.Add(shift);
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
            if (obj.Bookings.Count > 0) return Result.NOTREMOVE;
            try
            {
                db.Shifts.Remove(obj);
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
            if ((await GetAll()).Any() == false) return "SH000000";
            string id = "SH";
            string objID = db.Shifts.Max(x => x.ID);
            int number = int.Parse(objID[2..objID.Length]) + 1;
            while ((id + number).Length < 8)
            {
                id += '0';
            }
            return id + number;
        }

        public async Task<IEnumerable<Shift>> GetAll()
        {
            return await db.Shifts.Include(x => x.Bookings).ToListAsync();
        }

        public async Task<Shift> GetByID(string ID)
        {
            return await db.Shifts.Include(x => x.Bookings).FirstOrDefaultAsync(x => x.ID == ID);
        }

        public async Task<int> Update(Shift shift)
        {
            var obj = await GetByID(shift.ID);
            if (obj == null) return Result.NOTFOUND;
            try
            {
                obj.Name = shift.Name;
                obj.Note = shift.Note;
                await db.SaveChangesAsync();
                return Result.SUCCESS;
            }
            catch
            {
                return Result.FAIL;
            }
        }
    }
}
