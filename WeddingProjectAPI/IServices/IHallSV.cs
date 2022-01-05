using ResortProjectAPI.ModelEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeddingProjectAPI.IServices
{
    public interface IHallSV
    {
        Task<Hall> GetByID(string ID);

        Task<IEnumerable<Hall>> GetAll();

        Task<int> Create(Hall enti);

        Task<int> Update(Hall enti);

        Task<int> Delete(string ID);

        Task<string> GenerateID();

        Task<int> AddImg(ImageOfHall enti);

        Task<int> RemoveImg(string url);

        Task<ImageOfHall> GetImg(string url);
    }
}
