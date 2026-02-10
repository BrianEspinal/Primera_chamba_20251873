//Brian Espinal Polanco ---->  2025-1873
Console.WriteLine("===== CALCULADORA DE PROMEDIOS ======");


Console.WriteLine("INGRESE CALIFICACION 1"); //calculadora sencilla 
var num1 = Convert.ToDouble(Console.ReadLine());

Console.WriteLine("INGRESE CALIFICACION 2");
var num2 = Convert.ToDouble(Console.ReadLine());

Console.WriteLine("INGRESE CALIFICACION 3");
var num3 = Convert.ToDouble(Console.ReadLine());

var total = num1 + num2 + num3;// calculamos nuestras notas 
var promedio = total / 3; 

Console.WriteLine($"==== EL PROMEDIO DE LAS NOTAS REGISTRADAS  ES:{promedio:f2} ====");

if (promedio >= 90)
{
    Console.WriteLine("EL ESTUDIANTE APROBO SOBRESALIENTE :0");
}

else if (promedio >= 70)
{
    Console.WriteLine("EL ESTUDIANTE APROBO :D");
}

else
{
    Console.WriteLine("EL ESTUDIANTE REPROBO :(");
}