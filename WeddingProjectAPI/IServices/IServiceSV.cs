using ResortProjectAPI.ModelEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResortProjectAPI.IServices
{
    public interface IServiceSV
    {
        Task<Service> GetByID(string ID);

        Task<IEnumerable<Service>> GetAll();

        Task<int> Create(Service enti);

        Task<int> Update(Service enti);

        Task<int> Delete(string ID);

        Task<string> GenerateID();
    }
}
