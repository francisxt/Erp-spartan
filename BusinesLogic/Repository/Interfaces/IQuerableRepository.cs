using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinesLogic.Repository.Interfaces
{
    public interface IQuerableRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> GetById(Guid id);
        IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> expression);
    }
}
