//Brian Espinal Polanco ---->  2025-1873
Console.WriteLine("SALUDOS ESTUDIANTE BIENVENIDO DIGITE SUS DATOS PORFAVOR");


Console.WriteLine("ESCRIBA SU NOMBRE");
var nombre = Console.ReadLine();
Console.WriteLine("ESCRIBA SU EDAD");
var edad = int.Parse(Console.ReadLine());
Console.WriteLine("ESCRIBA LA CARRERA QUE ESTUDIA");
var carrera = Console.ReadLine();
Console.WriteLine("ESCRIBA EL SEMESTRE EN QUE SE ENCUENTRA");
var semes = Console.ReadLine();
Console.Clear();

Console.WriteLine("\n=== RESULTADOS ===");
Console.WriteLine($"NOMBRE: {nombre} ");
Console.WriteLine($"EDAD: {edad} ");
Console.WriteLine($"CARRERA: {carrera} ");
Console.WriteLine($"SEMESTRE: {semes} ");
