using BusinesLogic.Interfaces;
using BusinesLogic.Repository.Services;
using Microsoft.EntityFrameworkCore;
using Models.Contexts;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinesLogic.Services
{
    public class InventaryService : BaseRepository<Article>, IInventaryService
    {
        public InventaryService(ApplicationDbContext dbContext) : base(dbContext) { }

        public async Task<IEnumerable<Article>> GetByIdAll(string id) => await GetAll().Where(x => x.UserId == id).ToListAsync();
    }
}
