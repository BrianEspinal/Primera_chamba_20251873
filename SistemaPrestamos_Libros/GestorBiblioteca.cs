namespace SistemaPrestamos;

public class GestorBiblioteca
{
    private readonly MaterialRepository  _matRepo  = new();
    private readonly PersonaRepository   _perRepo  = new();
    private readonly PrestamoRepository  _preRepo  = new();

    public void RegistrarMaterial(Material m)         => _matRepo.Insertar(m);
    public void RegistrarPersona(Persona p)           => _perRepo.Insertar(p);

    public List<Material> ListarMateriales()          => _matRepo.ObtenerTodos();
    public List<Persona>  ListarPersonas()            => _perRepo.ObtenerTodos();
    public List<Prestamo> ListarPrestamos()           => _preRepo.ObtenerTodos();
    public List<Prestamo> PrestamosActivos()          => _preRepo.ObtenerActivos();
    public List<Prestamo> HistorialPersona(int id)    => _preRepo.ObtenerPorPersona(id);

    public List<Material> MaterialesDisponibles()     => ListarMateriales().FindAll(m => m.Disponible);
    public List<Material> MaterialesPrestados()       => ListarMateriales().FindAll(m => !m.Disponible);

    public Material? BuscarMaterial(string codigo)    => _matRepo.ObtenerPorCodigo(codigo);
    public Persona?  BuscarPersona(int id)            => _perRepo.ObtenerPorId(id);

    public Prestamo RealizarPrestamo(int idPersona, string codigoMaterial, string nota = "")
    {
        var persona  = BuscarPersona(idPersona)       ?? throw new Exception("Persona no encontrada.");
        var material = BuscarMaterial(codigoMaterial)  ?? throw new Exception("Material no encontrado.");

        if (!material.Disponible)
            throw new InvalidOperationException($"'{material.Nombre}' ya está prestado.");

        var prestamo = new Prestamo(persona, material, nota);
        _preRepo.Insertar(prestamo);
        _matRepo.ActualizarDisponible(material.Id, false);
        return prestamo;
    }

    public Prestamo RegistrarDevolucion(int idPrestamo, string observacion = "")
    {
        var activos  = _preRepo.ObtenerActivos();
        var prestamo = activos.Find(p => p.Id == idPrestamo)
                       ?? throw new Exception("Préstamo activo no encontrado.");

        prestamo.RegistrarDevolucion(observacion);
        _preRepo.Devolver(idPrestamo, prestamo.FechaDevol!.Value, observacion);
        _matRepo.ActualizarDisponible(prestamo.MaterialPrest.Id, true);
        return prestamo;
    }

    public void ImprimirResumen()
    {
        var mats    = ListarMateriales();
        var activos = PrestamosActivos();
        Console.WriteLine($"\n  Total materiales : {mats.Count}");
        Console.WriteLine($"  Disponibles      : {mats.FindAll(m =>  m.Disponible).Count}");
        Console.WriteLine($"  Prestados        : {mats.FindAll(m => !m.Disponible).Count}");
        Console.WriteLine($"  Personas         : {ListarPersonas().Count}");
        Console.WriteLine($"  Préstamos activos: {activos.Count}");
    }
}
