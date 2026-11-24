using Products.Cqrs.Application.Productos;

namespace Products.Cqrs.Application.Abstractions.Data;

/// <summary>
/// Puerto del MODELO DE LECTURA: devuelve DTOs listos para la respuesta, optimizados para consultar.
/// No pasa por el agregado ni aplica reglas de negocio.
/// </summary>
public interface IProductosReadStore
{
    Task<IReadOnlyList<ProductoResumenResponse>> ListarAsync(CancellationToken cancellationToken = default);
    Task<ProductoDetalleResponse?> ObtenerPorIdAsync(Guid id, CancellationToken cancellationToken = default);
}
