using MySqlConnector;
using MYBACK.Models;
namespace MYBACK.Services;

public class ProductoRepository : IProductoRepository
{
    private readonly Conexion _conexion;

    public ProductoRepository(Conexion conexion)
    {
        _conexion = conexion;
    }

    private static Producto Mapear(MySqlDataReader reader) => new Producto
    {
        Id = reader.GetInt32("id"),
        Nombre = reader.GetString("nombre"),
        Descripcion = reader.GetString("descripcion"),
        Precio = reader.GetDecimal("precio"),
        Stock = reader.GetInt32("stock")
    };

    public List<Producto> ObtenerTodos()
    {
        var lista = new List<Producto>();

        using var conexion = _conexion.ObtenerConexion();
        conexion.Open();

        using var cmd = new MySqlCommand(
            "SELECT id, nombre, descripcion, precio, stock FROM productos ORDER BY id", conexion);
        using var reader = cmd.ExecuteReader();

        while (reader.Read())
            lista.Add(Mapear(reader));

        return lista;
    }

    public Producto? ObtenerPorId(int id)
    {
        using var conexion = _conexion.ObtenerConexion();
        conexion.Open();

        using var cmd = new MySqlCommand(
            "SELECT id, nombre, descripcion, precio, stock FROM productos WHERE id = @id", conexion);
        cmd.Parameters.AddWithValue("@id", id);

        using var reader = cmd.ExecuteReader();
        return reader.Read() ? Mapear(reader) : null;
    }

    public int Insertar(Producto producto)
    {
        using var conexion = _conexion.ObtenerConexion();
        conexion.Open();

        using var cmd = new MySqlCommand(
            "INSERT INTO productos (nombre, descripcion, precio, stock) VALUES (@nombre, @descripcion, @precio, @stock)",
            conexion);
        cmd.Parameters.AddWithValue("@nombre", producto.Nombre);
        cmd.Parameters.AddWithValue("@descripcion", producto.Descripcion);
        cmd.Parameters.AddWithValue("@precio", producto.Precio);
        cmd.Parameters.AddWithValue("@stock", producto.Stock);
        cmd.ExecuteNonQuery();

        return (int)cmd.LastInsertedId;
    }

    public bool Actualizar(Producto producto)
    {
        using var conexion = _conexion.ObtenerConexion();
        conexion.Open();

        using var cmd = new MySqlCommand(
            "UPDATE productos SET nombre = @nombre, descripcion = @descripcion, precio = @precio, stock = @stock WHERE id = @id",
            conexion);
        cmd.Parameters.AddWithValue("@id", producto.Id);
        cmd.Parameters.AddWithValue("@nombre", producto.Nombre);
        cmd.Parameters.AddWithValue("@descripcion", producto.Descripcion);
        cmd.Parameters.AddWithValue("@precio", producto.Precio);
        cmd.Parameters.AddWithValue("@stock", producto.Stock);

        return cmd.ExecuteNonQuery() > 0;
    }

    public bool Eliminar(int id)
    {
        using var conexion = _conexion.ObtenerConexion();
        conexion.Open();

        using var cmd = new MySqlCommand("DELETE FROM productos WHERE id = @id", conexion);
        cmd.Parameters.AddWithValue("@id", id);

        return cmd.ExecuteNonQuery() > 0;
    }

    public List<Producto> Buscar(string nombre)
    {
        var lista = new List<Producto>();

        using var conexion = _conexion.ObtenerConexion();
        conexion.Open();

        using var cmd = new MySqlCommand(
            "SELECT id, nombre, descripcion, precio, stock FROM productos WHERE nombre LIKE @nombre ORDER BY id",
            conexion);
        cmd.Parameters.AddWithValue("@nombre", $"%{nombre}%");

        using var reader = cmd.ExecuteReader();
        while (reader.Read())
            lista.Add(Mapear(reader));

        return lista;
    }
}
