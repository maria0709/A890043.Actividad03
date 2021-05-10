using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A890043.Actividad03
{
    class Funcionalidades
    {
        public static double NumeroPositivo(string textoAImprimir)
        {
            double numero;
            bool completo = false;

            do
            {
                Console.WriteLine(textoAImprimir);

                if (!double.TryParse(Console.ReadLine(), out numero))
                {
                    Console.WriteLine("Debe ingresar un número. Por favor, intente nuevamente.");
                }
                else
                {
                    if (numero < 0)
                    {
                        Console.WriteLine("El número ingresado debe ser positivo. Por favor, intente nuevamente.");
                        break;
                    }
                    else
                    {
                        completo = true;
                    }
                }

            } while (completo == false);

            return numero;
        }


        public static int Codigo(string textoAImprimir)
        {
            int numero;
            bool completo = false;

            do
            {
                Console.WriteLine(textoAImprimir);

                if (!int.TryParse(Console.ReadLine(), out numero))
                {
                    Console.WriteLine("Debe ingresar un número. Por favor, intente nuevamente.");
                }
                else
                {
                    if (numero < 0)
                    {
                        Console.WriteLine("El número ingresado debe ser positivo. Por favor, intente nuevamente.");
                    }
                    else if (numero > 999)
                    {
                        Console.WriteLine("El número ingresado no debe tener más de 3 digitos. Por favor, intente nuevamente.");
                    }
                    else
                    {
                        completo = true;
                    }
                }

            } while (completo == false);

            return numero;
        }




        public static string SeguirIngresando(string textoAImprimir)
        {
            bool ok = false;
            string opcionElegida;

            do
            {
                Console.WriteLine(textoAImprimir);
                opcionElegida = Console.ReadLine().ToUpper();

                if (opcionElegida == "S")
                {
                    ok = true;
                }
                else if (opcionElegida == "N")
                {
                    ok = true;
                }
                else
                {
                    Console.WriteLine($"'{opcionElegida}' no es una opcion valida. Por favor, intente nuevamente.");
                }

            } while (ok == false);

            return opcionElegida;
        }
    }
}

