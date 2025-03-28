﻿using Entrance.Domain.Contracts;

namespace Entrance.Application.IRepositories;
public interface IReadRepositoryAsync<T, in TId> where T : class, IEntity<TId>
{
    Task<List<T>> GetAllAsync();
    Task<T> GetAsync(TId id);
    IQueryable<T> Entities { get; }
}
