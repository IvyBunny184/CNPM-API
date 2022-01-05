using ResortProjectAPI.ModelEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResortProjectAPI.IServices
{
    public interface IFoodSV
    {
        Task<Food> GetByID(string ID);

        Task<IEnumerable<Food>> GetAll();

        Task<int> Create(Food enti);

        Task<int> Update(Food enti);

        Task<int> Delete(string ID);

        Task<string> GenerateID();
    }
}
