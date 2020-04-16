using BusinesLogic.Repository.Interfaces;
using Models.Models.Accounting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinesLogic.Interfaces.HiAccouting
{
    public interface IEnterpriseService : IBaseRepository<Enterprise> , IHelperRepository<Enterprise>
    {
        Task<IEnumerable<Enterprise>> GetList(string userId);
    }
}
