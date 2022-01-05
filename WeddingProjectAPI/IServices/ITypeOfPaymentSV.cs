using ResortProjectAPI.ModelEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResortProjectAPI.IServices
{
    public interface ITypeOfPaymentSV
    {
        Task<TypeOfPayment> GetByID(string ID);

        Task<IEnumerable<TypeOfPayment>> GetAll();

        Task<int> Create(TypeOfPayment payment);

        Task<int> Update(TypeOfPayment payment);

        Task<int> Delete(string ID);

        Task<string> GenerateID();
    }
}
