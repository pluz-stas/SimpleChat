﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleChat.Dal.Interfaces
{
    public interface IRepository<T> where T : class, IDbEntity
    {
        Task<IEnumerable<T>> GetAllAsync(int skip, int top);
        
        Task<IEnumerable<T>> FilterAsync(Predicate<T> predicate);

        Task<int> CreateAsync(T model);

        Task<T> GetByIdAsync(int id);

        Task<bool> IsExistsAsync(int id);

        Task UpdateAsync(T model);

        Task DeleteAsync(int id);

        Task<int> GetCountAsync();
    }
}
