using BusinesLogic.Interfaces.HiAccouting;
using BusinesLogic.Repository.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Models.Contexts;
using Models.Models.Accounting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinesLogic.Services.HiAccouting
{
    public class EnterpriseService : BaseRepository<Enterprise>, IEnterpriseService
    {
        private readonly ApplicationDbContext _context;
        public EnterpriseService(ApplicationDbContext dbContext) : base(dbContext) => _context = dbContext;

        public Task<IEnumerable<Enterprise>> GetAllWithRelationShips()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Enterprise>> GetAllWithRelationShips(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Enterprise>> GetList(string userId)
            => await GetAll().Where(x => x.UserId == userId).ToListAsync();

        public async Task<IEnumerable<SelectListItem>> GetListItem(Expression<Func<Enterprise, bool>> filter = null)
            => await _context.Enterprises.Where(filter).Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToListAsync();
    }
}
