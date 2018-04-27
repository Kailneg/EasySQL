using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EasySQL.Utils
{
    public class Comprueba
    {
        public static bool? Usuario(string cadena)
        {
            if (cadena.Length > 0)
                return SoloLetras(cadena, 12);
            else
                return null;
        }
        public static bool? Contrasenia(string cadena)
        {
            if (cadena.Length > 0)
                return SoloLetras(cadena, 12);
            else
                return null;
        }

        public static bool SoloNumeros(string cadena)
        {
            return new Regex("^[0-9]+$").IsMatch(cadena);
        }

        public static bool SoloLetras(string cadena, int longitudMaxima)
        {
            if (cadena.Length > 0 && cadena.Length <= longitudMaxima)
            {
                return new Regex("^[a-zA-Z]+$").IsMatch(cadena);
            }
            else return false;
        }
    }
}
