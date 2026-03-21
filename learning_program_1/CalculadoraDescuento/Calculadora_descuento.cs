//Brian Espinal Polanco ---->  2025-1873

string opcion;

do // estructura de control principal
{
    // ponemos la interfaz de la tineda
    Console.WriteLine("Bienvenido a Electronica Espinal");
Console.WriteLine("!TENEMOS GRANDES OFERTAS!");
Console.WriteLine("PRODUCTOS > 340$ USD [30% OFF]");
Console.WriteLine("PRODUCTOS > 120$ USD [20% OFF]");
Console.WriteLine("PRODUCTOS > 60 $ USD [5% OFF]");

Console.WriteLine("Seleccione el producto deseado:");
    var producto = Console.ReadLine();

Console.WriteLine("digite el precio del producto marcado");
    var precio = Convert.ToInt32(Console.ReadLine());
 // marcamos double porque trabajaremos con numeros reales
double descuento = 0;

    // establcemos las condicionales para que el sistema decida la opcion marcada
if (precio >= 340)
{
    descuento = precio * 0.30;}
else if (precio >= 120)
{
    descuento = precio * 0.20;}

else if (precio >= 60)
{
    descuento = precio * 0.5; }

    double total = precio - descuento;

Console.WriteLine($"El precio de {producto} es de:{precio}$ USD");
Console.WriteLine($"este monto aplica para un descuento de {descuento}$ USD");
Console.WriteLine($"precio final mas descuento {total}$ USD");

    // aqui me puse crreativo e hice una condicional que le permita al usuario seguir comprando si desea
    ConsoleKey tecla;

    do {
        // en este caso utilice un comando similar al readline pero en este en vez de leerte un texto te lee una tecla
        Console.WriteLine($"PARA REALIZAR COMPRA PRESIONE [ENTER]");
     tecla = Console.ReadKey().Key;

} while (tecla != ConsoleKey.Enter);
     
    Console.WriteLine("GRACIAS POR SU COMPRA..."); // este forma part estructura de control principal 
    Console.Write("¿Desea continuar? (s/n): ");
    opcion = Console.ReadLine();
    Console.Clear();

} while (opcion == "s");


