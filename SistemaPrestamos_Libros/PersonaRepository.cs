using Microsoft.Data.SqlClient;

namespace SistemaPrestamos;

public class PersonaRepository
{
    public void Insertar(Persona p)
    {
        using var con = Conexion.Abrir();
        using var cmd = con.CreateCommand();
        cmd.CommandText = "INSERT INTO Personas (Nombre,Grado) VALUES (@n,@g); SELECT SCOPE_IDENTITY();";
        cmd.Parameters.AddWithValue("@n", p.Nombre);
        cmd.Parameters.AddWithValue("@g", p.Grado);
        p.Id = Convert.ToInt32(cmd.ExecuteScalar());
    }

    public List<Persona> ObtenerTodos()
    {
        var lista = new List<Persona>();
        using var con = Conexion.Abrir();
        using var cmd = con.CreateCommand();
        cmd.CommandText = "SELECT * FROM Personas ORDER BY Id";
        using var r = cmd.ExecuteReader();
        while (r.Read())
        {
            var p = new Persona(r.GetString(r.GetOrdinal("Nombre")),
                                r.GetString(r.GetOrdinal("Grado")));
            p.Id = r.GetInt32(r.GetOrdinal("Id"));
            lista.Add(p);
        }
        return lista;
    }

    public Persona? ObtenerPorId(int id)
    {
        using var con = Conexion.Abrir();
        using var cmd = con.CreateCommand();
        cmd.CommandText = "SELECT * FROM Personas WHERE Id=@id";
        cmd.Parameters.AddWithValue("@id", id);
        using var r = cmd.ExecuteReader();
        if (!r.Read()) return null;
        var p = new Persona(r.GetString(r.GetOrdinal("Nombre")),
                            r.GetString(r.GetOrdinal("Grado")));
        p.Id = r.GetInt32(r.GetOrdinal("Id"));
        return p;
    }
}
