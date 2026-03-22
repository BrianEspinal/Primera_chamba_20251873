
Console.WriteLine("╔══════════════════════════════════╗");
Console.WriteLine("║    Bienvenido a tu Agenda POO    ║");
Console.WriteLine("║    developed by: RacoonusCode    ║");
Console.WriteLine("╚══════════════════════════════════╝");
Console.WriteLine("║         Version. 2025-1873       ║");
Console.WriteLine("╚══════════════════════════════════╝");
Agenda agenda = new Agenda();
bool corriendo = true;

while (corriendo)
{
    Console.WriteLine("\n1. Agregar   2. Ver   3. Buscar   4. Modificar   5. Eliminar   6. Salir");
    Console.Write("Opción: ");
    string opcion = Console.ReadLine();

    switch (opcion)
    {
        case "1": agenda.AgregarContacto(); break;
        case "2": agenda.MostrarContactos(); break;
        case "3": agenda.BuscarContacto(); break;
        case "4": agenda.ModificarContacto(); break;
        case "5": agenda.EliminarContacto(); break;
        case "6":
            corriendo = false;
            Console.WriteLine("¡Hasta luego!");
            break;
        default:
            Console.WriteLine("Opción inválida.");
            break;
    }
}

/// 
/// 
/// 
public class Contacto  ///aqui creo la clase Contacto con sus propiedades y un constructor para inicializarlas,
                       ///además de un método ToString para mostrar la información del contacto de forma ordenada, 
                       ///y una propiedad MejorAmigoTexto para mostrar "Sí" o "No" dependiendo del valor de EsMejorAmigo
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public string Direccion { get; set; }
    public string Telefono { get; set; }
    public string Email { get; set; }
    public int Edad { get; set; }
    public bool EsMejorAmigo { get; set; }

    public Contacto(int id, string nombre, string apellido, string direccion,
                    string telefono, string email, int edad, bool esMejorAmigo)
    {
        Id = id;
        Nombre = nombre;
        Apellido = apellido;
        Direccion = direccion;
        Telefono = telefono;
        Email = email;
        Edad = edad;
        EsMejorAmigo = esMejorAmigo;
    }

    public string MejorAmigoTexto => EsMejorAmigo ? "Sí" : "No";

    public override string ToString()
        => $"{Id,-5}{Nombre,-15}{Apellido,-15}{Direccion,-20}" +
           $"{Telefono,-15}{Email,-25}{Edad,-6}{MejorAmigoTexto}";
}

/// 
///  EN LAS LINEAS SIGUIENTES ESTA EL VERDADERO LORE
///  SE CREA LA CLASE AGENDA, QUE CONTIENE UNA LISTA DE CONTACTOS Y LOS MÉTODOS PARA AGREGAR, MOSTRAR, BUSCAR, MODIFICAR Y ELIMINAR CONTACTOS.
public class Agenda
{
    private readonly List<Contacto> _contactos = new List<Contacto>();
    private int _nextId = 1;

    private const string Encabezado =
        "ID   Nombre         Apellido       Dirección           " +
        "Teléfono       Email                    Edad  Mejor Amigo";
    private static readonly string Separador = new string('─', 106);

    public void AgregarContacto()
    {
        Console.Write("Nombre: ");
        string nombre = Console.ReadLine();
        Console.Write("Apellido: ");
        string apellido = Console.ReadLine();
        Console.Write("Dirección: ");
        string direccion = Console.ReadLine();
        Console.Write("Teléfono: ");
        string telefono = Console.ReadLine();
        Console.Write("Email: ");
        string email = Console.ReadLine();
        Console.Write("Edad: ");
        int edad = LeerEntero();
        Console.Write("¿Mejor amigo? 1. Sí  2. No: ");
        bool esMejor = LeerEntero() == 1;

        Contacto c = new Contacto(_nextId++, nombre, apellido,
                                  direccion, telefono, email, edad, esMejor);
        _contactos.Add(c);
        Console.WriteLine("✔ Contacto '" + c.Nombre + " " + c.Apellido + "' agregado.");
    }

    public void MostrarContactos() //agregamos el método MostrarContactos para mostrar la lista de contactos
    {
        if (_contactos.Count == 0)
        {
            Console.WriteLine("No hay contactos registrados.");
            return;
        }
        Console.WriteLine("\n" + Encabezado);
        Console.WriteLine(Separador);
        foreach (Contacto c in _contactos)
            Console.WriteLine(c);
    }

    public void BuscarContacto() //agregamos el método BuscarContacto para buscar por nombre o apellido
    {
        Console.Write("Nombre o apellido a buscar: ");
        string query = Console.ReadLine().ToLower();

        bool encontrado = false;
        Console.WriteLine("\n" + Encabezado);
        Console.WriteLine(Separador);

        foreach (Contacto c in _contactos)
        {
            if (c.Nombre.ToLower().Contains(query) || c.Apellido.ToLower().Contains(query))
            {
                Console.WriteLine(c);
                encontrado = true;
            }
        }
        if (!encontrado)
            Console.WriteLine("Sin resultados.");
    }

    public void ModificarContacto() //agregamos el método ModificarContacto para modificar un contacto existente
    {
        MostrarContactos();
        if (_contactos.Count == 0) return;

        Contacto c = ObtenerPorId("modificar");
        if (c == null) return;

        Console.WriteLine("¿Qué campo modificar?");
        Console.WriteLine("1. Nombre  2. Apellido  3. Dirección  " +
                          "4. Teléfono  5. Email  6. Edad  7. Mejor Amigo");

        switch (LeerEntero())
        {
            case 1: Console.Write("Nuevo nombre: "); c.Nombre = Console.ReadLine(); break;
            //Con el switch-case, dependiendo de la opción que elija el usuario, se le pedirá que ingrese el nuevo valor para ese campo y se actualizará el contacto correspondiente.
            case 2: Console.Write("Nuevo apellido: "); c.Apellido = Console.ReadLine(); break; // utilizo c para acceder al contacto que se va a modificar, y dependiendo del campo que se elija,
                                                                                               // se le pedirá al usuario que ingrese el nuevo valor y se actualizará la propiedad correspondiente del contacto.
            case 3: Console.Write("Nueva dirección: "); c.Direccion = Console.ReadLine(); break;
            case 4: Console.Write("Nuevo teléfono: "); c.Telefono = Console.ReadLine(); break;
            case 5: Console.Write("Nuevo email: "); c.Email = Console.ReadLine(); break;
            case 6: Console.Write("Nueva edad: "); c.Edad = LeerEntero(); break;
            case 7:
                Console.Write("¿Mejor amigo? 1. Sí  2. No: ");
                c.EsMejorAmigo = LeerEntero() == 1;
                break;
            default:
                Console.WriteLine("Campo inválido.");
                return;
        }
        Console.WriteLine("✔ Contacto modificado.");
    }

    public void EliminarContacto() //agregamos el método EliminarContacto para eliminar un contacto existente
    {
        MostrarContactos();
        if (_contactos.Count == 0) return; //utilizo el _ para diferenciar la variable de clase _contactos del método MostrarContactos()

        Contacto c = ObtenerPorId("eliminar"); //la c es la variable que va a almacenar el contacto que se va a eliminar, y el método ObtenerPorId va a pedir el ID del contacto a eliminar y devolver el contacto correspondiente
        if (c == null) return;

        Console.Write("¿Eliminar a '" + c.Nombre + " " + c.Apellido + "'? 1. Sí  2. No: ");
        if (LeerEntero() == 1) //cree el método LeerEntero para validar que el usuario ingrese un número válido, y lo utilizo para leer la opción de eliminar o no el contacto
        {                                         /// en la line 200 se explica el método LeerEntero
            _contactos.Remove(c);
            Console.WriteLine("✔ Contacto eliminado.");
        }
        else
        {
            Console.WriteLine("Operación cancelada.");
        }
    }

    private Contacto ObtenerPorId(string accion)
    {
        Console.Write("ID del contacto a " + accion + ": ");
        int id = LeerEntero();

        foreach (Contacto c in _contactos)
        {
            if (c.Id == id) return c;
        }
        Console.WriteLine("ID no encontrado.");
        return null;
    }

    private static int LeerEntero() ///aqui creo el método LeerEntero para validar que el usuario ingrese un número válido,
                                    ///y lo utilizo en los métodos AgregarContacto, ModificarContacto y EliminarContacto para leer la edad del contacto.///
    {
        int resultado;
        while (!int.TryParse(Console.ReadLine(), out resultado))
            Console.Write("Ingresa un número válido: ");
        return resultado;
    }
}

////   ^_ ^  
///   (o, o)    GRACIAS POR USAR MI AGENDA POO
///   (    ) 
//     " " 