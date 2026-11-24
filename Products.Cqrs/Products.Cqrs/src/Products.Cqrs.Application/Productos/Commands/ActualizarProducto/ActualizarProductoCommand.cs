

using Products.Cqrs.Application.Abstractions.Messaging;

namespace Products.Cqrs.Application.Productos.Commands.ActualizarProducto;

public sealed record ActualizarProductoCommand(
    Guid Id,
    string Name,
    string? Description,
    decimal Price,
    int Stock) : ICommand<bool>;
