Console.WriteLine("Bienvenido a mi lista de Contactos");

bool runing = true;
List<int> ids = new List<int>();
Dictionary<int, string> names = new Dictionary<int, string>();
Dictionary<int, string> lastnames = new Dictionary<int, string>();
Dictionary<int, string> addresses = new Dictionary<int, string>();
Dictionary<int, string> telephones = new Dictionary<int, string>();
Dictionary<int, string> emails = new Dictionary<int, string>();
Dictionary<int, int> ages = new Dictionary<int, int>();
Dictionary<int, bool> bestFriends = new Dictionary<int, bool>();

while (runing)
{
    Console.WriteLine("\n1. Agregar Contacto     2. Ver Contactos    3. Buscar Contacto     4. Modificar Contacto   5. Eliminar Contacto    6. Salir");
    Console.WriteLine("Digite el número de la opción deseada");

    int typeOption = Convert.ToInt32(Console.ReadLine());

    switch (typeOption)
    {
        case 1:
            AddContact(ids, names, lastnames, addresses, telephones, emails, ages, bestFriends);
            break;

        case 2:
            ShowContacts(ids, names, lastnames, addresses, telephones, emails, ages, bestFriends);
            break;

        case 3:
            SearchContact(ids, names, lastnames, addresses, telephones, emails, ages, bestFriends);
            break;

        case 4:
            ModifyContact(ids, names, lastnames, addresses, telephones, emails, ages, bestFriends);
            break;

        case 5:
            DeleteContact(ids, names, lastnames, addresses, telephones, emails, ages, bestFriends);
            break;

        case 6:
            runing = false;
            Console.WriteLine("¡Hasta luego!");
            break;

        default:
            Console.WriteLine("Opción inválida, intenta de nuevo.");
            break;
    }
}


// ──────────────────────────────────────────────
// MÉTODOS
// ──────────────────────────────────────────────

static void AddContact(List<int> ids, Dictionary<int, string> names, Dictionary<int, string> lastnames,
    Dictionary<int, string> addresses, Dictionary<int, string> telephones,
    Dictionary<int, string> emails, Dictionary<int, int> ages, Dictionary<int, bool> bestFriends)
{
    Console.WriteLine("Digite el nombre de la persona");
    string name = Console.ReadLine();
    Console.WriteLine("Digite el apellido de la persona");
    string lastname = Console.ReadLine();
    Console.WriteLine("Digite la dirección");
    string address = Console.ReadLine();
    Console.WriteLine("Digite el telefono de la persona");
    string phone = Console.ReadLine();
    Console.WriteLine("Digite el email de la persona");
    string email = Console.ReadLine();
    Console.WriteLine("Digite la edad de la persona en números");
    int age = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine("Especifique si es mejor amigo: 1. Si, 2. No");
    bool isBestFriend = Convert.ToInt32(Console.ReadLine()) == 1;

    var id = ids.Count == 0 ? 1 : ids[ids.Count - 1] + 1; // evita IDs repetidos al eliminar
    ids.Add(id);
    names.Add(id, name);
    lastnames.Add(id, lastname);
    addresses.Add(id, address);
    telephones.Add(id, phone);
    emails.Add(id, email);
    ages.Add(id, age);
    bestFriends.Add(id, isBestFriend);

    Console.WriteLine($"Contacto '{name} {lastname}' agregado con éxito.");
}

static void ShowContacts(List<int> ids, Dictionary<int, string> names, Dictionary<int, string> lastnames,
    Dictionary<int, string> addresses, Dictionary<int, string> telephones,
    Dictionary<int, string> emails, Dictionary<int, int> ages, Dictionary<int, bool> bestFriends)
{
    if (ids.Count == 0)
    {
        Console.WriteLine("No hay contactos registrados.");
        return;
    }

    Console.WriteLine($"\n{"ID",-5}{"Nombre",-15}{"Apellido",-15}{"Dirección",-20}{"Teléfono",-15}{"Email",-25}{"Edad",-6}{"Mejor Amigo"}");
    Console.WriteLine(new string('─', 106));

    foreach (var id in ids)
    {
        string bf = bestFriends[id] ? "Sí" : "No";
        Console.WriteLine($"{id,-5}{names[id],-15}{lastnames[id],-15}{addresses[id],-20}{telephones[id],-15}{emails[id],-25}{ages[id],-6}{bf}");
    }
}

static void SearchContact(List<int> ids, Dictionary<int, string> names, Dictionary<int, string> lastnames,
    Dictionary<int, string> addresses, Dictionary<int, string> telephones,
    Dictionary<int, string> emails, Dictionary<int, int> ages, Dictionary<int, bool> bestFriends)
{
    Console.WriteLine("Digite el nombre o apellido a buscar:");
    string query = Console.ReadLine().ToLower();

    bool found = false;

    Console.WriteLine($"\n{"ID",-5}{"Nombre",-15}{"Apellido",-15}{"Dirección",-20}{"Teléfono",-15}{"Email",-25}{"Edad",-6}{"Mejor Amigo"}");
    Console.WriteLine(new string('─', 106));

    foreach (var id in ids)
    {
        if (names[id].ToLower().Contains(query) || lastnames[id].ToLower().Contains(query))
        {
            string bf = bestFriends[id] ? "Sí" : "No";
            Console.WriteLine($"{id,-5}{names[id],-15}{lastnames[id],-15}{addresses[id],-20}{telephones[id],-15}{emails[id],-25}{ages[id],-6}{bf}");
            found = true;
        }
    }

    if (!found)
        Console.WriteLine("No se encontró ningún contacto con ese nombre o apellido.");
}

static void ModifyContact(List<int> ids, Dictionary<int, string> names, Dictionary<int, string> lastnames,
    Dictionary<int, string> addresses, Dictionary<int, string> telephones,
    Dictionary<int, string> emails, Dictionary<int, int> ages, Dictionary<int, bool> bestFriends)
{
    ShowContacts(ids, names, lastnames, addresses, telephones, emails, ages, bestFriends);

    if (ids.Count == 0) return;

    Console.WriteLine("\nDigite el ID del contacto a modificar:");
    int id = Convert.ToInt32(Console.ReadLine());

    if (!ids.Contains(id))
    {
        Console.WriteLine("ID no encontrado.");
        return;
    }

    Console.WriteLine("¿Qué desea modificar?");
    Console.WriteLine("1. Nombre  2. Apellido  3. Dirección  4. Teléfono  5. Email  6. Edad  7. Mejor Amigo");
    int field = Convert.ToInt32(Console.ReadLine());

    switch (field)
    {
        case 1:
            Console.WriteLine("Nuevo nombre:");
            names[id] = Console.ReadLine();
            break;
        case 2:
            Console.WriteLine("Nuevo apellido:");
            lastnames[id] = Console.ReadLine();
            break;
        case 3:
            Console.WriteLine("Nueva dirección:");
            addresses[id] = Console.ReadLine();
            break;
        case 4:
            Console.WriteLine("Nuevo teléfono:");
            telephones[id] = Console.ReadLine();
            break;
        case 5:
            Console.WriteLine("Nuevo email:");
            emails[id] = Console.ReadLine();
            break;
        case 6:
            Console.WriteLine("Nueva edad:");
            ages[id] = Convert.ToInt32(Console.ReadLine());
            break;
        case 7:
            Console.WriteLine("¿Es mejor amigo? 1. Sí  2. No");
            bestFriends[id] = Convert.ToInt32(Console.ReadLine()) == 1;
            break;
        default:
            Console.WriteLine("Campo inválido.");
            return;
    }

    Console.WriteLine("Contacto modificado con éxito.");
}

static void DeleteContact(List<int> ids, Dictionary<int, string> names, Dictionary<int, string> lastnames,
    Dictionary<int, string> addresses, Dictionary<int, string> telephones,
    Dictionary<int, string> emails, Dictionary<int, int> ages, Dictionary<int, bool> bestFriends)
{
    ShowContacts(ids, names, lastnames, addresses, telephones, emails, ages, bestFriends);

    if (ids.Count == 0) return;

    Console.WriteLine("\nDigite el ID del contacto a eliminar:");
    int id = Convert.ToInt32(Console.ReadLine());

    if (!ids.Contains(id))
    {
        Console.WriteLine("ID no encontrado.");
        return;
    }

    Console.WriteLine($"¿Seguro que desea eliminar a '{names[id]} {lastnames[id]}'? 1. Sí  2. No");
    int confirm = Convert.ToInt32(Console.ReadLine());

    if (confirm == 1)
    {
        ids.Remove(id);
        names.Remove(id);
        lastnames.Remove(id);
        addresses.Remove(id);
        telephones.Remove(id);
        emails.Remove(id);
        ages.Remove(id);
        bestFriends.Remove(id);

        Console.WriteLine("Contacto eliminado con éxito.");
    }
    else
    {
        Console.WriteLine("Operación cancelada.");
    }
}