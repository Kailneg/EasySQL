using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySQL.Modelos
{
    public class CampoOperadorValor
    {
        public string Campo { get; set; }
        public string Operador { get; set; }
        public string Valor { get; set; }

        public CampoOperadorValor(string campo, string operador, string valor)
        {
            Campo = campo;
            Operador = operador;
            Valor = valor;
        }

        public override string ToString()
        {
            return Campo + " " + Operador + " " + Valor;
        }
    }
}
