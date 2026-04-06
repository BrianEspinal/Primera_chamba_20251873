// Clase Paciente
class Paciente
{
 // Atributos (Encapsulados)
    public int Id { get; set; }
    public string Nombre { get; set; }
    public int Edad { get; set; }
    public string Diagnostico { get; set; }

 
    public Paciente(int id, string nombre, int edad, string diagnostico)
    {
        Id = id;
        Nombre = nombre;
        Edad = edad;
        Diagnostico = diagnostico;
    }

 // Método para mostrar información
    public void MostrarInfo()
    {
        Console.WriteLine($"ID: {Id} | Nombre: {Nombre} | Edad: {Edad} | Diagnóstico: {Diagnostico}");
    }
}

// Clase Sistema
class SistemaPacientes
{
    private List<Paciente> pacientes = new List<Paciente>();

// Agregar paciente
    public void AgregarPaciente(Paciente p)
    {
        pacientes.Add(p);
        Console.WriteLine("Paciente agregado correctamente.\n");
    }

// Mostrar todos
    public void MostrarPacientes()
    {
        Console.WriteLine("\n--- Lista de Pacientes ---");
        foreach (var p in pacientes)
        {
            p.MostrarInfo();
        }
    }

// Buscar paciente por ID
    public void BuscarPaciente(int id)
    {
        foreach (var p in pacientes)
        {
            if (p.Id == id)
            {
                Console.WriteLine("\nPaciente encontrado:");
                p.MostrarInfo();
                return;
            }
        }
        Console.WriteLine("Paciente no encontrado.");
    }
}

// Programa principal
class Program
{
    static void Main(string[] args)
    {
        SistemaPacientes sistema = new SistemaPacientes();
        int opcion;

        do
        {
            Console.WriteLine("\n--- SISTEMA DE PACIENTES ---");
            Console.WriteLine("1. Agregar paciente");
            Console.WriteLine("2. Mostrar pacientes");
            Console.WriteLine("3. Buscar paciente");
            Console.WriteLine("4. Salir");
            Console.Write("Seleccione una opción: ");
            opcion = int.Parse(Console.ReadLine());

            switch (opcion)
            {
                case 1:
                    Console.Write("ID: ");
                    int id = int.Parse(Console.ReadLine());

                    Console.Write("Nombre: ");
                    string nombre = Console.ReadLine();

                    Console.Write("Edad: ");
                    int edad = int.Parse(Console.ReadLine());

                    Console.Write("Diagnóstico: ");
                    string diag = Console.ReadLine();

                    Paciente nuevo = new Paciente(id, nombre, edad, diag);
                    sistema.AgregarPaciente(nuevo);
                    break;

                case 2:
                    sistema.MostrarPacientes();
                    break;

                case 3:
                    Console.Write("Ingrese ID a buscar: ");
                    int buscarId = int.Parse(Console.ReadLine());
                    sistema.BuscarPaciente(buscarId);
                    break;

                case 4:
                    Console.WriteLine("Saliendo...");
                    break;

                default:
                    Console.WriteLine("Opción inválida.");
                    break;
            }

        } while (opcion != 4);
    }
}
