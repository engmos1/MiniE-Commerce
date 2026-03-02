using Domain.Entities;

namespace Application.Interfaces.RepoInterfaces;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<T?> GetByIdAsync(Guid id);
    Task<List<T>> GetAllAsync();
    Task<(List<T> Items, int TotalCount)> GetPaginatedAsync(int page, int pageSize);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task SaveChangesAsync();
}
