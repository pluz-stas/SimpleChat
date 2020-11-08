using SimpleChat.Dal.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleChat.Bll.Interfaces
{
    public interface IService<TModel, QEntity>
        where TModel : class, new()
        where QEntity : class, IDbEntity, new()
    {
        Task<IEnumerable<TModel>> GetAllAsync(int skip, int top, Func<QEntity, TModel> entityToModelMapper);

        Task<TModel> GetByIdAsync(int id, Func<QEntity, TModel> entityToModelMapper);

        Task<int> CreateAsync(TModel model, Func<TModel, QEntity> modelToEntityMapper);

        Task UpdateAsync(TModel model, Func<TModel, QEntity> modelToEntityMapper);

        Task DeleteAsync(int id);

        Task<int> GetCountAsync();
    }
}
