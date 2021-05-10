using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace A890043.Actividad03
{
    class LibroDiario
    {
        public static Dictionary<int, Asientos> Diario = new Dictionary<int, Asientos>();
        static string nombreDiario = "Diario.txt";
        static int contadorDeAsientos = 0;

        public static readonly Dictionary<int, Cuentas> PlanDeCuentas = new Dictionary<int, Cuentas>();
        static string nombrePlanDeCuentas = "Plan de cuentas.txt";


        public static void IniciarPlanDeCuentas()
        {
            if (File.Exists(nombrePlanDeCuentas))
            {
                using (var reader = new StreamReader(nombrePlanDeCuentas))     
                {
                    reader.ReadLine();                                            
                    while (!reader.EndOfStream)
                    {
                        var linea = reader.ReadLine();
                        var cuenta = new Cuentas(linea);
                        PlanDeCuentas.Add(cuenta.Codigo, cuenta);
                    }
                }
                Console.WriteLine("El Plan de cuentas se encuentra cargado.");
            }
            else
            {
                Console.WriteLine($"Se ha creado el archivo 'Plan de cuentas' en la ubicación '../bin/Debug' de este proyecto\n");
                Console.ReadKey();

                using (StreamWriter writer = File.CreateText(nombrePlanDeCuentas))
                {
                    writer.Write("Codigo|Nombre|Tipo");

                }
            }
        }

        public static void ImprimirPlanDeCuentas()
        { 
            if (PlanDeCuentas.Count == 0)
            {
                Console.WriteLine("El plan de cuentas no tiene cuentas. Por favor,inserte el archivo de plan de cuentas en la ubicación '../bin/Debug' de este proyecto .");

            }
            else
            {
                foreach (var cuenta in PlanDeCuentas)
                {
                    Console.WriteLine($"{cuenta.Key} | {cuenta.Value.Nombre} | {cuenta.Value.Tipo} ");                 

                }
            }
        }


        public static void IniciarDiario()
        {
            if (File.Exists(nombreDiario))
            {
                using (var reader = new StreamReader(nombreDiario))     
                {
                    int numeroAsiento = 0;
                    DateTime fecha = DateTime.Now;
                    List<string> renglones = new List<string>();

                    reader.ReadLine();

                    while (!reader.EndOfStream)
                    {
                        string linea = reader.ReadLine();
                        renglones.Add(linea);
                    }

                    foreach (var renglon in renglones)
                    {
                        Dictionary<int, double> debeTemporal = new Dictionary<int, double>();
                        Dictionary<int, double> haberTemporal = new Dictionary<int, double>();
                        bool finDeAsientoActual = false;
                        int i = 1;

                        if (char.IsDigit(renglon[0]))                                                                  
                        {
                            var columnas = renglon.Split('|');
                            numeroAsiento = int.Parse(columnas[0]);
                            fecha = DateTime.Parse(columnas[1]);
                            debeTemporal.Add(int.Parse(columnas[2]), double.Parse(columnas[3]));

                            while (finDeAsientoActual == false)                                                        
                            {
                                int renglonSiguiente = renglones.IndexOf(renglon) + i;

                                if (!char.IsDigit(renglones[renglonSiguiente][0]))                                     
                                {
                                    var columnasRenglonSiguiente = renglones[renglonSiguiente].Split('|');

                                    if (!string.IsNullOrWhiteSpace(columnasRenglonSiguiente[3]))                       
                                    {

                                        debeTemporal.Add(int.Parse(columnasRenglonSiguiente[2]), double.Parse(columnasRenglonSiguiente[3]));
                                    }
                                    else
                                    {
                                        if (!char.IsDigit(renglones[renglonSiguiente][0]))
                                        {
                                            haberTemporal.Add(int.Parse(columnasRenglonSiguiente[2]), double.Parse(columnasRenglonSiguiente[4]));
                                        }
                                    }
                                }
                                else
                                {
                                    finDeAsientoActual = true;
                                    break;
                                }

                                i++;

                                if (renglonSiguiente + i > renglones.Count)
                                {
                                    break;
                                }
                            }


                            Asientos asientoImportado = new Asientos(numeroAsiento, fecha, debeTemporal, haberTemporal);
                            reader.Close();
                            contadorDeAsientos++;
                            Diario.Add(asientoImportado.Numero, asientoImportado);
                        }
                    }
                }
            }
            else
            {
               
                Console.WriteLine($"Se ha creado el archivo 'Diario.txt' en la ubicación '.../bin/Debug' de este proyecto\n");
                Console.ReadKey();

                using (StreamWriter writer = File.CreateText(nombreDiario))
                {
                    writer.Write("NroAsiento |      Fecha                |         CodigoCuenta                  |         Debe                              |         Haber             ");
                }             //  012345678901|012345678901234567890|012345678901|0123456789012345|0123456789012345
            }
            Console.WriteLine("Presionar [Enter] para visualizar el menú.");
            GrabarDiario();
        }

    
        public static void AgregarAsiento()
        {
            contadorDeAsientos++;
            Diario.Add(contadorDeAsientos, new Asientos(contadorDeAsientos));
            GrabarDiario();
        }

       
        public static void VisualizarLibroDiario()
        {
            if (Diario.Count == 0)
            {
                Console.WriteLine("No se han ingresado asientos...\n");
            }
            else
            {
                Console.WriteLine("|NroAsiento|       Fecha       |CodigoCuenta|     Debe    |    Haber    |");
                //                                                               0123456789012-0123456789012
                 foreach (var asiento in Diario)
                {
                    Console.WriteLine(asiento.Value.Serializar());
                }
            }


        }

        private static void GrabarDiario()
        {
            using (var writer = new StreamWriter(nombreDiario, append: false))
            {
                writer.WriteLine("|NroAsiento|       Fecha       |CodigoCuenta|     Debe    |    Haber    |");
            
                foreach (var asiento in Diario)
                {
                    writer.WriteLine(asiento.Value.Serializar());
                }
            }
        }
    }
}


