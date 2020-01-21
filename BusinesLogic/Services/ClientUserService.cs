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

        public async Task<IEnumerable<ClientUser>> GetAllWithRelationships(string UserId)
            => await GetAll().Where(x => x.CreatedBy == UserId).Include(x => x.User).ToListAsync();
        public async Task<ClientUser> GetByIdWithRelationships(Guid id)
            => await _dbContext.ClientUsers.Include(x => x.User).SingleOrDefaultAsync(x => x.Id == id);


    }
}
