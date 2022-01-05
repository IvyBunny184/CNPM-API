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
    public class StaffSV : IStaffSV
    {
        private readonly WeddingDBContext db;

        public StaffSV(WeddingDBContext db)
        {
            this.db = db;
        }

        public async Task<int> Create(Staff staff)
        {
            if (await GetByID(staff.ID) != null) return Result.EXIST;
            try
            {
                db.Staffs.Add(staff);
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
            try
            {
                db.Staffs.Remove(obj);
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
            if ((await GetAll()).Any() == false) return "NV000000";
            string id = "NV";
            string staffID = db.Staffs.Max(x => x.ID);
            int number = int.Parse(staffID[2..staffID.Length]) + 1;
            while((id + number).Length < 8)
            {
                id += '0';
            }
            return id + number;
        }

        public async Task<IEnumerable<Staff>> GetAll()
        {
            return await db.Staffs.Include(s => s.Permission).ToListAsync();
        }

        public async Task<Staff> GetByID(string ID)
        {
            return await db.Staffs.Include(x => x.Permission).FirstOrDefaultAsync(x => x.ID == ID);
        }

        public async Task<int> Update(Staff staff)
        {
            var obj = await GetByID(staff.ID);
            if (obj == null) return Result.NOTFOUND;
            try
            {
                obj.Name = staff.Name;
                obj.Birth = staff.Birth;
                obj.Phone = staff.Phone;
                obj.Gender = staff.Gender;
                obj.RoleID = staff.RoleID;
                obj.Password = staff.Password;
                obj.Email = staff.Email;
                await db.SaveChangesAsync();
                return Result.SUCCESS;
            }
            catch
            {
                return Result.FAIL;
            }
        }

        public async Task<Permission> GetRole(string ID)
        {
            return await db.Permissions.FindAsync(ID);
        }

        public async Task<IEnumerable<Permission>> GetAllRole()
        {
            return await db.Permissions.ToListAsync();
        }
    }
}
