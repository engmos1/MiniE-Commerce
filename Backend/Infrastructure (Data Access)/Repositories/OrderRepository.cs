using Application.Interfaces.RepoInterfaces;
using Domain.Entities;
using Infrastructure__Data_Access_.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure__Data_Access_.Repositories;

public class OrderRepository : GenericRepository<Order>, IOrderRepository
{
    public OrderRepository(AppDbContext context) : base(context) { }

    public async Task<Order?> GetByIdWithItemsAsync(Guid id)
    {
        return await _dbSet
            .Include(o => o.Items)
            .ThenInclude(i => i.Product)
            .FirstOrDefaultAsync(o => o.Id == id);
    }
}
