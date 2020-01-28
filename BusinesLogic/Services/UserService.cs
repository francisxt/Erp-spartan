using BusinesLogic.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Contexts;
using Models.Enums;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinesLogic.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _dbContext;
        public UserService(ApplicationDbContext dbContext) => _dbContext = dbContext;

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
            result.Email = model.Email;
            result.PhoneNumber = model.PhoneNumber;
            result.Name = model.Name;
            result.LastName = model.LastName;
            result.UserName = model.Email;
            result.NormalizedEmail = model.Email;
            result.NormalizedUserName = model.Email;
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
    }
}
