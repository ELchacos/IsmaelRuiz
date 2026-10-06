using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsmaelRuiz   
{
    // Ruta B: Monitoreo de Signos y Gravedad
    // El hospital perdió su sistema. El médico necesita saber quién está
    // internado y qué tan grave está cada paciente (nivel de 1 a 5).
    // Solución: dos arreglos paralelos. La posición i de "paciente" y la
    // posición i de "urgencia" pertenecen a la misma persona.
    internal class Program
    {
        // Límite de registros: último dígito de mi carné (8) + 10 = 18
        static readonly int N = 18;
        static string[] paciente = new string[N];
        static int[] urgencia = new int[N];

        // Cuántos pacientes hay guardados ahora mismo.
        // Los pacientes siempre ocupan de la posición 0 a cantidad - 1,
        // así el conteo no se estorba.
        static int cantidad = 0;

        // Pone todo vacío al empezar el programa (inicialización de valores)
        static void InicializarDatos()
        {
            for (int i = 0; i < paciente.Length; i++)
            {
                paciente[i] = "";
                urgencia[i] = 0;
            }
            cantidad = 0;
        }

        // El médico pidió "meter nuevos ingresos".
        // Guardo UN paciente nuevo en la primera posición libre (cantidad).
        static void IngresarPaciente()
        {
            // Control del límite: si ya hay N pacientes no hay más camas
            if (cantidad == N)
            {
                Console.WriteLine("No hay camas disponibles, el limite es " + N);
            }
            else
            {
                Console.Write("Ingrese el nombre del paciente: ");
                paciente[cantidad] = Console.ReadLine();
                int nivel = 0;
                // El nivel solo puede ser de 1 a 5, por eso se repite la
                // pregunta hasta que el dato sea correcto
                while (nivel < 1 || nivel > 5)
                {
                    Console.Write("Ingrese el nivel de urgencia (1 al 5): ");
                    // TryParse no rompe el programa si escribo letras: deja el valor en 0
                    int.TryParse(Console.ReadLine(), out nivel);
                    if (nivel < 1 || nivel > 5)
                    {
                        Console.WriteLine("Nivel incorrecto");
                    }
                }
                urgencia[cantidad] = nivel;
                // Sube el contador para que el próximo paciente use la siguiente posición
                cantidad = cantidad + 1;
                Console.WriteLine("Paciente ingresado");
            }
        }

        // El médico pidió "consultar la lista completa viendo el nombre y
        // su nivel a la par". Recorro solo hasta cantidad para no mostrar
        // casillas vacías.
        static void ReportePacientes()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("****** Reporte de Pacientes ******");
            Console.WriteLine(" Nombre                 Urgencia");
            Console.ForegroundColor = ConsoleColor.White;
            for (int i = 0; i < cantidad; i++)
            {
                Console.WriteLine($" {paciente[i]}        {urgencia[i]}");
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("****** Total de pacientes: " + cantidad);
            Console.ForegroundColor = ConsoleColor.White;
        }

        // El médico pidió "cambiar el nivel de urgencia al instante" cuando
        // un paciente empeora o mejora. Busco al paciente por nombre y solo
        // cambio su urgencia, el nombre se queda igual.
        static void ModificarUrgencia()
        {
            Boolean Encontrado = false;   // me dice si el paciente existe
            Console.WriteLine("Digite el paciente");
            string nomb = Console.ReadLine();
            for (int i = 0; i < cantidad; i++)
            {
                // Paso los dos nombres a minúsculas para que no importe cómo se escribieron
                if (paciente[i].ToLower() == nomb.ToLower())
                {
                    Console.WriteLine($"El paciente {paciente[i]} tiene urgencia {urgencia[i]}");
                    int nivel = 0;
                    // Mismo control de 1 a 5 que al ingresar
                    while (nivel < 1 || nivel > 5)
                    {
                        Console.Write("Digite el nuevo nivel (1 al 5): ");
                        // TryParse no rompe el programa si escribo letras: deja el valor en 0
                        int.TryParse(Console.ReadLine(), out nivel);
                        if (nivel < 1 || nivel > 5)
                        {
                            Console.WriteLine("Nivel incorrecto");
                        }
                    }
                    urgencia[i] = nivel;
                    Encontrado = true;
                    break;   // ya lo encontré, no sigo buscando
                }
            }
            if (Encontrado == false)
            {
                Console.WriteLine("Paciente no encontrado");
            }
        }

        // El médico pidió sacar al paciente "de forma limpia" cuando lo
        // dan de alta o lo trasladan, "sin perder la sincronía de los datos".
        // Para no dejar un hueco, muevo los datos de las posiciones
        // siguientes una posición hacia atrás, en LOS DOS arreglos.
        static void EliminarPaciente()
        {
            Boolean Encontrado = false;
            Console.WriteLine("Digite el paciente");
            string nomb = Console.ReadLine();
            for (int i = 0; i < cantidad; i++)
            {
                // Paso los dos nombres a minúsculas para que no importe cómo se escribieron
                if (paciente[i].ToLower() == nomb.ToLower())
                {
                    // Este for tapa el hueco copiando cada dato de la posición siguiente
                    for (int j = i; j < cantidad - 1; j++)
                    {
                        paciente[j] = paciente[j + 1];
                        urgencia[j] = urgencia[j + 1];
                    }
                    // La última posición quedó repetida, la dejo vacía
                    paciente[cantidad - 1] = "";
                    urgencia[cantidad - 1] = 0;
                    // Hay un paciente menos
                    cantidad = cantidad - 1;
                    Console.WriteLine("Paciente eliminado");
                    Encontrado = true;
                    break;
                }
            }
            if (Encontrado == false)
            {
                Console.WriteLine("Paciente no encontrado");
            }
        }

        // Menú principal: se repite hasta que el usuario elija la opción 5
        static void menu()
        {
            int opcion = 0;
            do
            {
                Console.WriteLine("1. Ingresar paciente");
                Console.WriteLine("2. Reporte de pacientes");
                Console.WriteLine("3. Modificar urgencia");
                Console.WriteLine("4. Eliminar paciente");
                Console.WriteLine("5. Salir");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Ingrese una opción: ");
                // CTryParse no rompe el programa si escribo letras: deja el valor en 0
                int.TryParse(Console.ReadLine(), out opcion);
                Console.ForegroundColor = ConsoleColor.White;
                // Cada opción llama a su propio método
                switch (opcion)
                {
                    case 1:
                        IngresarPaciente();
                        break;
                    case 2:
                        ReportePacientes();
                        break;
                    case 3:
                        ModificarUrgencia();
                        break;
                    case 4:
                        EliminarPaciente();
                        break;
                    case 5:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("opcion incorrecta");
                        break;
                }
            } while (opcion != 5);
        }

        static void Main(string[] args)
        {
            // Primero inicializo los datos y luego muestro el menú
            InicializarDatos();
            menu();
        }
    }
}