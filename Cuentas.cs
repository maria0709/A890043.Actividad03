using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A890043.Actividad03
{
    class Cuentas
    {
        public int Codigo { get; }
        public string Nombre { get; }
        public string Tipo { get; }


        public Cuentas(int codigo, string nombre, string tipo)
        {
            Codigo = codigo;
            Nombre = nombre;
            Tipo = tipo;
        }

        public Cuentas(string linea)
        {
            var datos = linea.Split('|');
            Codigo = int.Parse(datos[0]);
            Nombre = datos[1];
            Tipo = datos[2];
        }

        public string Serializar()
        {
            return $"{Codigo}      |      {Nombre}|      {Tipo}";
        }
    }
}
