using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySQL.Modelos
{
    public class CampoValor
    {
        public string Campo { get; set; }
        public string Valor { get; set; }

        public CampoValor(string campo, string valor)
        {
            Campo = campo;
            Valor = valor;
        }

        public override string ToString()
        {
            return Campo + ": " + Valor;
        }
    }
}
