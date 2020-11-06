using SimpleChat.Bll.Interfaces;
using SimpleChat.Dal.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SimpleChat.Bll.Extensions;
using System.Linq;

namespace SimpleChat.Bll.Services
{
    public abstract class AbstractService<QEntity, TModel> : IService<TModel> where QEntity : class, IDbEntity, new() where TModel : class, new()
    {
        protected private readonly IRepository<QEntity> _repository;

        protected AbstractService(IRepository<QEntity> repository)
        {
            _repository = repository;
        }

        public virtual async Task<int> CreateAsync(TModel model) => await _repository.CreateAsync(model.MapToModel<TModel, QEntity>());

        public virtual async Task DeleteAsync(int id) => await _repository.DeleteAsync(id);

        public virtual async Task<IEnumerable<TModel>> GetAllAsync(int skip, int top) => (await _repository.GetAllAsync(skip, top)).Select(x => x.MapToModel<QEntity, TModel>());

        public virtual async Task<TModel> GetByIdAsync(int id) => (await _repository.GetByIdAsync(id)).MapToModel<QEntity, TModel>();

        public virtual async Task<int> GetCountAsync() => await _repository.GetCountAsync();

        public virtual async Task UpdateAsync(TModel model) => await _repository.UpdateAsync(model.MapToModel<TModel, QEntity>());
    }
}