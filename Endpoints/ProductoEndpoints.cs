using MYBACK.Models;
using MYBACK.Services;

namespace MYBACK.Endpoints;

public static class ProductoEndpoints
{
    public static void MapProductoEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/productos").WithTags("Productos");

        group.MapGet("/", (IProductoRepository repo) =>
            Results.Ok(repo.ObtenerTodos())
        );

        group.MapGet("/buscar", (string nombre, IProductoRepository repo) =>
            Results.Ok(repo.Buscar(nombre))
        );

        group.MapGet("/{id:int}", (int id, IProductoRepository repo) =>
        {
            var producto = repo.ObtenerPorId(id);
            return producto is null
            ? Results.NotFound(new { mensaje = "Producto no encontrado" })
            : Results.Ok(producto);
        });

        group.MapPost("/", (Producto nuevo, IProductoRepository repo) =>
        {
            if (string.IsNullOrWhiteSpace(nuevo.Nombre))
                return Results.BadRequest(new { mensaje = "El nombre es obligatorio" });

            nuevo.Id = repo.Insertar(nuevo);
            return Results.Created($"/productos/{nuevo.Id}", nuevo);
        });

        group.MapPut("/{id:int}", (int id, Producto producto, IProductoRepository repo) =>
        {
            if (string.IsNullOrWhiteSpace(producto.Nombre))
                return Results.BadRequest(new { mensaje = "El nombre es obligatorio" });

            producto.Id = id;
            return repo.Actualizar(producto)
            ? Results.NoContent()
            : Results.NotFound(new { mensaje = "Producto no encontrado" });
        });

        group.MapDelete("/{id:int}", (int id, IProductoRepository repo) =>
            repo.Eliminar(id)
            ? Results.NoContent()
            : Results.NotFound(new { mensaje = "Producto no encontrado" })
        );
    }
}
