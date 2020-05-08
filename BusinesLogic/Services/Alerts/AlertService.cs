using BusinesLogic.Interfaces.Alerts;
using BusinesLogic.Repository.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Models.Contexts;
using Models.Enums;
using Models.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BusinesLogic.Services.Alerts
{
    public class AlertService : BaseRepository<Alert>, IAlertService
    {
        private readonly ApplicationDbContext _dbContext;
        public AlertService(ApplicationDbContext dbContext) : base(dbContext)
        {

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

        public async Task<bool> SendMasive(Alert model, string id)
        {
            var results = await _dbContext.ClientUsers.Include(x => x.User).Where(x => x.UserId != id
            && x.CreatedBy == id
            && x.State == State.Active).Select(x => x.User.Id).ToListAsync();
            var list = new List<Alert>();
            foreach (var item in results) list.Add(new Alert { Title = model.Title, Text = model.Text, UserId = item });
            await _dbContext.AddRangeAsync(list);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> RemoveAll()
        {
            var alerts = await _dbContext.Alerts.ToListAsync();
            _dbContext.RemoveRange(alerts);
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
