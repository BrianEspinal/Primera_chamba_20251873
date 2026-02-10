//Brian Espinal Polanco ---> 20251873 --->10/02/2026
//===============================================//

int numero = 0; // declaramos nuestra variable numero fuera del while para evitar posibles bucles
do // aqui comienza nuestro buclq
{
    Console.Write("Ingresa un número: "); //establcemos nuestro 1er estatement
    numero = int.Parse(Console.ReadLine()); //capturamos el valor de la variable numero

    if (numero % 2 == 0) //número módulo 2 es igual a 0 es par
    {
        Console.WriteLine("El número es par."); 
    }
    else
    {
        Console.WriteLine("El número es impar."); // de lo contrario es impar
    }
     
} while (numero % 2 != 0); //mientras el resto sea diferente de 0 el bucle continua 