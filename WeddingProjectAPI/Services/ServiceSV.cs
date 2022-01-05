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
    public class ServiceSV: IServiceSV
    {
        private readonly WeddingDBContext db;

        public ServiceSV(WeddingDBContext db)
        {
            this.db = db;
        }

        public async Task<int> Create(Service enti)
        {
            if (await GetByID(enti.ID) != null) return Result.EXIST;
            try
            {
                db.Services.Add(enti);
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
            if (obj.ListServices.Count > 0) return Result.NOTREMOVE;
            try
            {
                db.Services.Remove(obj);
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
            if ((await GetAll()).Any() == false) return "SV000000";
            string id = "SV";
            string objID = db.Services.Max(x => x.ID);
            int number = int.Parse(objID[2..objID.Length]) + 1;
            while ((id + number).Length < 8)
            {
                id += '0';
            }
            return id + number;
        }

        public async Task<IEnumerable<Service>> GetAll()
        {
            return await db.Services.ToListAsync();
        }

        public async Task<Service> GetByID(string ID)
        {
            return await db.Services.Include(x => x.ListServices).FirstOrDefaultAsync(x => x.ID == ID);
        }

        public async Task<int> Update(Service enti)
        {
            var obj = await GetByID(enti.ID);
            if (obj == null) return Result.NOTFOUND;
            try
            {
                obj.Name = enti.Name;
                obj.Describe = enti.Describe;
                obj.Price = enti.Price;
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
