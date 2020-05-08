using BusinesLogic.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Models.Contexts;
using Models.Enums;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BusinesLogic.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _dbContext;
        public UserService(ApplicationDbContext dbContext) => _dbContext = dbContext;

        public Task<IEnumerable<User>> GetAllWithRelationShips()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAllWithRelationShips(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<SelectListItem>> GetListItem(Expression<Func<User, bool>> filter = null)
        {
            var result = _dbContext.ApplicationUsers;
            if (filter != null) result.Where(filter);
            return await result.Select(x => new SelectListItem { Text = $"{x.FullName} {x.UserName}" , Value = x.Id }).ToListAsync();
        }

        public async Task<User> GetUserAsync(string id) => await _dbContext.ApplicationUsers.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<bool> LockAndUnlockUser(string id)
        {
            var model = await _dbContext.ApplicationUsers.FirstOrDefaultAsync(x => x.Id == id);
            if (model == null) return false;
            model.State = model.State == State.Blocked ? State.Active : State.Blocked;
            _dbContext.Update(model);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> Update(User model)
        {
            var result = await _dbContext.ApplicationUsers.FirstOrDefaultAsync(x => x.Id == model.Id);
            if (result == null) return false;
            result.PhoneNumber = model.PhoneNumber;
            result.Name = model.Name;
            result.LastName = model.LastName;
            model.UpdateAt = DateTime.Now;
            try
            {
                _dbContext.Users.Update(result);
                return await _dbContext.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<User>> Users() => await _dbContext.ApplicationUsers.ToListAsync();

        public async Task<bool> AddToRole(string UserId, string RoleId)
        {
            _dbContext.UserRoles.Add(new IdentityUserRole<string> { UserId = UserId, RoleId = RoleId });
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> RemoveUserRoleAsync(string userId, string RoleName)
        {
            var role = await _dbContext.Roles.FirstOrDefaultAsync(x => x.Name.Equals(RoleName));
            _dbContext.UserRoles.Remove(new IdentityUserRole<string> { UserId = userId, RoleId = role.Id });
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<User> GetByEmailAsync(string email) => await _dbContext.ApplicationUsers.FirstOrDefaultAsync(x => x.UserName == email);
    }
}
