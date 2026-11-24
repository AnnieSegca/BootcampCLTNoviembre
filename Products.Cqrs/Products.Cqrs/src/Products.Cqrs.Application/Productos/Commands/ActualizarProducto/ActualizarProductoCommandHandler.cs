using Products.Cqrs.Application.Abstractions;
using Products.Cqrs.Application.Abstractions.Messaging;
using Products.Cqrs.Application.Common;
using Products.Cqrs.Domain.Productos;

namespace Products.Cqrs.Application.Productos.Commands.ActualizarProducto;

public sealed class ActualizarProductoCommandHandler(
    IProductRepository repository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<ActualizarProductoCommand, bool>
{
    public async Task<bool> Handle(ActualizarProductoCommand command, CancellationToken ct)
    {
        var producto = await repository.GetByIdAsync(command.Id, ct)
            ?? throw new NotFoundException($"Producto con Id {command.Id} no encontrado.");

        producto.Actualizar(command.Name, command.Description, command.Price, command.Stock);
        repository.Update(producto);
        await unitOfWork.SaveChangesAsync(ct);
        return true;
    }
}