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
                Console.WriteLine("\nPLAN DE CUENTAS\n");
                LibroDiario.ImprimirPlanDeCuentas();

                do
                {
                    debe = 0;
                    haber = 0;

                    codigo = Funcionalidades.Codigo($"\nIngrese el codigo de cuenta del DEBE:");

                    if (!LibroDiario.PlanDeCuentas.ContainsKey(codigo))
                    {
                        Console.WriteLine($"El codigo '{codigo}' no está asociado a ninguna cuenta dentro del Plan de cuentas. Intente nuevamente...");
                        Console.ReadKey();
                    }
                    else
                    {
                        debe = Funcionalidades.NumeroPositivo($"Ingrese el monto de '{LibroDiario.PlanDeCuentas[codigo].Nombre}':");
                        deseaContinuar = Funcionalidades.SoN("Desea agregar mas cuentas dentro del DEBE? (S)i o (N)o");

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
                    debe = 0;       // Reset de variables para evitar acarreo de errores.
                    haber = 0;

                    codigo = Funcionalidades.Codigo($"\nIngrese el codigo de cuenta del HABER:");

                    if (!LibroDiario.PlanDeCuentas.ContainsKey(codigo))
                    {
                        Console.WriteLine($"El codigo '{codigo}' no está asociado a ninguna cuenta dentro del Plan de cuentas. Intente nuevamente...");
                        Console.ReadKey();
                    }
                    else
                    {
                        haber = Funcionalidades.NumeroPositivo($"Ingrese el monto de '{LibroDiario.PlanDeCuentas[codigo].Nombre}':");
                        deseaContinuar = Funcionalidades.SoN("Desea agregar mas cuentas dentro del HABER? (S)i o (N)o");

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
                    Console.WriteLine($"ERROR: El DEBE ({totalDebe}) no es IGUAL al HABER ({totalHaber}). Intente nuevamente...");
                    Console.ReadKey();
                    DebeTemporal.Clear();
                    HaberTemporal.Clear();
                    totalDebe = 0;              // Reset de variables para eliminar acarreo de errores.
                    totalHaber = 0;             // Reset de variables para eliminar acarreo de errores.
                }
                else
                {
                    salir = true;
                    Debe = DebeTemporal;
                    Haber = HaberTemporal;
                }

            } while (salir == false);

            Console.WriteLine($"Se ha creado el asiento Nº{Numero} con exito!");
            Console.ReadKey();
            Console.Clear();
        }

        // Constructor para importar asientos desde Diario.txt
        // Se ejecuta unicamente al inicio.
        public Asientos(int numero, DateTime fecha, Dictionary<int, double> debe, Dictionary<int, double> haber)
        {
            Numero = numero;
            Fecha = fecha;
            Debe = debe;
            Haber = haber;
        }

        public string Serializar()
        {
            // return $"{}";
            string retorno = "";
            int contador = 0;
            string padding = string.Empty;

            foreach (var item in Debe)
            {
                if (contador == 0)
                {
                    // Formato de salida:
                    //                  NroAsiento                   |Fecha  |          CodigoCuenta                |           Debe                         |Haber
                    retorno += $"{Numero.ToString().PadLeft(10, ' ')}|{Fecha}|{item.Key.ToString().PadLeft(12, ' ')}|{item.Value.ToString().PadLeft(10, ' ')}|";
                }
                else
                {
                    // Formato de salida:
                    //                    NroAsiento          |         Fecha             |             CodigoCuenta              |             Debe                        |Haber  
                    retorno += $"\n{padding.PadLeft(10, ' ')}|{padding.PadRight(17, ' ')}|{item.Key.ToString().PadLeft(12, ' ')}|{item.Value.ToString().PadLeft(10, ' ')}|";
                }

                contador++;
            }

            contador = 0;

            foreach (var item in Haber)
            {
                if (contador < Haber.Count)
                {
                    //                  NroAsiento            |      Fecha                |CodigoCuenta                           |         Debe              |         Haber  
                    retorno += $"\n{padding.PadLeft(10, ' ')}|{padding.PadRight(17, ' ')}|{item.Key.ToString().PadLeft(12, ' ')}|{padding.PadLeft(10, ' ')}|{item.Value.ToString().PadLeft(10, ' ')}";
                }
                else
                {
                    //                  NroAsiento            |      Fecha                |         CodigoCuenta                  |         Debe              |         Haber  
                    retorno += $"\n{padding.PadLeft(10, ' ')}|{padding.PadRight(17, ' ')}|{item.Key.ToString().PadLeft(12, ' ')}|{padding.PadLeft(10, ' ')}|{item.Value.ToString().PadLeft(10, ' ')}";
                }

            }

            return retorno;
        }

    }
}



//        public int NroAsiento { get; set; }
//        public string Nombre { get; set; }
//        public int CodigoCuenta { get; set; }
//        public Double Debe { get; set; }
//        public Double Haber { get; set; }

//        public DateTime Fecha { get; set; }


//        public string TituloEntrada => $"{CodigoCuenta}, {Nombre},  - NroAsiento {NroAsiento}";

//        public Asientos() { }
//        public Asientos(string linea)
//        {
//            var datos = linea.Split('|');
//            NroAsiento = int.Parse(datos[0]);
//            Nombre = datos[1];
//            CodigoCuenta = int.Parse(datos[2]);
//            Debe = double.Parse(datos[3]);
//            Haber = double.Parse(datos[4]);
//            Fecha = DateTime.Parse(datos[5]);
//        }

//        public string ObtenerLineaDatos() => $"{NroAsiento};{Nombre};{CodigoCuenta};{Debe};{Haber};{Fecha}";

//        public static Asientos IngresarAsiento()
//        {
//            var asientos = new Asientos();

//            Console.WriteLine("Nuevo asiento");


//            asientos.NroAsiento = IngresarNroAsiento();
//            asientos.Nombre = Ingreso("Ingrese el nombre");
//            asientos.CodigoCuenta = Ingreso("Ingrese el nombre", permiteNumeros: true);
//            asientos.Debe = Ingreso("Ingrese el Debe", permiteNumeros: true);
//            asientos.Haber = Ingreso("Ingrese el Haber", permiteNumeros: true);
//            asientos.Fecha = IngresarFecha("Ingrese la fecha.");


//            return asientos;
//        }

//        private static int IngresarNroAsiento(bool obligatorio = true)
//        {
//            var titulo = "Ingrese el número del asiento";
//            if (!obligatorio)
//            {
//                titulo += " o presione [Enter] para continuar";
//            }

//            do
//            {
//                Console.WriteLine(titulo);
//                var ingreso = Console.ReadLine();
//                if (!obligatorio && string.IsNullOrWhiteSpace(ingreso))
//                {
//                    return 0;
//                }

//                if (!int.TryParse(ingreso, out var NroAsiento))
//                {
//                    Console.WriteLine("No ha ingresado un número valido");
//                    continue;
//                }

//                //if (Agenda.Existe(NroAsiento))
//                //{
//                //    Console.WriteLine("El DNI indicado ya existe en agenda");
//                //    continue;
//                //}

//                return NroAsiento;

//            } while (true);
//        }

//        //public bool CoincideCon(Persona modelo)
//        //{
//        //    if (modelo.DNI != 0 && modelo.DNI != DNI)
//        //    {
//        //        return false;
//        //    }
//        //    if (!string.IsNullOrWhiteSpace(modelo.Apellido) && !Apellido.Equals(modelo.Apellido, StringComparison.CurrentCultureIgnoreCase))
//        //    {
//        //        return false;
//        //    }
//        //    if (!string.IsNullOrWhiteSpace(modelo.Nombre) && !Nombre.Equals(modelo.Nombre, StringComparison.CurrentCultureIgnoreCase))
//        //    {
//        //        return false;
//        //    }
//        //    if (!string.IsNullOrWhiteSpace(modelo.Direccion) && !Direccion.Equals(modelo.Direccion, StringComparison.CurrentCultureIgnoreCase))
//        //    {
//        //        return false;
//        //    }
//        //    if (!string.IsNullOrWhiteSpace(modelo.Telefono) && !Telefono.Equals(modelo.Telefono, StringComparison.CurrentCultureIgnoreCase))
//        //    {
//        //        return false;
//        //    }
//        //    if (modelo.FechaDeNacimiento != DateTime.MinValue && FechaDeNacimiento != modelo.FechaDeNacimiento)
//        //    {
//        //        return false;
//        //    }
//        //    return true;

//        //}

//        //private static DateTime IngresarFecha(string titulo, bool obligatorio = true)
//        //{
//        //    do
//        //    {
//        //        if (!obligatorio)
//        //        {
//        //            titulo += " o presione [Enter para continuar]";
//        //        }

//        //        Console.WriteLine(titulo);

//        //        var ingreso = Console.ReadLine();

//        //        if (!obligatorio && string.IsNullOrWhiteSpace(ingreso))
//        //        {
//        //            return DateTime.MinValue;
//        //        }

//        //        if (!DateTime.TryParse(ingreso, out DateTime fechaNacimiento))
//        //        {
//        //            Console.WriteLine("No es una fecha válida");
//        //            continue;
//        //        }
//        //        if (fechaNacimiento > DateTime.Now)
//        //        {
//        //            Console.WriteLine("La fecha debe ser menor a la actual");
//        //            continue;
//        //        }
//        //        return fechaNacimiento;

//        //    } while (true);

//    }
//}