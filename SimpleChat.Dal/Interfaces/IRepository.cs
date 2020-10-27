using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SimpleChat.Dal.Interfaces
{
    public interface IRepository<T> where T : class, IDbEntity
    {
        IEnumerable<T> GetAll(int skip);
        
        Task<IEnumerable<T>> FilterAsync(Expression<Func<T, bool>> predicate);

        Task<int> CreateAsync(T model);

        Task<T> GetByIdAsync(int id);

        Task UpdateAsync(T model);

        Task DeleteAsync(int id);

        Task<int> GetCount();
    }
}
