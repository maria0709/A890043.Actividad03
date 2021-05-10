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

        //internal static string TipoCuenta(string textoAImprimir)
        //{
        //    string ingreso;
        //    string retorno = "";
        //    bool ok = false;

        //    do
        //    {
        //        Console.WriteLine(textoAImprimir);
        //        ingreso = Console.ReadLine().ToUpper();

        //        if (ingreso == "A")
        //        {
        //            Console.WriteLine("ACTIVO");
        //            ok = true;
        //            retorno = "Activo";
        //        }
        //        else if (ingreso == "P")
        //        {
        //            Console.WriteLine("PASIVO");
        //            ok = true;
        //            retorno = "Pasivo";
        //        }
        //        else if (ingreso == "N")
        //        {
        //            Console.WriteLine("PATRIMONIO NETO");
        //            ok = true;
        //            retorno = "PatrimonioNeto";
        //        }
        //        else
        //        {
        //            Console.WriteLine($"La opcion '{ingreso}' no es valida. Por favor, intente nuevamente.");
        //        }
        //    } while (ok == false);

        //    return retorno;
        //}

        // Valida que el codigo sea un int y no tenga mas de 3 cifras.
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

        // Valida que el texto a ingresar no este vacio
        // ni tenga un largo mayor a 40 caracteres.
        //public static string Texto(string textoAImprimir)
        //{
        //    string ingreso;
        //    bool ok = false;

        //    do
        //    {
        //        Console.WriteLine(textoAImprimir);
        //        ingreso = Console.ReadLine();

        //        if (string.IsNullOrWhiteSpace(ingreso))
        //        {
        //            Console.WriteLine("Este campo no puede estar vacio. Por favor, intente nuevamente.");
        //        }
        //        else if (ingreso.Length > 41)
        //        {
        //            Console.WriteLine("Este campo no puede tener una longitud mayor a 40 caracteres. Intente nuevamente...");
        //        }
        //        else
        //        {
        //            ok = true;
        //        }

        //    } while (ok == false);

        //    return ingreso;
        //}


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

