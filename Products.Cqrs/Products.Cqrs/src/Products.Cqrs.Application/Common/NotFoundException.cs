

namespace Products.Cqrs.Application.Common;
/// <summary>El recurso solicitado no existe (HTTP 404).</summary>
public class NotFoundException(string message) : Exception(message)
{
    public static NotFoundException Tarjeta(Guid id) => new($"No existe la tarjeta {id}.");
}
