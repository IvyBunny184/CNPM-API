using ResortProjectAPI.ModelEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResortProjectAPI.IServices
{
    public interface IShiftSV
    {
        Task<Shift> GetByID(string ID);

        Task<IEnumerable<Shift>> GetAll();

        Task<int> Create(Shift shift);

        Task<int> Update(Shift shift);

        Task<int> Delete(string ID);

        Task<string> GenerateID();
    }
}
