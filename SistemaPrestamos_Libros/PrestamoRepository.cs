using Microsoft.Data.SqlClient;

namespace SistemaPrestamos;

public class PrestamoRepository
{
    private readonly PersonaRepository  _personas  = new();
    private readonly MaterialRepository _materiales = new();

    public void Insertar(Prestamo p)
    {
        using var con = Conexion.Abrir();
        using var cmd = con.CreateCommand();
        cmd.CommandText = @"
            INSERT INTO Prestamos (IdPersona,IdMaterial,FechaPrestamo,Devuelto,Nota)
            VALUES (@ip,@im,@fp,0,@no);
            SELECT SCOPE_IDENTITY();";
        cmd.Parameters.AddWithValue("@ip", p.Prestatario.Id);
        cmd.Parameters.AddWithValue("@im", p.MaterialPrest.Id);
        cmd.Parameters.AddWithValue("@fp", p.FechaPrestamo);
        cmd.Parameters.AddWithValue("@no", (object?)p.NotaOpcional ?? DBNull.Value);
        p.Id = Convert.ToInt32(cmd.ExecuteScalar());
    }

    public void Devolver(int idPrestamo, DateTime fechaDevol, string nota)
    {
        using var con = Conexion.Abrir();
        using var cmd = con.CreateCommand();
        cmd.CommandText = @"
            UPDATE Prestamos
            SET Devuelto=1, FechaDevol=@fd, Nota=@no
            WHERE Id=@id";
        cmd.Parameters.AddWithValue("@fd", fechaDevol);
        cmd.Parameters.AddWithValue("@no", (object?)nota ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@id", idPrestamo);
        cmd.ExecuteNonQuery();
    }

    public List<Prestamo> ObtenerActivos()
    {
        using var con = Conexion.Abrir();
        using var cmd = con.CreateCommand();
        cmd.CommandText = "SELECT * FROM Prestamos WHERE Devuelto=0 ORDER BY Id";
        return Leer(cmd);
    }

    public List<Prestamo> ObtenerTodos()
    {
        using var con = Conexion.Abrir();
        using var cmd = con.CreateCommand();
        cmd.CommandText = "SELECT * FROM Prestamos ORDER BY Id";
        return Leer(cmd);
    }

    public List<Prestamo> ObtenerPorPersona(int idPersona)
    {
        using var con = Conexion.Abrir();
        using var cmd = con.CreateCommand();
        cmd.CommandText = "SELECT * FROM Prestamos WHERE IdPersona=@id ORDER BY Id";
        cmd.Parameters.AddWithValue("@id", idPersona);
        return Leer(cmd);
    }

    private List<Prestamo> Leer(SqlCommand cmd)
    {
        var lista = new List<Prestamo>();
        using var r = cmd.ExecuteReader();
        while (r.Read())
        {
            var persona  = _personas.ObtenerPorId(r.GetInt32(r.GetOrdinal("IdPersona")))!;
            var material = _materiales.ObtenerPorCodigo(
                               ObtenerCodigoPorId(r.GetInt32(r.GetOrdinal("IdMaterial"))))!;

            var p = new Prestamo(persona, material, r["Nota"] as string ?? "");
            p.Id            = r.GetInt32(r.GetOrdinal("Id"));
            p.FechaPrestamo = r.GetDateTime(r.GetOrdinal("FechaPrestamo"));
            if (!r.IsDBNull(r.GetOrdinal("FechaDevol")))
            {
                p.FechaDevol = r.GetDateTime(r.GetOrdinal("FechaDevol"));
                p.Devuelto   = true;
                material.Disponible = true;
            }
            lista.Add(p);
        }
        return lista;
    }

    private static string ObtenerCodigoPorId(int id)
    {
        using var con = Conexion.Abrir();
        using var cmd = con.CreateCommand();
        cmd.CommandText = "SELECT Codigo FROM Materiales WHERE Id=@id";
        cmd.Parameters.AddWithValue("@id", id);
        return cmd.ExecuteScalar()?.ToString() ?? "";
    }
}
