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
    public class FoodSV: IFoodSV
    {
        private readonly WeddingDBContext db;

        public FoodSV(WeddingDBContext db)
        {
            this.db = db;
        }

        public async Task<int> Create(Food enti)
        {
            if (await GetByID(enti.ID) != null) return Result.EXIST;
            try
            {
                db.Foods.Add(enti);
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
            if (obj.Menus.Count > 0) return Result.NOTREMOVE;
            try
            {
                db.Foods.Remove(obj);
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
            if ((await GetAll()).Any() == false) return "FD000000";
            string id = "FD";
            string objID = db.Foods.Max(x => x.ID);
            int number = int.Parse(objID[2..objID.Length]) + 1;
            while ((id + number).Length < 8)
            {
                id += '0';
            }
            return id + number;
        }

        public async Task<IEnumerable<Food>> GetAll()
        {
            return await db.Foods.ToListAsync();
        }

        public async Task<Food> GetByID(string ID)
        {
            return await db.Foods.Include(x => x.Menus).FirstOrDefaultAsync(x => x.ID == ID);
        }

        public async Task<int> Update(Food enti)
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
