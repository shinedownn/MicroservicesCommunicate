using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ContactMicroservice.DataAccess.Abstract
{
    public interface IEntityRepository<T>
        where T : class, IEntity
    {
        T Add(T entity);
        T Update(T entity);
        void Delete(T entity);
        Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>> expression = null);
        Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>> expression, Expression<Func<T, object>>[] includes, params Func<IQueryable<T>, IIncludableQueryable<T, object>>[] subincludes);
        Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>> expression, Expression<Func<T, object>>[] include);
        T Get(Expression<Func<T, bool>> expression);
        Task<T> GetAsync(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes);
        int SaveChanges();
        Task<int> SaveChangesAsync();
        IQueryable<T> Query();

        TResult InTransaction<TResult>(Func<TResult> action, Action successAction = null, Action<Exception> exceptionAction = null);
        Task<int> GetCountAsync(Expression<Func<T, bool>> expression = null);
        int GetCount(Expression<Func<T, bool>> expression = null);
        IEnumerable<T> Include(params Expression<Func<T, object>>[] includes);

    }
}
