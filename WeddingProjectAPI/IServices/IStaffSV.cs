using ResortProjectAPI.ModelEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResortProjectAPI.IServices
{
    public interface IStaffSV
    {
        Task<Staff> GetByID(string ID);

        Task<IEnumerable<Staff>> GetAll();

        Task<int> Create(Staff staff);

        Task<int> Update(Staff staff);

        Task<int> Delete(string ID);

        Task<string> GenerateID();

        Task<Permission> GetRole(string ID);

        Task<IEnumerable<Permission>> GetAllRole();
    }
}
