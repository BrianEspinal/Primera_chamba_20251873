using SistemaPrestamos;

class Program
{
    static readonly GestorBiblioteca gestor = new();

    static void Main() => MenuPrincipal();

    static void MenuPrincipal()
    {
        while (true)
        {
            Console.Clear();
            Encabezado("     SISTEMA DE PRÉSTAMOS DE MATERIALES");
            Console.WriteLine("  [1] Gestión de Materiales");
            Console.WriteLine("  [2] Gestión de Compañeros");
            Console.WriteLine("  [3] Préstamos y Devoluciones");
            Console.WriteLine("  [4] Reportes");
            Console.WriteLine("  [0] Salir");
            Console.Write("\n  Seleccione: ");
            Encabezado("\n");
            Encabezado("\n");
            Encabezado("\n           DEVELOPED BY RACOON_CODE");

            switch (Console.ReadLine()?.Trim())
            {
                case "1": MenuMateriales(); break;
                case "2": MenuPersonas(); break;
                case "3": MenuPrestamos(); break;
                case "4": MenuReportes(); break;
                case "0": return;
                default: Pausa("Opción no válida."); break;
            }
        }
    }

    static void MenuMateriales()
    {
        while (true)
        {
            Console.Clear();
            Encabezado("MATERIALES");
            Console.WriteLine("  [1] Registrar cuaderno");
            Console.WriteLine("  [2] Registrar libro");
            Console.WriteLine("  [3] Registrar herramienta");
            Console.WriteLine("  [4] Listar todos");
            Console.WriteLine("  [5] Buscar por código");
            Console.WriteLine("  [0] Volver");
            Console.Write("\n  Seleccione: ");

            switch (Console.ReadLine()?.Trim())
            {
                case "1": RegistrarCuaderno(); break;
                case "2": RegistrarLibro(); break;
                case "3": RegistrarHerramienta(); break;
                case "4": ListarMateriales(); break;
                case "5": BuscarMaterial(); break;
                case "0": return;
                default: Pausa("Opción no válida."); break;
            }
        }
    }

    static void RegistrarCuaderno()
    {
        Console.Clear(); Encabezado("REGISTRAR CUADERNO");
        try
        {
            string nombre = Leer("Nombre");
            string codigo = Leer("Código único");
            string duenio = Leer("Propietario");
            string materia = Leer("Materia");
            string pags = Leer("Páginas (Enter = 100)");
            int paginas = int.TryParse(pags, out int p) ? p : 100;

            gestor.RegistrarMaterial(new Cuaderno(nombre, codigo, duenio, materia, paginas));
            Pausa($"✔  Cuaderno '{nombre}' registrado.");
        }
        catch (Exception ex) { Pausa($"Error: {ex.Message}"); }
    }

    static void RegistrarLibro()
    {
        Console.Clear(); Encabezado("REGISTRAR LIBRO");
        try
        {
            string nombre = Leer("Nombre");
            string codigo = Leer("Código único");
            string duenio = Leer("Propietario");
            string autor = Leer("Autor");
            string edicion = Leer("Edición (Enter = 1ra)");

            gestor.RegistrarMaterial(new Libro(nombre, codigo, duenio, autor,
                                     string.IsNullOrWhiteSpace(edicion) ? "1ra" : edicion));
            Pausa($"  Libro '{nombre}' registrado.");
        }
        catch (Exception ex) { Pausa($"Error: {ex.Message}"); }
    }

    static void RegistrarHerramienta()
    {
        Console.Clear(); Encabezado("REGISTRAR HERRAMIENTA");
        try
        {
            string nombre = Leer("Nombre");
            string codigo = Leer("Código único");
            string duenio = Leer("Propietario");
            string categoria = Leer("Categoría");

            gestor.RegistrarMaterial(new Herramienta(nombre, codigo, duenio, categoria));
            Pausa($" Herramienta '{nombre}' registrada.");
        }
        catch (Exception ex) { Pausa($"Error: {ex.Message}"); }
    }

    static void ListarMateriales()
    {
        Console.Clear(); Encabezado("LISTADO DE MATERIALES");
        var lista = gestor.ListarMateriales();
        if (lista.Count == 0) { Pausa("No hay materiales registrados."); return; }

        foreach (var m in lista)
        {
            Console.WriteLine($"\n  ID {m.Id:D3} | {m}");
            Console.WriteLine($"         {m.ObtenerDescripcion()}  |  Propietario: {m.Propietario}");
        }
        Pausa();
    }

    static void BuscarMaterial()
    {
        Console.Clear(); Encabezado("BUSCAR MATERIAL");
        var m = gestor.BuscarMaterial(Leer("Código"));
        if (m is null) { Pausa("No encontrado."); return; }
        Console.WriteLine($"\n  {m}\n  {m.ObtenerDescripcion()}\n  Propietario: {m.Propietario}");
        Pausa();
    }

    static void MenuPersonas()
    {
        while (true)
        {
            Console.Clear();
            Encabezado("COMPAÑEROS");
            Console.WriteLine("  [1] Registrar compañero");
            Console.WriteLine("  [2] Listar compañeros");
            Console.WriteLine("  [0] Volver");
            Console.Write("\n  Seleccione: ");

            switch (Console.ReadLine()?.Trim())
            {
                case "1": RegistrarPersona(); break;
                case "2": ListarPersonas(); break;
                case "0": return;
                default: Pausa("Opción no válida."); break;
            }
        }
    }

    static void RegistrarPersona()
    {
        Console.Clear(); Encabezado("REGISTRAR COMPAÑERO");
        try
        {
            string nombre = Leer("Nombre");
            string grado = Leer("Grado / Sección");
            var p = new Persona(nombre, grado);
            gestor.RegistrarPersona(p);
            Pausa($"✔  {nombre} registrado (ID {p.Id}).");
        }
        catch (Exception ex) { Pausa($"Error: {ex.Message}"); }
    }

    static void ListarPersonas()
    {
        Console.Clear(); Encabezado("COMPAÑEROS REGISTRADOS");
        var lista = gestor.ListarPersonas();
        if (lista.Count == 0) { Pausa("No hay compañeros registrados."); return; }
        foreach (var p in lista)
            Console.WriteLine($"  ID {p.Id:D3} | {p}");
        Pausa();
    }

    static void MenuPrestamos()
    {
        while (true)
        {
            Console.Clear();
            Encabezado("PRÉSTAMOS Y DEVOLUCIONES");
            Console.WriteLine("  [1] Nuevo préstamo");
            Console.WriteLine("  [2] Registrar devolución");
            Console.WriteLine("  [3] Ver préstamos activos");
            Console.WriteLine("  [4] Historial de un compañero");
            Console.WriteLine("  [0] Volver");
            Console.Write("\n  Seleccione: ");

            switch (Console.ReadLine()?.Trim())
            {
                case "1": NuevoPrestamo(); break;
                case "2": RegistrarDevolucion(); break;
                case "3": ListarActivos(); break;
                case "4": HistorialPersona(); break;
                case "0": return;
                default: Pausa("Opción no válida."); break;
            }
        }
    }

    static void NuevoPrestamo()
    {
        Console.Clear(); Encabezado("NUEVO PRÉSTAMO");
        try
        {
            foreach (var p in gestor.ListarPersonas())
                Console.WriteLine($"  ID {p.Id:D3} | {p}");

            if (!int.TryParse(Leer("\n  ID del compañero"), out int idP))
            { Pausa("ID no válido."); return; }

            Console.WriteLine();
            foreach (var m in gestor.MaterialesDisponibles())
                Console.WriteLine($"  {m}");

            string cod = Leer("\n  Código del material");
            string nota = Leer("  Nota (Enter para omitir)");

            var pr = gestor.RealizarPrestamo(idP, cod,
                         string.IsNullOrWhiteSpace(nota) ? "" : nota);
            Pausa($" {pr}");
        }
        catch (Exception ex) { Pausa($"Error: {ex.Message}"); }
    }

    static void RegistrarDevolucion()
    {
        Console.Clear(); Encabezado("REGISTRAR DEVOLUCIÓN");
        try
        {
            var activos = gestor.PrestamosActivos();
            if (activos.Count == 0) { Pausa("No hay préstamos activos."); return; }

            foreach (var pr in activos) Console.WriteLine($"  {pr}");

            if (!int.TryParse(Leer("\n  ID del préstamo"), out int idPr))
            { Pausa("ID no válido."); return; }

            string obs = Leer("  Observación (Enter para omitir)");
            var p = gestor.RegistrarDevolucion(idPr,
                        string.IsNullOrWhiteSpace(obs) ? "" : obs);
            Pausa($" +  {p}");
        }
        catch (Exception ex) { Pausa($"Error: {ex.Message}"); }
    }

    static void ListarActivos()
    {
        Console.Clear(); Encabezado("PRÉSTAMOS ACTIVOS");
        var activos = gestor.PrestamosActivos();
        if (activos.Count == 0) { Pausa("No hay préstamos activos."); return; }
        foreach (var pr in activos) Console.WriteLine($"  {pr}");
        Pausa();
    }

    static void HistorialPersona()
    {
        Console.Clear(); Encabezado("HISTORIAL DE COMPAÑERO");
        foreach (var p in gestor.ListarPersonas())
            Console.WriteLine($"  ID {p.Id:D3} | {p}");

        if (!int.TryParse(Leer("\n  ID del compañero"), out int id))
        { Pausa("ID no válido."); return; }

        Console.Clear();
        var per = gestor.BuscarPersona(id);
        Encabezado($"HISTORIAL: {per?.Nombre ?? "N/A"}");
        var hist = gestor.HistorialPersona(id);
        if (hist.Count == 0) { Pausa("Sin préstamos registrados."); return; }
        foreach (var pr in hist) Console.WriteLine($"  {pr}");
        Pausa();
    }

    static void MenuReportes()
    {
        Console.Clear(); Encabezado("REPORTE GENERAL");
        gestor.ImprimirResumen();

        Console.WriteLine("\n  ── Disponibles ─────────────────────────");
        foreach (var m in gestor.MaterialesDisponibles())
            Console.WriteLine($"  • {m}");

        Console.WriteLine("\n  ── Prestados ───────────────────────────");
        foreach (var m in gestor.MaterialesPrestados())
            Console.WriteLine($"  • {m}");

        Console.WriteLine("\n  ── Todos los préstamos ─────────────────");
        foreach (var pr in gestor.ListarPrestamos())
            Console.WriteLine($"  • {pr}");

        Pausa();
    }

    static void Encabezado(string titulo)
    {
        string linea = new string('─', 48);
        Console.WriteLine($"\n  {linea}\n  {titulo}\n  {linea}\n");
    }

    static string Leer(string prompt)
    {
        Console.Write($"  {prompt}: ");
        return Console.ReadLine() ?? "";
    }

    static void Pausa(string msg = "")
    {
        if (!string.IsNullOrWhiteSpace(msg)) Console.WriteLine($"\n  {msg}");
        Console.WriteLine("\n  Presione Enter para continuar...");
        Console.ReadLine();
    }
}