using Products.Cqrs.Application.Abstractions.Messaging;

namespace Products.Cqrs.Application.Productos.Commands.Eliminar_Producto;

public sealed record EliminarProductoCommand(Guid Id) : ICommand<bool>;
