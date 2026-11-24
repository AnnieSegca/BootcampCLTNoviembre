using Products.Cqrs.Application.Abstractions;
using Products.Cqrs.Application.Abstractions.Messaging;
using Products.Cqrs.Domain.Productos;

namespace Products.Cqrs.Application.Productos.Commands.CrearProducto;

public sealed class CrearProductoCommandHandler(
    IProductRepository repository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CrearProductoCommand, Guid>
{
    public async Task<Guid> Handle(CrearProductoCommand command, CancellationToken ct)
    {
        var producto = Product.Crear(command.Name, command.Description, command.Price, command.Stock);
        await repository.AddAsync(producto, ct);
        await unitOfWork.SaveChangesAsync(ct);

        return producto.Id;
    }
}