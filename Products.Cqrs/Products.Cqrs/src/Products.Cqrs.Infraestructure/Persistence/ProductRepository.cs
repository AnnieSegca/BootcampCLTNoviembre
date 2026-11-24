using Microsoft.EntityFrameworkCore;
using Products.Cqrs.Domain.Productos;

namespace Products.Cqrs.Infrastructure.Persistence;

/// <summary>Lado de escritura: entidades con tracking para que EF detecte los cambios del agregado.</summary>
internal sealed class ProductRepository(ProductosDbContext db) : IProductRepository
{
    public Task<Product?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => db.Productos.FirstOrDefaultAsync(p => p.Id == id, ct);

    public async Task AddAsync(Product product, CancellationToken ct = default)
        => await db.Productos.AddAsync(product, ct);

    public void Update(Product product)
        => db.Productos.Update(product);

    public void Remove(Product product)
        => db.Productos.Remove(product);
}