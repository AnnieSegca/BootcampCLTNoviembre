using Products.Cqrs.Application.Abstractions.Messaging;
using System.Collections.Concurrent;
using Microsoft.Extensions.DependencyInjection;


namespace Products.Cqrs.Application.Messaging;

/// <summary>
/// Despachador: resuelve el handler del request y lo envuelve con los behaviors registrados
/// (el primero registrado es el mas externo). La reflexion se usa una sola vez por tipo y se cachea.
/// </summary>
internal sealed class Sender(IServiceProvider serviceProvider) : ISender
{
    private static readonly ConcurrentDictionary<Type, object> Wrappers = new();

    public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var wrapper = (RequestHandlerWrapper<TResponse>)Wrappers.GetOrAdd(
            request.GetType(),
            requestType => Activator.CreateInstance(
                typeof(RequestHandlerWrapperImpl<,>).MakeGenericType(requestType, typeof(TResponse)))!);

        return wrapper.Handle(request, serviceProvider, ct);
    }

    private abstract class RequestHandlerWrapper<TResponse>
    {
        public abstract Task<TResponse> Handle(IRequest<TResponse> request, IServiceProvider sp, CancellationToken ct);
    }

    private sealed class RequestHandlerWrapperImpl<TRequest, TResponse> : RequestHandlerWrapper<TResponse>
        where TRequest : IRequest<TResponse>
    {
        public override Task<TResponse> Handle(IRequest<TResponse> request, IServiceProvider sp, CancellationToken ct)
        {
            var typedRequest = (TRequest)request;
            var handler = sp.GetRequiredService<IRequestHandler<TRequest, TResponse>>();

            RequestHandlerDelegate<TResponse> pipeline = () => handler.Handle(typedRequest, ct);

            foreach (var behavior in sp.GetServices<IPipelineBehavior<TRequest, TResponse>>().Reverse())
            {
                var next = pipeline;
                pipeline = () => behavior.Handle(typedRequest, next, ct);
            }

            return pipeline();
        }
    }
}

