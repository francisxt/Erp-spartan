using BusinesLogic.Interfaces.HiLoans;
using BusinesLogic.Repository.Services;
using Microsoft.EntityFrameworkCore;
using Models.Contexts;
using Models.Models.HiAccounting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinesLogic.Services.HiLoans
{
    public class LoanService : BaseRepository<Loan>, ILoanService
    {
        public readonly ApplicationDbContext _dbContext;
        public LoanService(ApplicationDbContext dbContext) : base(dbContext) 
            => _dbContext = dbContext;
        

        public async Task<IEnumerable<Loan>> GetAllWithRelationShip(string userId)
            => await Filter(x => x.UserId == userId).Include(x => x.ClientUser)
            .ThenInclude(x => x.User).ToListAsync();
    }
}
