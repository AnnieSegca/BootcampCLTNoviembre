

using Products.Cqrs.Application.Abstractions.Messaging;

namespace Products.Cqrs.Application.Productos.Commands.CrearProducto;

public sealed record CrearProductoCommand(
    string Name,
    string? Description,
    decimal Price,
    int Stock) : ICommand<Guid>;
