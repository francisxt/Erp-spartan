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
            => await _dbContext.ClientUsers.Include(x => x.User)
                    .Include(x => x.Movements).SingleOrDefaultAsync(x => x.Id == id);

        public async Task<bool> SoftRemove(Guid id)
        {
            var model = await GetById(id);
            model.State = Models.Enums.State.Removed;
            return await Update(model);
        }

        public async Task<ClientUser> GetClientByUserId(string id)
            => await _dbContext.ClientUsers.FirstOrDefaultAsync(x => x.UserId == id);


    }
}
