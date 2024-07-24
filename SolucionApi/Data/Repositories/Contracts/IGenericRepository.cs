using SolucionApi.Data.Entities;

namespace SolucionApi.Data.Repositories.Contracts;

public interface IGenericRepository<T, TId> where T : Entity<TId>
{
    Task<T> Insert(T entity);
    Task<T?> GetById(TId id);
    IQueryable<T> GetAll();
    T Update(T entity);
    Task<bool> Remove(TId id);
    void RemoveRange(params T[] entities);
    int Count();
    Task<bool> Exist(TId id);
}