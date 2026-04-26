using Microsoft.Data.SqlClient;

namespace SistemaPrestamos;

public static class Conexion
{
    private const string _connStr =
        "Server=(localdb)\\MSSQLLocalDB;Database=SistemaPrestamos;Integrated Security=true;";

    public static SqlConnection Abrir()
    {
        var con = new SqlConnection(_connStr);
        con.Open();
        return con;
    }
}
