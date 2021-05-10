using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A890043.Actividad03
{
    class Asientos
    {
        public int Numero { get; set; }
        public DateTime Fecha { get; set; }
        public Dictionary<int, double> Debe = new Dictionary<int, double>();
        public Dictionary<int, double> Haber = new Dictionary<int, double>();

        public Asientos(int numero)
        {
            Numero = numero;
            Fecha = DateTime.Now;

            bool salir = false;
            int codigo;
            double debe = 0;
            double haber = 0;
            double totalDebe = 0;
            double totalHaber = 0;

            Dictionary<int, double> DebeTemporal = new Dictionary<int, double>();
            Dictionary<int, double> HaberTemporal = new Dictionary<int, double>();

            do
            {
                bool continuar = true;
                string deseaContinuar;
                Console.WriteLine("\n                                  PLAN DE CUENTAS\n");
                LibroDiario.ImprimirPlanDeCuentas();

                do
                {
                    debe = 0;          
                    haber = 0;

                    codigo = Funcionalidades.Codigo($"\nIngrese el código de cuenta del DEBE:");

                    if (!LibroDiario.PlanDeCuentas.ContainsKey(codigo))
                    {
                        Console.WriteLine($"El codigo '{codigo}' no está asociado a ninguna cuenta dentro del Plan de cuentas. Por favor, intente nuevamente.");
                        Console.ReadKey();
                    }
                    else
                    {
                        debe = Funcionalidades.NumeroPositivo($"Ingrese el monto para el código '{codigo}':");
                        deseaContinuar = Funcionalidades.SeguirIngresando("¿Desea ingresar más cuentas dentro del DEBE?. Ingrese (S)--> SI o (N)--> NO.");

                        if (deseaContinuar == "N")
                        {
                            continuar = false;
                        }

                        DebeTemporal.Add(codigo, debe);
                        totalDebe += debe;
                    }
                } while (continuar == true);

                continuar = true;

                do
                {
                    debe = 0;       
                    haber = 0;

                    codigo = Funcionalidades.Codigo($"\nIngrese el código de cuenta del HABER:");

                    if (!LibroDiario.PlanDeCuentas.ContainsKey(codigo))
                    {
                        Console.WriteLine($"El código '{codigo}' no está asociado a ninguna cuenta dentro del Plan de cuentas. Intente nuevamente...");
                        Console.ReadKey();
                    }
                    else
                    {
                        haber = Funcionalidades.NumeroPositivo($"Ingrese el monto para el código '{codigo}':");
                        deseaContinuar = Funcionalidades.SeguirIngresando("¿Desea ingresar más cuentas dentro del HABER?. Ingrese (S)--> SI o (N)--> NO.");

                        if (deseaContinuar == "N")
                        {
                            continuar = false;
                        }

                        HaberTemporal.Add(codigo, haber);
                        totalHaber += haber;
                    }
                } while (continuar == true);

                if (totalDebe != totalHaber)
                {
                    Console.WriteLine($"ERROR: El DEBE ({totalDebe}) no es IGUAL al HABER ({totalHaber}). Por favor, intente nuevamente.");
                    Console.ReadKey();
                    DebeTemporal.Clear();
                    HaberTemporal.Clear();
                    totalDebe = 0;             
                    totalHaber = 0;            
                }
                else
                {
                    salir = true;
                    Debe = DebeTemporal;
                    Haber = HaberTemporal;
                }

            } while (salir == false);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Se ha creado el asiento Nº{Numero}.");
            Console.ResetColor();
            Console.ReadKey();
            Console.Clear();
        }

        public Asientos(int numero, DateTime fecha, Dictionary<int, double> debe, Dictionary<int, double> haber)
        {
            Numero = numero;
            Fecha = fecha;
            Debe = debe;
            Haber = haber;
        }

        public string Serializar()
        {
            string retorno = "";
            int contador = 0;
            string padding = string.Empty;

            foreach (var item in Debe)
            {
                if (contador == 0)
                {
                    retorno += $"|{Numero.ToString().PadLeft(10, ' ')}|{Fecha} |{item.Key.ToString().PadLeft(12, ' ')}|{item.Value.ToString().PadLeft(13, ' ')}|             |";
                }
                else
                {
                    retorno += $"\n{padding.PadLeft(10, ' ')} |{padding.PadRight(17, ' ')} |{item.Key.ToString().PadLeft(12, ' ')}|{item.Value.ToString().PadLeft(13, ' ')}|                       |";            
                }   

                contador++;
            }

            contador = 0;

            foreach (var item in Haber)
            {
                if (contador < Haber.Count)
                {
                    retorno += $"\n|{Numero.ToString().PadLeft(10, ' ')}|{Fecha} |{item.Key.ToString().PadLeft(12, ' ')}|{padding.PadLeft(13, ' ')}|{item.Value.ToString().PadLeft(10, ' ')}   |"; 
                }
                else
                {

                    retorno += $"\n{padding.PadLeft(10, ' ')} |{padding.PadRight(19, ' ')}|{item.Key.ToString().PadLeft(12, ' ')} |{padding.PadLeft(13, ' ')} |{item.Value.ToString().PadLeft(10, ' ')} |";
                }
            }
            return retorno;
        }
    }
}





