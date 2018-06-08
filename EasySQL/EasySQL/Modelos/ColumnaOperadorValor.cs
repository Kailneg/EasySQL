using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySQL.Modelos
{
    public class ColumnaOperadorValor
    {
        public string Columna { get; set; }
        public string Operador { get; set; }
        public string Valor { get; set; }

        public ColumnaOperadorValor(string columna, string operador, string valor)
        {
            Columna = columna;
            Operador = operador;
            Valor = valor;
        }

        public override string ToString()
        {
            return Columna + " " + Operador + " " + Valor;
        }
    }
}
