

using Products.Cqrs.Application.Abstractions.Data;
using Products.Cqrs.Application.Abstractions.Messaging;
using Products.Cqrs.Application.Common;

namespace Products.Cqrs.Application.Productos.Queries;

public sealed record ObtenerProductoQuery(Guid Id) : IQuery<ProductoDetalleResponse>;

public sealed class ObtenerProductoQueryHandler(IProductosReadStore readStore)
    : IQueryHandler<ObtenerProductoQuery, ProductoDetalleResponse>
{
    public async Task<ProductoDetalleResponse> Handle(
        ObtenerProductoQuery query, CancellationToken cancellationToken)
    {
        var producto = await readStore.ObtenerPorIdAsync(query.Id, cancellationToken);
        return producto ?? throw new NotFoundException($"Producto con Id {query.Id} no encontrado.");
    }
}
