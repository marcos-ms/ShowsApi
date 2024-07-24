using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SolucionApi.Data.Entities;
using SolucionApi.Data.Repositories.Contracts;

namespace SolucionApi.Data.Repositories.Implementations;

public abstract class GenericRepository<T, TId> : IGenericRepository<T, TId> where T : Entity<TId>
{
    private readonly DataBaseApiDbContext _context;
    protected DbSet<T> Entities => _context.Set<T>();

    protected GenericRepository(DataBaseApiDbContext context)
    {
        _context = context;
    }

    public async Task<T> Insert(T entity)
    {
        EntityEntry<T> insertedValue = await _context.Set<T>().AddAsync(entity);
        return insertedValue.Entity;
    }

    public async Task<T?> GetById(TId id)
        => await _context.Set<T>()
            .FindAsync(id);


    public IQueryable<T> GetAll()
        => _context.Set<T>();

    public T Update(T entity)
    {
        EntityEntry<T> updatedValue =_context.Set<T>().Update(entity);
        return updatedValue.Entity;
    }

    public async Task<bool> Remove(TId id)
    {
        T? entity = await GetById(id);
        if (entity is null)
            return false;
        _context.Set<T>().Remove(entity);
        return true;
    }
    public void RemoveRange(params T[] entities)
    {
        _context.Set<T>().RemoveRange(entities);
    }

    public int Count() => _context.Set<T>().Count();

    public async Task<bool> Exist(TId id)
    {
        bool res = await _context.Set<T>().AnyAsync(x => x.Id.Equals(id));
        return res;
    }
}