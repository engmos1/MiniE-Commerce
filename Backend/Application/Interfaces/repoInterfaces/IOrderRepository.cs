using Domain.Entities;

namespace Application.Interfaces.RepoInterfaces;

public interface IOrderRepository : IGenericRepository<Order>
{
    Task<Order?> GetByIdWithItemsAsync(Guid id);
}
