

namespace Products.Cqrs.Application.Productos;

public sealed record ProductoResumenResponse(
    Guid Id,
    string Name,
    decimal Price,
    int Stock,
    string Estado);

public sealed record ProductoDetalleResponse(
    Guid Id,
    string Name,
    string? Description,
    decimal Price,
    int Stock,
    string Estado);
