using BusinesLogic.Interfaces.HiAccouting;
using BusinesLogic.Repository.Services;
using Microsoft.EntityFrameworkCore;
using Models.Contexts;
using Models.Models.Accounting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinesLogic.Services.HiAccouting
{
    public class EnterpriseService : BaseRepository<Enterprise>, IEnterpriseService
    {
        private readonly ApplicationDbContext _context;
        public EnterpriseService(ApplicationDbContext dbContext) : base(dbContext) => _context = dbContext;

        public async Task<IEnumerable<Enterprise>> GetList(string userId)
            => await GetAll().Where(x => x.UserId == userId).ToListAsync();
    }
}
