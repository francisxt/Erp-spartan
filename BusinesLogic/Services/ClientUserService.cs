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
    public class ClientUserService : BaseRepository<ClientUser>, IClientUserService
    {
        private readonly ApplicationDbContext _dbContext;
        public ClientUserService(ApplicationDbContext dbContext) : base(dbContext) => _dbContext = dbContext;

        public async Task<IEnumerable<ClientUser>> GetAllWithRelationships()  
            => await GetAll().Include(x => x.User).ToListAsync();
        
    }
}
