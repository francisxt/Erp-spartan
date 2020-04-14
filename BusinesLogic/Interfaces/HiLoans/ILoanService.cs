using BusinesLogic.Repository.Interfaces;
using Models.Enums;
using Models.Models.HiAccounting;
using Models.Models.HiAccounting.Debs;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinesLogic.Interfaces.HiLoans
{
    public interface ILoanService : IBaseRepository<Loan>
    {
        Task<IEnumerable<Loan>> GetAllWithRelationShip(string userId);
        Task<Loan> GetByIdWithRelationships(Guid id, State state);
        Task<bool> SoftRemove(Guid id);
        Task<bool> PaymentDeb(Guid id, Guid idLoan, decimal extraMount);
    }
}
