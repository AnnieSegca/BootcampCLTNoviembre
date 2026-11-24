namespace Products.Cqrs.Domain.Common;

    public abstract class Entity
    {
        public Guid Id { get; protected init; }
    }

    /// <summary>Violacion de una regla de negocio del dominio.</summary>
    public class DomainException(string message) : Exception(message);
