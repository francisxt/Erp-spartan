using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BusinesLogic.Repository.Interfaces
{
    public interface IHelperRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllWithRelationShips();
        Task<IEnumerable<TEntity>> GetAllWithRelationShips(string userId);
        Task<IEnumerable<SelectListItem>> GetListItem(Expression<Func<TEntity, bool>> filter = null);
    }
}
