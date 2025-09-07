﻿using System.Linq.Expressions;
using Ordering.Entities;

namespace Ordering.Repositories
{
    public interface IAsyncRepository<T> where T : EntityBase
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<IReadOnlyList<T>> GetAllAsync(Expression<Func<T, bool>> predicate);
        Task<T> GetByIdAsync(int Id);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
