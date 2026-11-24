using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Products.Cqrs.Application.Abstractions;
using Products.Cqrs.Application.Abstractions.Data;
using Products.Cqrs.Domain.Productos;
using Products.Cqrs.Infrastructure.Persistence;
using Products.Cqrs.Infrastructure.ReadModels;

namespace Products.Cqrs.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string databaseName = "ProductosDb")
    {
        services.AddDbContext<ProductosDbContext>(options => options.UseInMemoryDatabase(databaseName));

        // Escritura
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ProductosDbContext>());
        services.AddScoped<IProductRepository, ProductRepository>();

        // Lectura
        services.AddScoped<IProductosReadStore, ProductosReadStore>();

        return services;
    }

    public static void InicializarBaseDeDatos(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ProductosDbContext>();
        db.Database.EnsureCreated();
        DatosIniciales.Cargar(db);
    }
}