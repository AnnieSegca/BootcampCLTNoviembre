using FluentValidation;
using Products.Cqrs.Application.Abstractions.Messaging;


namespace Products.Cqrs.Application.Behaviors;


/// <summary>
/// Intercepta TODOS los requests y ejecuta sus validadores antes del handler.
/// Los handlers quedan libres de validacion de formato (preocupacion transversal).
/// </summary>
internal sealed class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken ct)
    {
        if (!validators.Any())
            return await next();

        var context = new ValidationContext<TRequest>(request);
        var results = await Task.WhenAll(validators.Select(v => v.ValidateAsync(context, ct)));
        var errores = results.SelectMany(r => r.Errors).Where(e => e is not null).ToList();

        if (errores.Count != 0)
            throw new ValidationException(errores);

        return await next();
    }
}
