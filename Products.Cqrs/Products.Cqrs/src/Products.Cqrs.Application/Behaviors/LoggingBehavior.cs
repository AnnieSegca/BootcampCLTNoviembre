using Products.Cqrs.Application.Abstractions.Messaging;
using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace Products.Cqrs.Application.Behaviors;


/// <summary>Registra cada request y su duracion. Ejemplo de preocupacion transversal en el pipeline.</summary>
internal sealed class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken ct)
    {
        var nombre = typeof(TRequest).Name;
        var inicio = Stopwatch.GetTimestamp();
        try
        {
            var response = await next();
            logger.LogInformation("{Request} completado en {Ms:N1} ms", nombre, Stopwatch.GetElapsedTime(inicio).TotalMilliseconds);
            return response;
        }
        catch (Exception ex)
        {
            logger.LogWarning("{Request} fallo en {Ms:N1} ms: {Error}", nombre, Stopwatch.GetElapsedTime(inicio).TotalMilliseconds, ex.Message);
            throw;
        }
    }
}

