using Microsoft.Data.SqlClient;

namespace SistemaPrestamos;

public class MaterialRepository
{
    public void Insertar(Material m)
    {
        using var con = Conexion.Abrir();
        using var cmd = con.CreateCommand();
        cmd.CommandText = @"
            INSERT INTO Materiales (Nombre,Codigo,Tipo,Disponible,Propietario,Materia,Paginas,Autor,Edicion,Categoria)
            VALUES (@n,@c,@t,@d,@p,@ma,@pg,@au,@ed,@ca);
            SELECT SCOPE_IDENTITY();";

        cmd.Parameters.AddWithValue("@n",  m.Nombre);
        cmd.Parameters.AddWithValue("@c",  m.Codigo);
        cmd.Parameters.AddWithValue("@t",  m.ObtenerTipo());
        cmd.Parameters.AddWithValue("@d",  m.Disponible);
        cmd.Parameters.AddWithValue("@p",  m.Propietario);

        if (m is Cuaderno cu)
        {
            cmd.Parameters.AddWithValue("@ma", cu.Materia);
            cmd.Parameters.AddWithValue("@pg", cu.NumeroPaginas);
            cmd.Parameters.AddWithValue("@au", DBNull.Value);
            cmd.Parameters.AddWithValue("@ed", DBNull.Value);
            cmd.Parameters.AddWithValue("@ca", DBNull.Value);
        }
        else if (m is Libro li)
        {
            cmd.Parameters.AddWithValue("@ma", DBNull.Value);
            cmd.Parameters.AddWithValue("@pg", DBNull.Value);
            cmd.Parameters.AddWithValue("@au", li.Autor);
            cmd.Parameters.AddWithValue("@ed", li.Edition);
            cmd.Parameters.AddWithValue("@ca", DBNull.Value);
        }
        else if (m is Herramienta h)
        {
            cmd.Parameters.AddWithValue("@ma", DBNull.Value);
            cmd.Parameters.AddWithValue("@pg", DBNull.Value);
            cmd.Parameters.AddWithValue("@au", DBNull.Value);
            cmd.Parameters.AddWithValue("@ed", DBNull.Value);
            cmd.Parameters.AddWithValue("@ca", h.Categoria);
        }

        m.Id = Convert.ToInt32(cmd.ExecuteScalar());
    }

    public void ActualizarDisponible(int id, bool disponible)
    {
        using var con = Conexion.Abrir();
        using var cmd = con.CreateCommand();
        cmd.CommandText = "UPDATE Materiales SET Disponible=@d WHERE Id=@id";
        cmd.Parameters.AddWithValue("@d",  disponible);
        cmd.Parameters.AddWithValue("@id", id);
        cmd.ExecuteNonQuery();
    }

    public List<Material> ObtenerTodos()
    {
        var lista = new List<Material>();
        using var con = Conexion.Abrir();
        using var cmd = con.CreateCommand();
        cmd.CommandText = "SELECT * FROM Materiales ORDER BY Id";
        using var r = cmd.ExecuteReader();
        while (r.Read()) lista.Add(Mapear(r));
        return lista;
    }

    public Material? ObtenerPorCodigo(string codigo)
    {
        using var con = Conexion.Abrir();
        using var cmd = con.CreateCommand();
        cmd.CommandText = "SELECT * FROM Materiales WHERE Codigo=@c";
        cmd.Parameters.AddWithValue("@c", codigo);
        using var r = cmd.ExecuteReader();
        return r.Read() ? Mapear(r) : null;
    }

    private static Material Mapear(SqlDataReader r)
    {
        string tipo = r.GetString(r.GetOrdinal("Tipo"));
        Material m = tipo switch
        {
            "CUADERNO"    => new Cuaderno(
                                r.GetString(r.GetOrdinal("Nombre")),
                                r.GetString(r.GetOrdinal("Codigo")),
                                r.GetString(r.GetOrdinal("Propietario")),
                                r["Materia"] as string ?? "",
                                r["Paginas"]  is int pg ? pg : 100),
            "LIBRO"       => new Libro(
                                r.GetString(r.GetOrdinal("Nombre")),
                                r.GetString(r.GetOrdinal("Codigo")),
                                r.GetString(r.GetOrdinal("Propietario")),
                                r["Autor"]   as string ?? "",
                                r["Edicion"] as string ?? "1ra"),
            _             => new Herramienta(
                                r.GetString(r.GetOrdinal("Nombre")),
                                r.GetString(r.GetOrdinal("Codigo")),
                                r.GetString(r.GetOrdinal("Propietario")),
                                r["Categoria"] as string ?? ""),
        };
        m.Id         = r.GetInt32(r.GetOrdinal("Id"));
        m.Disponible = r.GetBoolean(r.GetOrdinal("Disponible"));
        return m;
    }
}
