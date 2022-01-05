using ResortProjectAPI.ModelEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeddingProjectAPI.IServices
{
    public interface IBillSV
    {
        Task<int> Create(Bill obj);

        Task<int> Delete(string id);

        Task<IEnumerable<Bill>> GetAll();

        Task<Bill> GetByID(string id);

        Task<string> GenerateID();

        Task<float> CalPrice(string bookingId);

        Task<float> CalFee(string bookingId);
    }
}
