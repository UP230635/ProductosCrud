using MySqlConnector;
namespace MYBACK.Services;

public class Conexion
{
    private readonly string _cadena;

    public Conexion(IConfiguration configuration)
    {
        _cadena = configuration.GetConnectionString("MySql")
            ?? throw new InvalidOperationException("Falta la cadena de conexion 'MySql' en appsettings.json");
    }

    public MySqlConnection ObtenerConexion() => new MySqlConnection(_cadena);
}
