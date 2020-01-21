using BusinesLogic.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Contexts;
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
        public async Task<bool> Update(User model)
        {
            var result = await _dbContext.ApplicationUsers.FirstOrDefaultAsync(x => x.Id == model.Id);
            if (result == null) return false;
            result.Email = model.Email;
            result.PhoneNumber = model.PhoneNumber;
            result.Name = model.Name;
            result.LastName = model.LastName;
            _dbContext.Users.Update(result);
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
