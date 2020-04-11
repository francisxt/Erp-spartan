using BusinesLogic.Repository.Interfaces;
using Models.Models.HiAccounting;
using Models.Models.HiAccounting.Debs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinesLogic.Interfaces.HiLoans
{
    public interface ILoanService : IBaseRepository<Loan>
    {
        Task<IEnumerable<Loan>> GetAllWithRelationShip(string userId);
        Task<Loan> GetByIdWithRelationships(Guid id);
        Task<bool> SoftRemove(Guid id);
    }
}
