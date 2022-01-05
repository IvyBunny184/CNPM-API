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
    public class TypeOfPaymentSV : ITypeOfPaymentSV
    {
        private readonly WeddingDBContext db;

        public TypeOfPaymentSV(WeddingDBContext db)
        {
            this.db = db;
        }

        public async Task<int> Create(TypeOfPayment payment)
        {
            if (await GetByID(payment.ID) != null) return Result.EXIST;
            try
            {
                db.TypeOfPayments.Add(payment);
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
            if (obj.Bills.Count > 0) return Result.NOTREMOVE;
            try
            {
                db.TypeOfPayments.Remove(obj);
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
            if ((await GetAll()).Any() == false) return "TPM00000";
            string id = "TPM";
            string objID = db.TypeOfPayments.Max(x => x.ID);
            int number = int.Parse(objID[3..objID.Length]) + 1;
            while ((id + number).Length < 8)
            {
                id += '0';
            }
            return id + number;
        }

        public async Task<IEnumerable<TypeOfPayment>> GetAll()
        {
            return await db.TypeOfPayments.Include(x => x.Bills).ToListAsync();
        }

        public async Task<TypeOfPayment> GetByID(string ID)
        {
            return await db.TypeOfPayments.Include(x => x.Bills).FirstOrDefaultAsync(x => x.ID == ID);
        }

        public async Task<int> Update(TypeOfPayment payment)
        {
            var obj = await GetByID(payment.ID);
            if (obj == null) return Result.NOTFOUND;
            try
            {
                obj.Name = payment.Name;
                obj.Describe = payment.Describe;
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
