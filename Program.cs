using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A890043.Actividad03
{
    class Program
    {
        static void Main(string[] args)
        {
            string opcion;
            bool salir = false;

            Console.WriteLine("BIENVENIDO AL SISTEMA DE GESTIÓN DE ASIENTOS CONTABLES.\n");
            LibroDiario.IniciarPlanDeCuentas();
            LibroDiario.IniciarDiario();
            Console.ReadKey();

            Console.WriteLine();
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("                  Menú principal                  ");
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("1 - Ingresar asientos contables.");
            Console.WriteLine("2 - Visualizar libro diario.");
            Console.WriteLine("3 - Salir.\n");
            Console.WriteLine("--------------------------------------------------");
            do
            {
                Console.Write("Ingrese una opción del menú y presione [Enter]: ");

                opcion = Console.ReadLine().ToUpper();



                switch (opcion)
                {
                    case "1":
                        LibroDiario.AgregarAsiento();
                        break;

                    case "2":
                        Console.WriteLine("\tLIBRO DIARIO\n");
                        LibroDiario.ImprimirDiario();
                        Console.WriteLine("------ Presione [Enter] para volver al menú ------\n");
                        Console.ReadKey();
                        break;

                    case "3":
                        salir = true;
                        break;

                    default:
                        Console.WriteLine("No ha ingresado una opción del menú. Por favor, intente nuevamente.");
                        Console.WriteLine();
                        break;
                }
            } while (!salir);
        }
    }
}
