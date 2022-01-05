using ResortProjectAPI.ModelEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeddingProjectAPI.IServices
{
    public interface IBookingSV
    {
        Task<Booking> GetByID(string ID);

        Task<IEnumerable<Booking>> GetAll();

        Task<int> Create(Booking enti);

        Task<int> Update(Booking enti);

        Task<int> Delete(string ID);

        Task<string> GenerateID();

        Task<int> AddService(ListServices enti);

        Task<int> RemoveService(ListServices enti);

        Task<int> AddFood(Menu enti);

        Task<int> RemoveFood(Menu enti);

        //Task<bool> CanBooking(Booking enti);
    }
}
