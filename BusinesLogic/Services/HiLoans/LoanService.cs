using BusinesLogic.Interfaces.HiLoans;
using BusinesLogic.Repository.Services;
using Microsoft.EntityFrameworkCore;
using Models.Contexts;
using Models.Models.HiAccounting;
using System;
using System.Collections.Generic;
using System.Linq;
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
            => await Filter(x => x.UserId == userId)
            .Include(x => x.Debs)
            .Include(x => x.Payments).Include(x => x.ClientUser)
            .ThenInclude(x => x.User).ToListAsync();

        public async Task<Loan> GetByIdWithRelationships(Guid id)
        {
            return await GetAll().Include(x => x.Debs)
                .Include(x => x.Payments)
                .Include(x => x.ClientUser)
                .ThenInclude(x => x.User).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> SoftRemove(Guid id)
        {
            var model = await GetById(id);
            model.State = Models.Enums.State.Removed;
            model.UpdateAt = DateTime.Now;
            return await Update(model);
        }
    }
}
