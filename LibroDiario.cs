﻿using System;
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


        // -----------------------------------METODOS-----------------------------------  //

        // !-------PLAN DE CUENTAS-------! //

        // Busca 'Plan de cuentas.txt' en la ubicación predeterminada.
        // Si no existe, pregunta si se desea importar uno existente o crear uno nuevo.
        // Solo se utiliza una vez al iniciar el programa.
        public static void IniciarPlanDeCuentas()
        {
            //Console.WriteLine("El 'Plan de cuentas' definido se encuentra cargado.");
            if (File.Exists(nombrePlanDeCuentas))
            {
                using (var reader = new StreamReader(nombrePlanDeCuentas))     // Importa el txt (si este existe en la ubicacion por defecto)
                {
                    reader.ReadLine();                                            // Solución muy sucia para ignorar la primera linea del txt.

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
                // Crea 'Plan de cuentas.txt' en la ubicacion 'C:\'
                // Agrega una linea de referencia.
                Console.WriteLine($"Se ha creado el archivo 'Plan de cuentas' en la ubicación '.../bin/Debug' de este proyecto\n");
                Console.ReadKey();

                using (StreamWriter writer = File.CreateText(nombrePlanDeCuentas))
                {
                    writer.Write("Codigo|Nombre|Tipo");
                }
            }
        }

        // Agrega una cuenta a Plan verificando que el codigo para identificarla este disponible.
        //public static void AgregarCuenta()
        //{
        //    int codigo = Funcionalidades.Codigo("Ingrese el codigo de la cuenta nueva");
        //    string nombre = Funcionalidades.Texto("Ingrese el nombre de la cuenta nueva");
        //    string tipo = Funcionalidades.TipoCuenta("Es (A)ctivo, (P)asivo o Patrimonio (N)eto?");
        //    Cuentas nueva = new Cuentas(codigo, nombre, tipo);

        //    if (PlanDeCuentas.ContainsKey(codigo))
        //    {
        //        Console.WriteLine($"Error: El codigo '{codigo}' ya esta siendo utilizado por otra cuenta. Intente con otro codigo...");
        //        Console.ReadKey();
        //    }
        //    else
        //    {
        //        PlanDeCuentas.Add(nueva.Codigo, nueva);
        //        Console.WriteLine($"Se ha agregado la cuenta '{nueva.Nombre}' con el codigo {nueva.Codigo}");
        //        Console.ReadKey();
        //        GrabarPlan();
        //    }

        //}

        ////// Verifica que la cuenta exista en Plan y la elimina.
        ////public static void QuitarCuenta()
        ////{
        ////    int codigo = Funcionalidades.Codigo("Ingrese el codigo de la cuenta a eliminar");

        ////    if (!PlanDeCuentas.TryGetValue(codigo, out var cuentaEliminada))
        ////    {
        ////        Console.WriteLine($"No existe una cuenta con el codigo '{codigo}'. Intente nuevamente...");
        ////        Console.ReadKey();
        ////    }
        ////    else
        ////    {
        ////        PlanDeCuentas.Remove(codigo);
        ////        Console.WriteLine($"Se ha eliminado la cuenta '{cuentaEliminada.Nombre}' con el codigo {cuentaEliminada.Codigo}");
        ////        Console.ReadKey();
        ////        GrabarPlan();
        ////    }
        ////}

        // Print de consola del diccionario Plan.
        public static void ImprimirPlanDeCuentas()
        {
            if (PlanDeCuentas.Count == 0)
            {
                Console.WriteLine("No se han ingresado cuentas...\n");
            }
            else
            {
                foreach (var cuenta in PlanDeCuentas)
                {
                    Console.WriteLine($"{cuenta.Key} | {cuenta.Value.Nombre} | {cuenta.Value.Tipo} ");
                }
            }
        }

        // Guarda los cambios realizados al plan de cuentas en el archivo Plan de cuentas.txt
        //private static void GrabarPlan()
        //{
        //    using (var writer = new StreamWriter(nombrePlanDeCuentas, append: false))
        //    {
        //        writer.WriteLine("Codigo|Nombre|Tipo");

        //        foreach (var cuentas in PlanDeCuentas.Values)
        //        {
        //            var linea = cuentas.Serializar();
        //            writer.WriteLine(linea);
        //        }
        //    }
        //}

        // !-------LIBRO DIARIO-------! //

        // Busca 'Diario.txt' en la ubicación predeterminada.
        // Si no existe, pregunta si se desea importar uno existente o crear uno nuevo.
        // Solo se utiliza una vez al iniciar el programa.
        public static void IniciarDiario()
        {
           // Console.WriteLine($"Buscando 'Diario.txt'...");
            if (File.Exists(nombreDiario))
            {
                using (var reader = new StreamReader(nombreDiario))     // Importa el txt (si este existe en la ubicacion por defecto)
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

                        if (char.IsDigit(renglon[0]))                                                                  // Controla que el primer caracter es un digito (Nº de Asiento)
                        {
                            var columnas = renglon.Split('|');
                            numeroAsiento = int.Parse(columnas[0]);
                            fecha = DateTime.Parse(columnas[1]);
                            debeTemporal.Add(int.Parse(columnas[2]), double.Parse(columnas[3]));

                            while (finDeAsientoActual == false)                                                        // RENGLONSIGUIENTE SE ESTA PASANDO DEL TAMAÑO DE LA LISTA
                            {
                                int renglonSiguiente = renglones.IndexOf(renglon) + i;

                                if (!char.IsDigit(renglones[renglonSiguiente][0]))                                     // Verifica que el renglon siguiente no tenga Nº de asiento
                                {
                                    var columnasRenglonSiguiente = renglones[renglonSiguiente].Split('|');

                                    if (!string.IsNullOrWhiteSpace(columnasRenglonSiguiente[3]))                       // Verifica si corresponde al debe o al haber
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

                        /*if (char.IsDigit(renglon[0]))                                                               // Controla que el primer caracter es un digito (Nº de Asiento)
                        {
                            var columnas = renglon.Split('|');
                            numeroAsiento = int.Parse(columnas[0]);
                            fecha = DateTime.Parse(columnas[1]);
                            debeTemporal.Add(int.Parse(columnas[2]), double.Parse(columnas[3]));

                            if (!char.IsDigit(renglones[renglones.IndexOf(renglon) + 1][0]))                                     // Verifica que el renglon siguiente no tenga Nº de asiento
                            {
                                var columnasRenglonSiguiente = renglones[renglones.IndexOf(renglon) + 1].Split('|');

                                if (!string.IsNullOrWhiteSpace(columnasRenglonSiguiente[3]))                       // Verifica si corresponde al debe o al haber
                                {

                                    debeTemporal.Add(int.Parse(columnasRenglonSiguiente[2]), double.Parse(columnasRenglonSiguiente[3]));
                                }
                                else
                                {
                                    if (!char.IsDigit(renglones[renglones.IndexOf(renglon) + 1][0]))
                                    {
                                        haberTemporal.Add(int.Parse(columnasRenglonSiguiente[2]), double.Parse(columnasRenglonSiguiente[4]));
                                    }
                                }
                            }

                            Asiento asientoImportado = new Asiento(numeroAsiento, fecha, debeTemporal, haberTemporal);
                            reader.Close();
                            contadorDeAsientos++;
                            Diario.Add(asientoImportado.Numero, asientoImportado);
                        }*/
                    }
                }
            }
            else
            {
                // Crea 'Diario.txt' dentro del proyecto.
                // Agrega una linea de referencia.
                Console.WriteLine($"Se ha creado el archivo 'Diario.txt' en la ubicación '.../bin/Debug' de este proyecto\n");
                Console.ReadKey();

                using (StreamWriter writer = File.CreateText(nombreDiario))
                {
                    writer.Write("NroAsiento|      Fecha      |CodigoCuenta|   Debe   |   Haber  ");
                }
            }
            Console.WriteLine("Presionar [Enter] para visualizar el menú.");
            GrabarDiario();
        }

        // Agrega un asiento al libro diario.
        public static void AgregarAsiento()
        {
            contadorDeAsientos++;
            Diario.Add(contadorDeAsientos, new Asientos(contadorDeAsientos));
            GrabarDiario();
        }

        // Print del diccionario Diario.
        public static void VisualizarLibroDiario()
        {
            if (Diario.Count == 0)
            {
                Console.WriteLine("No se han ingresado asientos...\n");
            }
            else
            {
                Console.WriteLine("NroAsiento | Fecha | CodigoCuenta | Debe | Haber");
                foreach (var asiento in Diario)
                {
                    Console.WriteLine(asiento.Value.Serializar());
                }
            }


        }

        // Guarda los cambios en el archivo Diario.txt
        private static void GrabarDiario()
        {
            using (var writer = new StreamWriter(nombreDiario, append: false))
            {
                writer.WriteLine("NroAsiento | Fecha | CodigoCuenta | Debe | Haber");

                foreach (var asiento in Diario)
                {
                    writer.WriteLine(asiento.Value.Serializar());
                }
            }
        }
    }
}
//        private static readonly Dictionary<int, Asientos> entradas;
//        const string nombreArchivo = "Diario.txt";

//        static LibroDiario()
//        {
//            //1) Al inicializar (la primera vez que se utiliza la clase Agenda) cargar los datos a partir de un archivo
//            //File => permite manejar archivos como un todo
//            //StreamReader => leer archivos
//            //Streamwriter => escribir archivos

//            entradas = new Dictionary<int, Asientos>();

//            if (File.Exists(nombreArchivo))
//            {
//                using (var reader = new StreamReader(nombreArchivo))
//                {
//                    while (!reader.EndOfStream)
//                    {
//                        var linea = reader.ReadLine();
//                        var asientos = new Asientos(linea);
//                        entradas.Add(asientos.NroAsiento, asientos);
//                    }
//                }

//            }
//        }
//        public static void Agregar(Asientos asientos)
//        {
//            entradas.Add(asientos.NroAsiento, asientos);
//            Grabar();
//        }

//        //public static void Baja(Persona persona)
//        //{
//        //    entradas.Remove(persona.DNI);
//        //    Grabar();
//        //}

//        public static bool Existe(int dni)
//        {
//            return entradas.ContainsKey(dni);
//        }

//        public static Persona Seleccionar()
//        {
//            var modelo = Persona.CrearModeloBusqueda();
//            foreach (var persona in entradas.Values)
//            {
//                if (persona.CoincideCon(modelo))
//                {
//                    return persona;
//                }
//            }

//            Console.WriteLine("No se ha encontrado una persona que coincida");
//            return null;
//        }

//        public static void Grabar()
//        {
//            using (var writer = new StreamWriter(nombreArchivo, append: false))
//            {
//                foreach (var asientos
//                    in entradas.Values)
//                {
//                    var linea = .ObtenerLineaDatos();
//                    writer.WriteLine(linea);
//                }
//            }
//        }
//    }
//}