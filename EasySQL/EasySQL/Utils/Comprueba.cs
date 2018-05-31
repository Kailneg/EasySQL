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
        private static readonly int LONGITUD_NOMBRE = 30;
        private static readonly int LONGITUD_DIRECCION = 50;
        private static readonly int LONGITUD_USUARIO = 50;
        private static readonly int LONGITUD_CONTRASENIA = 50;

        public static bool? Nombre(string cadena)
        {
            if (cadena.Length == 0)
                return null;
            else
                return SoloLetras(cadena, LONGITUD_NOMBRE);
        }

        public static bool? Direccion(string cadena)
        {
            if (cadena.Length == 0)
                return null;
            else
                return SoloLetras(cadena, LONGITUD_DIRECCION);
        }

        public static bool Puerto(string cadena)
        {
            if (cadena.Length == 0)
                return true;
            else
                return SoloNumeros(cadena);
        }

        public static bool? Usuario(string cadena)
        {
            if (cadena.Length == 0)
                return null;
            else
                return SoloLetras(cadena, LONGITUD_USUARIO);
        }
        public static bool? Contrasenia(string cadena)
        {
            if (cadena.Length == 0)
                return null;
            else
                return SoloLetras(cadena, LONGITUD_CONTRASENIA);
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
