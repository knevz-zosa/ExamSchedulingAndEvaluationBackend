﻿using Entrance.Domain.Contracts;

namespace Entrance.Application.IRepositories;
public interface IWriteRepositoryAsync<T, in TId> where T : class, IEntity<TId>
{
    Task<T> CreateAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}
