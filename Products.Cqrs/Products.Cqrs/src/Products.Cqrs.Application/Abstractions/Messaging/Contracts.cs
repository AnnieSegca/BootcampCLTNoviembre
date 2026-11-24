namespace Products.Cqrs.Application.Abstractions.Messaging;

// Mediator propio y liviano (reemplaza a MediatR, que desde v13 tiene licencia comercial).
// Mismo modelo mental: Request -> Pipeline de Behaviors -> Handler.

/// <summary>Mensaje que produce una respuesta.</summary>
public interface IRequest<out TResponse> { }

/// <summary>Intencion de CAMBIAR el estado del sistema.</summary>
public interface ICommand<out TResponse> : IRequest<TResponse> { }

/// <summary>Solicitud de LECTURA: nunca modifica el estado.</summary>
public interface IQuery<out TResponse> : IRequest<TResponse> { }

public interface IRequestHandler<in TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    Task<TResponse> Handle(TRequest request, CancellationToken ct);
}

public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
    where TCommand : ICommand<TResponse>;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
    where TQuery : IQuery<TResponse>;

public delegate Task<TResponse> RequestHandlerDelegate<TResponse>();

/// <summary>Middleware del pipeline (validacion, logging, transacciones, cache...).</summary>
public interface IPipelineBehavior<in TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken ct);
}

/// <summary>Punto de entrada: la presentacion solo conoce ISender.</summary>
public interface ISender
{
    Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken ct = default);
}
