using Entrance.Application.IRepositories;
using Entrance.Domain.Contracts;
using Entrance.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Entrance.Infrastructure.Repositories;
public class ReadRepositoryAsync<T, TId> : IReadRepositoryAsync<T, TId> where T : BaseEntity<TId>
{
    private readonly ApplicationDbContext _context;

    public ReadRepositoryAsync(ApplicationDbContext context)
    {
        _context = context;
    }

    public IQueryable<T> Entities => _context.Set<T>();

    public async Task<List<T>> GetAllAsync()
    {
        return await _context.Set<T>().AsNoTracking().ToListAsync();
    }

    public async Task<T> GetAsync(TId id)
    {
        return await _context.Set<T>().FindAsync(id);
    }
}
