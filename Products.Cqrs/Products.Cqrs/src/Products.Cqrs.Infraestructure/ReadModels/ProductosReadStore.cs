using Microsoft.EntityFrameworkCore;
using Products.Cqrs.Application.Abstractions.Data;
using Products.Cqrs.Application.Productos;
using Products.Cqrs.Infrastructure.Persistence;

namespace Products.Cqrs.Infrastructure.ReadModels;

/// <summary>Lado de LECTURA: proyecciones directas a DTO, sin tracking y sin pasar por el agregado.</summary>
internal sealed class ProductosReadStore(ProductosDbContext db) : IProductosReadStore
{
    public async Task<IReadOnlyList<ProductoResumenResponse>> ListarAsync(CancellationToken ct = default)
        => await db.Productos.AsNoTracking()
            .OrderBy(p => p.Name)
            .Select(p => new ProductoResumenResponse(
                p.Id,
                p.Name,
                p.Price,
                p.Stock,
                p.Estado.ToString()))
            .ToListAsync(ct);

    public Task<ProductoDetalleResponse?> ObtenerPorIdAsync(Guid id, CancellationToken ct = default)
        => db.Productos.AsNoTracking()
            .Where(p => p.Id == id)
            .Select(p => new ProductoDetalleResponse(
                p.Id,
                p.Name,
                p.Description,
                p.Price,
                p.Stock,
                p.Estado.ToString()))
            .FirstOrDefaultAsync(ct);
}