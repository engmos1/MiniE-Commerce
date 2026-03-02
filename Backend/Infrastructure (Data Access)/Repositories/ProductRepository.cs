using Application.Interfaces.RepoInterfaces;
using Domain.Entities;
using Infrastructure__Data_Access_.Context;

namespace Infrastructure__Data_Access_.Repositories;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(AppDbContext context) : base(context) { }
}
