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
    public class TypeOfHallSV:ITypeOfHallSV
    {
        private readonly WeddingDBContext db;

        public TypeOfHallSV(WeddingDBContext db)
        {
            this.db = db;
        }

        public async Task<int> Create(TypeOfHall hallType)
        {
            if(await GetByID(hallType.ID) != null) return Result.EXIST;
            try
            {
                db.TypeOfHalls.Add(hallType);
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
            if (obj.Halls.Count > 0) return Result.NOTREMOVE;
            try
            {
                db.TypeOfHalls.Remove(obj);
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
            if ((await GetAll()).Any() == false) return "TOH00000";
            string id = "TOH";
            string objID = db.TypeOfHalls.Max(x => x.ID);
            int number = int.Parse(objID[3..objID.Length]) + 1;
            while ((id + number).Length < 8)
            {
                id += '0';
            }
            return id + number;
        }

        public async Task<IEnumerable<TypeOfHall>> GetAll()
        {
            return await db.TypeOfHalls.ToListAsync();
        }

        public async Task<TypeOfHall> GetByID(string ID)
        {
            return await db.TypeOfHalls.Include(x => x.Halls).FirstOrDefaultAsync(x => x.ID == ID);
        }

        public async Task<int> Update(TypeOfHall hallType)
        {
            var obj = await GetByID(hallType.ID);
            if (obj == null) return Result.NOTFOUND;
            try
            {
                obj.Name = hallType.Name;
                obj.MinPrice = hallType.MinPrice;
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
