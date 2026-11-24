using Products.Cqrs.Domain.Productos;

namespace Products.Cqrs.Infrastructure.Persistence;

/// <summary>Datos de ejemplo. Se crean usando el propio dominio, respetando sus reglas.</summary>
public static class DatosIniciales
{
    public static readonly Guid ProductoTecladoId = Guid.Parse("11111111-1111-1111-1111-111111111111");
    public static readonly Guid ProductoMouseId = Guid.Parse("22222222-2222-2222-2222-222222222222");

    internal static void Cargar(ProductosDbContext db)
    {
        if (db.Productos.Any())
            return;

        var teclado = Product.Crear("Teclado Mecanico", "Switch tactil", 250_000m, 15);
        var mouse = Product.Crear("Mouse Inalambrico", "Ergonomico", 120_000m, 30);

        db.Productos.AddRange(teclado, mouse);
        db.SaveChanges();
    }
}