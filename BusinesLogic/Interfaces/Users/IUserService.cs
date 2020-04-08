using BusinesLogic.Repository.Interfaces;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinesLogic.Interfaces
{
    public interface IUserService : IHelperRepository<User>
    {
        Task<bool> Update(User model);
        Task<bool> LockAndUnlockUser(string id);
        Task<User> GetUserAsync(string id);

        Task<IEnumerable<User>> Users();
    }
}
