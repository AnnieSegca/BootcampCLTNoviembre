using Products.Cqrs.Application.Abstractions;
using Products.Cqrs.Application.Abstractions.Messaging;
using Products.Cqrs.Application.Common;
using Products.Cqrs.Application.Productos.Commands.Eliminar_Producto;
using Products.Cqrs.Domain.Productos;

namespace Products.Cqrs.Application.Productos.Commands.EliminarProducto;

public sealed class EliminarProductoCommandHandler(
    IProductRepository repository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<EliminarProductoCommand, bool>
{
    public async Task<bool> Handle(EliminarProductoCommand command, CancellationToken ct)
    {
        var producto = await repository.GetByIdAsync(command.Id, ct)
            ?? throw new NotFoundException($"Producto con Id {command.Id} no encontrado.");

        repository.Remove(producto);
        await unitOfWork.SaveChangesAsync(ct);

        return true;
    }
}