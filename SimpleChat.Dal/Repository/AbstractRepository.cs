using Microsoft.EntityFrameworkCore;
using SimpleChat.Dal.Interfaces;
using SimpleChat.Dal.Resources;
using SimpleChat.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SimpleChat.Dal.Repository
{
    public abstract class AbstractRepository<TEntity> : IRepository<TEntity> where TEntity : class, IDbEntity
    {
        private protected SimpleChatDbContext dbContext;
        protected AbstractRepository(SimpleChatDbContext context)
        {
            dbContext = context;
        }

        public virtual async Task<int> CreateAsync(TEntity model)
        {
            if (model == null)
                throw new ArgumentNullException(string.Format(ErrorDetails.EntityCannotBeNull, nameof(TEntity)));

            await dbContext.Set<TEntity>().AddAsync(model);
            await dbContext.SaveChangesAsync();

            return model.Id;
        }

        public virtual async Task DeleteAsync(int id)
        {
            var model = await dbContext.Set<TEntity>().FindAsync(id);

            if (model == null)
                throw new NotFoundException(string.Format(ErrorDetails.ResourceDoesNotExist, nameof(TEntity), id));

            dbContext.Set<TEntity>().Remove(model);
            await dbContext.SaveChangesAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> FilterAsync(Expression<Func<TEntity, bool>> predicate) => 
            await dbContext.Set<TEntity>()
                .AsNoTracking()
                .Where(predicate)
                .ToListAsync();

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(int skip, int top) => 
            await dbContext.Set<TEntity>()
                .Skip(skip)
                .Take(top)
                .AsNoTracking()
                .ToListAsync();

        public virtual async Task<TEntity> GetByIdAsync(int id) => 
            await dbContext.Set<TEntity>().FindAsync(id) ??
            throw new NotFoundException(string.Format(ErrorDetails.ResourceDoesNotExist, nameof(TEntity), id));

        public virtual async Task<int> GetCountAsync() =>
            await dbContext.Set<TEntity>()
                .CountAsync();

        public Task<bool> IsExistsAsync(int id) => dbContext.Set<TEntity>().AnyAsync(x => x.Id == id);

        public virtual async Task UpdateAsync(TEntity model)
        {
            if (model == null)
                throw new ArgumentNullException(string.Format(ErrorDetails.EntityCannotBeNull, nameof(TEntity)));

            dbContext.Set<TEntity>().Update(model);
            await dbContext.SaveChangesAsync();
        }
    }
}
