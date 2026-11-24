using Products.Cqrs.Domain.Common;

namespace Products.Cqrs.Domain.Productos;

    public class Product : Entity
{
    private Product() { }

    public string Name { get; private set; } = null!;
    public string? Description { get; private set; }
    public decimal Price { get; private set; }
    public int Stock { get; private set; }
    public EstadoProducto Estado { get; private set; }



    public static Product Crear(string name, string? description, decimal price, int stock)
    {
        Validar(name, price, stock);

        return new Product
        {
            Id = Guid.CreateVersion7(),
            Name = name.Trim(),
            Description = string.IsNullOrWhiteSpace(description) ? null : description.Trim(),
            Price = price,
            Stock = stock,
            Estado = EstadoProducto.Activo
        };
    }

    public void Actualizar(string name, string? description, decimal price, int stock)
    {
        Validar(name, price, stock);

        Name = name.Trim();
        Description = string.IsNullOrWhiteSpace(description) ? null : description.Trim();
        Price = price;
        Stock = stock;
    }

    public void AjustarStock(int cantidad)
    {
        var nuevoStock = Stock + cantidad;
        if (nuevoStock < 0)
            throw new DomainException("El stock resultante no puede ser negativo.");

        Stock = nuevoStock;
    }

    public void Desactivar() => Estado = EstadoProducto.Inactivo;

    public void Activar() => Estado = EstadoProducto.Activo;

    private static void Validar(string name, decimal price, int stock)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("El nombre del producto es obligatorio.");
        if (price < 0)
            throw new DomainException("El precio no puede ser negativo.");
        if (stock < 0)
            throw new DomainException("El stock no puede ser negativo.");
    }
}


