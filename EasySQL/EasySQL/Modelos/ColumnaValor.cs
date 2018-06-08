using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySQL.Modelos
{
    public class ColumnaValor
    {
        public string Columna { get; set; }
        public string Valor { get; set; }

        public ColumnaValor(string campo, string valor)
        {
            Columna = campo;
            Valor = valor;
        }

        public override string ToString()
        {
            return Columna + ": " + Valor;
        }
    }
}
