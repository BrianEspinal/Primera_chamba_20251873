//Brian Espinal Polanco ---->  2025-1873
Console.Write("Ingrese la temperatura en Celsius: ");
double cel = Convert.ToDouble(Console.ReadLine());

// Convertir a Fahrenheit
double fahren = (cel * 9 / 5) + 32;

// Mostrar resultados
Console.WriteLine("\n--- Conversión de Temperatura ---");
Console.WriteLine($"Temperatura en Celsius: {cel} °C");
Console.WriteLine($"Temperatura en Fahrenheit: {fahren:F2} °F");
    
