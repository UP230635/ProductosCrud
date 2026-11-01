using MYBACK.Models;
namespace MYBACK.Services;

public interface IProductoRepository
{
    List<Producto> ObtenerTodos();
    Producto? ObtenerPorId(int id);
    int Insertar(Producto producto);
    bool Actualizar(Producto producto);
    bool Eliminar(int id);
    List<Producto> Buscar(string nombre);
}
