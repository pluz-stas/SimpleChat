using SimpleChat.Bll.Interfaces;
using SimpleChat.Dal.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace SimpleChat.Bll.Services
{
    public abstract class AbstractService<TModel, QEntity> : IService<TModel, QEntity>
        where TModel : class, new()
        where QEntity : class, IDbEntity, new() 
    {
        protected private readonly IRepository<QEntity> _repository;

        protected AbstractService(IRepository<QEntity> repository)
        {
            _repository = repository;
        }

        public virtual async Task<IEnumerable<TModel>> GetAllAsync(int skip, int top, Func<QEntity, TModel> entityToModelMapper = null) =>
            (await _repository.GetAllAsync(skip, top)).Select(x => entityToModelMapper?.Invoke(x));

        public virtual async Task<TModel> GetByIdAsync(int id, Func<QEntity, TModel> entityToModelMapper = null) =>
            entityToModelMapper?.Invoke(await _repository.GetByIdAsync(id));

        public virtual async Task<int> CreateAsync(TModel model, Func<TModel, QEntity> modelToEntityMapper = null) =>
            await _repository.CreateAsync(modelToEntityMapper?.Invoke(model));

        public virtual async Task UpdateAsync(TModel model, Func<TModel, QEntity> modelToEntityMapper = null) =>
            await _repository.UpdateAsync(modelToEntityMapper?.Invoke(model));

        public virtual async Task DeleteAsync(int id) =>
            await _repository.DeleteAsync(id);

        public virtual async Task<int> GetCountAsync() =>
            await _repository.GetCountAsync();
    }
}