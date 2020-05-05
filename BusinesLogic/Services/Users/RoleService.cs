using BusinesLogic.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Models.Contexts;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinesLogic.Services
{
    public class RoleService : IRoleService
    {
        private readonly ApplicationDbContext _dbContext;
        public RoleService(ApplicationDbContext dbContext) => _dbContext = dbContext;

        public async Task<bool> Create(string RoleName)
        {
            _dbContext.Roles.Add(new IdentityRole { Name = RoleName, NormalizedName = RoleName });
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<IdentityRole>> GetAll() => await _dbContext.Roles.ToListAsync();

        public Task<IEnumerable<IdentityRole>> GetAllWithRelationShips()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IdentityRole>> GetAllWithRelationShips(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<SelectListItem>> GetListItem(Expression<Func<IdentityRole, bool>> filter = null)
            => await _dbContext.Roles.Select(x => new SelectListItem { Text = x.Name, Value = x.Id }).ToListAsync();

        public async Task<bool> Remove(string id)
        {
            var model = await _dbContext.Roles.FindAsync(id);
            if (model == null) return false;
            _dbContext.Remove(model);
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
