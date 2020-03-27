using BusinesLogic.Repository.Interfaces;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinesLogic.Interfaces
{
    public interface IInventaryService : IBaseRepository<Article>
    {
        Task<IEnumerable<Article>> GetByIdAll(string id);
    }
}
