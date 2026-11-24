using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Products.Cqrs.Application.Abstractions.Messaging;
using Products.Cqrs.Application.Behaviors;
using Products.Cqrs.Application.Messaging;

namespace Products.Cqrs.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        services.AddScoped<ISender, Sender>();

        // Registra todos los handlers (commands y queries) del ensamblado.
        var handlers = assembly.GetTypes()
            .Where(t => t is { IsAbstract: false, IsInterface: false, IsGenericTypeDefinition: false })
            .SelectMany(t => t.GetInterfaces()
                .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>))
                .Select(i => (Service: i, Implementation: t)));
        foreach (var (service, implementation) in handlers)
            services.AddScoped(service, implementation);

        // Pipeline: el orden de registro es el orden de ejecucion (Logging envuelve a Validation).
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddValidatorsFromAssembly(assembly, includeInternalTypes: true);

        return services;
    }
}
