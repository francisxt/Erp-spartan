using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinesLogic.Interfaces
{
    public interface IRoleService
    {
        Task<bool> Create(string RoleName);
        Task<bool> Remove(string id);
        Task<IEnumerable<IdentityRole>> GetAll();

    }
}
