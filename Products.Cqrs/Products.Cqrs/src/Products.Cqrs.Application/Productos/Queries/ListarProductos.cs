
using Products.Cqrs.Application.Abstractions.Data;
using Products.Cqrs.Application.Abstractions.Messaging;

namespace Products.Cqrs.Application.Productos.Queries;

public sealed record ListarProductosQuery : IQuery<IReadOnlyList<ProductoResumenResponse>>;

public sealed class ListarProductosQueryHandler(IProductosReadStore readStore)
    : IQueryHandler<ListarProductosQuery, IReadOnlyList<ProductoResumenResponse>>
{
    public Task<IReadOnlyList<ProductoResumenResponse>> Handle(
        ListarProductosQuery query, CancellationToken cancellationToken)
        => readStore.ListarAsync(cancellationToken);
}
