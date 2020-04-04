using BusinesLogic.Interfaces.HiLoans;
using BusinesLogic.Repository.Services;
using Models.Contexts;
using Models.Models.HiAccounting;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinesLogic.Services.HiLoans
{
    public class LoanService : BaseRepository<Loan>, ILoanService
    {
        public readonly ApplicationDbContext _dbContext;
        public LoanService(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
