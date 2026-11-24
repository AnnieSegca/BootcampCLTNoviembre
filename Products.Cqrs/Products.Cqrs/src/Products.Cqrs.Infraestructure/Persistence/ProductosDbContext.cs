using Microsoft.EntityFrameworkCore;
using Products.Cqrs.Application.Abstractions;
using Products.Cqrs.Domain.Productos;

namespace Products.Cqrs.Infrastructure.Persistence;

public sealed class ProductosDbContext(DbContextOptions<ProductosDbContext> options) : DbContext(options), IUnitOfWork
{
    public DbSet<Product> Productos => Set<Product>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductosDbContext).Assembly);
}