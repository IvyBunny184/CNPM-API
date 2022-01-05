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
    public class HallSV: IHallSV
    {
        private readonly WeddingDBContext db;

        public HallSV(WeddingDBContext db)
        {
            this.db = db;
        }

        public async Task<int> AddImg(ImageOfHall enti)
        {
            var hall = await GetByID(enti.HallID);
            var img = await db.ImageOfHalls.FirstOrDefaultAsync(x => x.Url == enti.Url);
            if (hall == null) return Result.NOTFOUND;
            if (img != null) return Result.EXIST;
            try
            {
                db.ImageOfHalls.Add(enti);
                await db.SaveChangesAsync();
                return Result.SUCCESS;
            }
            catch
            {
                return Result.FAIL;
            }
        }

        public async Task<int> Create(Hall enti)
        {
            if (await GetByID(enti.ID) != null) return Result.EXIST;
            if (enti.Price < enti.TypeOfHall.MinPrice) return Result.IGNOREPRICE;
            try
            {
                db.Halls.Add(enti);
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
                foreach(var img in obj.Images)
                {
                    if(await RemoveImg(img.Url) != Result.SUCCESS)
                    {
                        return Result.FAIL;
                    }
                }
                db.Halls.Remove(obj);
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
            if ((await GetAll()).Any() == false) return "HALL0000";
            string id = "HALL";
            string objID = db.Halls.Max(x => x.ID);
            int number = int.Parse(objID[4..objID.Length]) + 1;
            while ((id + number).Length < 8)
            {
                id += '0';
            }
            return id + number;
        }

        public async Task<IEnumerable<Hall>> GetAll()
        {
            return await db.Halls
                .Include(x => x.Images)
                .Include(x => x.TypeOfHall)
                .ToListAsync();
        }

        public async Task<Hall> GetByID(string ID)
        {
            return await db.Halls
                .Include(x => x.Images)
                .Include(x => x.TypeOfHall)
                .Include(x => x.Bookings)
                .FirstOrDefaultAsync(x => x.ID == ID);
        }

        public async Task<ImageOfHall> GetImg(string url)
        {
            return await db.ImageOfHalls.FirstOrDefaultAsync(x => x.Url == url);
        }

        public async Task<int> RemoveImg(string url)
        {
            var obj = await GetImg(url);
            if (obj == null) return Result.NOTFOUND;
            try
            {
                db.ImageOfHalls.Remove(obj);
                await db.SaveChangesAsync();
                return Result.SUCCESS;
            }
            catch
            {
                return Result.FAIL;
            }
        }

        public async Task<int> Update(Hall enti)
        {
            var obj = await GetByID(enti.ID);
            if (obj == null) return Result.NOTFOUND;
            if (enti.Price < enti.TypeOfHall.MinPrice) return Result.IGNOREPRICE;
            try
            {
                obj.Name = enti.Name;
                obj.TypeID = enti.TypeID;
                obj.MaxTables = enti.MaxTables;
                obj.Note = enti.Note;
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
