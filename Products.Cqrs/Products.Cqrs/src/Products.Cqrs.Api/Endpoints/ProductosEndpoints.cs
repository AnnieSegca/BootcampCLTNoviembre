using Products.Cqrs.Application.Abstractions.Messaging;
using Products.Cqrs.Application.Productos.Commands.ActualizarProducto;
using Products.Cqrs.Application.Productos.Commands.CrearProducto;
using Products.Cqrs.Application.Productos.Commands.EliminarProducto;
using Products.Cqrs.Application.Productos.Queries;

namespace Products.Cqrs.Api.Endpoints;

/// <summary>
/// La presentacion solo conoce ISender: GET -> Query, POST/PUT/DELETE -> Command.
/// No sabe que handler se ejecuta ni que behaviors lo envuelven.
/// </summary>
public static class ProductosEndpoints
{
    public static IEndpointRouteBuilder MapProductosEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/productos").WithTags("Productos");

        // ---------- Queries ----------
        group.MapGet("/", async (ISender sender, CancellationToken ct)
                => TypedResults.Ok(await sender.Send(new ListarProductosQuery(), ct)))
            .WithName("ListarProductos");

        group.MapGet("/{id:guid}", async (Guid id, ISender sender, CancellationToken ct)
                => TypedResults.Ok(await sender.Send(new ObtenerProductoQuery(id), ct)))
            .WithName("ObtenerProducto")
            .ProducesProblem(StatusCodes.Status404NotFound);

        // ---------- Commands ----------
        group.MapPost("/", async (CrearProductoCommand command, ISender sender, CancellationToken ct) =>
        {
            var id = await sender.Send(command, ct);
            return TypedResults.Created($"/api/productos/{id}", new { id });
        })
            .WithName("CrearProducto")
            .ProducesValidationProblem()
            .ProducesProblem(StatusCodes.Status422UnprocessableEntity);

        group.MapPut("/{id:guid}", async (Guid id, ActualizarProductoRequest request, ISender sender, CancellationToken ct) =>
        {
            var command = new ActualizarProductoCommand(
                id,
                request.Name,
                request.Description,
                request.Price,
                request.Stock);

            await sender.Send(command, ct);
            return TypedResults.NoContent();
        })
            .WithName("ActualizarProducto")
            .ProducesValidationProblem()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status422UnprocessableEntity);

        group.MapDelete("/{id:guid}", async (Guid id, ISender sender, CancellationToken ct) =>
        {
            await sender.Send(new EliminarProductoCommand(id), ct);
            return TypedResults.NoContent();
        })
            .WithName("EliminarProducto")
            .ProducesProblem(StatusCodes.Status404NotFound);

        return app;
    }
}

public sealed record ActualizarProductoRequest(
    string Name,
    string? Description,
    decimal Price,
    int Stock);