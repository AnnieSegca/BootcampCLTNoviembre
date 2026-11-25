using Products.Cqrs.Application.Abstractions.Messaging;

namespace Products.Cqrs.Application.Productos.Commands.EliminarProducto;

public sealed record EliminarProductoCommand(Guid Id) : ICommand<bool>;
