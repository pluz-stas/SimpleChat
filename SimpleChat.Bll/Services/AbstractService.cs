﻿using SimpleChat.Bll.Interfaces;
using SimpleChat.Dal.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using AutoMapper;
using SimpleChat.Shared.Exceptions;
using SimpleChat.Dal.Resources;

namespace SimpleChat.Bll.Services
{
    public abstract class AbstractService<TModel, QEntity> : IService<TModel, QEntity>
        where TModel : class, new()
        where QEntity : class, IDbEntity, new() 
    {
        protected private readonly IMapper _mapper;
        protected private readonly IRepository<QEntity> _repository;

        protected AbstractService(IRepository<QEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public virtual async Task<IEnumerable<TModel>> GetAllAsync(int skip, int top) =>
            (await _repository.GetAllAsync(skip, top))
            .Select(x => _mapper.Map<QEntity, TModel>(x));

        public virtual async Task<TModel> GetByIdAsync(int id) =>
            _mapper.Map<QEntity, TModel>(await _repository.GetByIdAsync(id));

        public virtual async Task<int> CreateAsync(TModel model) =>
            await _repository.CreateAsync(_mapper.Map<TModel, QEntity>(model));

        public virtual async Task UpdateAsync(TModel model)
        {
            var entity = _mapper.Map<TModel, QEntity>(model);

            if (!await _repository.IsExistsAsync(entity.Id))
            {
                throw new NotFoundException(string.Format(ErrorDetails.ResourceDoesNotExist, nameof(QEntity), entity.Id));
            }

            await _repository.UpdateAsync(entity);
        }

        public virtual async Task DeleteAsync(int id) =>
            await _repository.DeleteAsync(id);

        public virtual async Task<int> GetCountAsync() =>
            await _repository.GetCountAsync();
    }
}