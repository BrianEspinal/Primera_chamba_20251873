namespace SistemaPrestamos;

public class Persona
{
    public int    Id     { get; set; }
    public string Nombre { get; set; }
    public string Grado  { get; set; }

    public Persona(string nombre, string grado)
    {
        Nombre = nombre;
        Grado  = grado;
    }

    public Persona(string nombre) : this(nombre, "Sin especificar") { }

    public override string ToString() => $"{Nombre} (Grado: {Grado})";
}

public class Prestamo
{
    public int       Id            { get; set; }
    public Persona   Prestatario   { get; private set; }
    public Material  MaterialPrest { get; private set; }
    public DateTime  FechaPrestamo { get; set; }
    public DateTime? FechaDevol    { get; set; }
    public bool      Devuelto      { get; set; }
    public string    NotaOpcional  { get; set; }

    public Prestamo(Persona prestatario, Material material, string nota = "")
    {
        Prestatario   = prestatario;
        MaterialPrest = material;
        FechaPrestamo = DateTime.Now;
        Devuelto      = false;
        NotaOpcional  = nota;
        material.Disponible = false;
    }

    public void RegistrarDevolucion()
    {
        FechaDevol           = DateTime.Now;
        Devuelto             = true;
        MaterialPrest.Disponible = true;
    }

    public void RegistrarDevolucion(string observacion)
    {
        RegistrarDevolucion();
        NotaOpcional = observacion;
    }

    public int DiasTranscurridos()
    {
        DateTime fin = Devuelto ? FechaDevol!.Value : DateTime.Now;
        return (fin - FechaPrestamo).Days;
    }

    public override string ToString()
    {
        string estado = Devuelto
            ? $"[*] Devuelto el {FechaDevol:dd /MM / yyyy HH:mm}"
            : $"[*] Pendiente ({DiasTranscurridos()}  día(s)) ";
        return $"Préstamo #{Id:D3} | {MaterialPrest.Nombre} → {Prestatario.Nombre} | {estado}";
    }
}
