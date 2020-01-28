using BusinesLogic.Repository.Interfaces;
using BusinesLogic.Repository.Services;
using BusinesLogic.UnitOfWork;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinesLogic.Interfaces
{
    public interface IClientUserService : IBaseRepository<ClientUser>
    {
        Task<IEnumerable<ClientUser>> GetAllWithRelationships(string UserId);
        Task<ClientUser> GetByIdWithRelationships(Guid id);
        Task<bool> SoftRemove(Guid id);
    }
}
