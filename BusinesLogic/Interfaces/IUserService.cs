using Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinesLogic.Interfaces
{
    public interface IUserService
    {
        Task<bool> Update(User model);
        Task<bool> LockAndUnlockUser(string id);
        Task<User> GetUserAsync(string id);
    }
}
