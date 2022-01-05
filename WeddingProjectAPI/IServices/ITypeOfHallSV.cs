using ResortProjectAPI.ModelEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResortProjectAPI.IServices
{
    public interface ITypeOfHallSV
    {
        Task<TypeOfHall> GetByID(string ID);

        Task<IEnumerable<TypeOfHall>> GetAll();

        Task<int> Create(TypeOfHall hallType);

        Task<int> Update(TypeOfHall hallType);

        Task<int> Delete(string ID);

        Task<string> GenerateID();
    }
}
