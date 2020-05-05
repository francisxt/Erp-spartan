using BusinesLogic.Interfaces.Alerts;
using BusinesLogic.Repository.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Models.Contexts;
using Models.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BusinesLogic.Services.Alerts
{
    public class AlertService : BaseRepository<Alert> , IAlertService
    {
        private readonly ApplicationDbContext _dbContext;
        public AlertService(ApplicationDbContext dbContext) : base(dbContext) {

            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Alert>> GetAllWithRelationShips()
            => await GetAll().Include(x => x.User).ToListAsync();

        public Task<IEnumerable<Alert>> GetAllWithRelationShips(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<SelectListItem>> GetListItem(Expression<Func<Alert, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> MaskAsRead(string userId)
        {
            await Filter(x => x.UserId == userId).ForEachAsync(x =>
            {
                x.State = Models.Enums.State.View;
            });
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<int> Quantity(string userId)
            => await Filter(x => x.UserId == userId).Where(x => x.State != Models.Enums.State.View).CountAsync();
    }
}
