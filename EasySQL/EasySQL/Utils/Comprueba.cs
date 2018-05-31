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
        private static readonly int LONGITUD_USUARIO_PROGRAMA = 30;
        private static readonly int LONGITUD_CONTRASENIA_PROGRAMA = 30;
        private static readonly int LONGITUD_USUARIO_CONEXION = 50;
        private static readonly int LONGITUD_CONTRASENIA_CONEXION = 50;
        private static readonly int LONGITUD_MINIMA = 0;
        private static readonly int LONGITUD_MINIMA_CONTRASENIA_CONEXION = 4;

        public static bool Nombre(string cadena)
        {
            return (cadena.Length > LONGITUD_MINIMA && cadena.Length < LONGITUD_NOMBRE);
        }

        public static bool Direccion(string cadena)
        {
            return (cadena.Length > LONGITUD_MINIMA && cadena.Length < LONGITUD_NOMBRE);
        }

        public static bool? Puerto(string cadena)
        {
            if (cadena.Length == 0)
                return null;
            else
                return SoloNumeros(cadena);
        }

        public static bool? UsuarioPrograma(string cadena)
        {
            if (cadena.Length == 0)
                return null;
            else
                return SoloLetras(cadena, LONGITUD_USUARIO_PROGRAMA);
        }
        
        public static bool? ContraseniaPrograma(string cadena)
        {
            if (cadena.Length == 0)
                return null;
            else
                return SoloLetras(cadena, LONGITUD_CONTRASENIA_PROGRAMA);
        }

        public static bool UsuarioConexion(string cadena)
        {
            return (cadena.Length > LONGITUD_MINIMA && cadena.Length < LONGITUD_USUARIO_CONEXION);
        }

        public static bool? ContraseniaConexion(string cadena)
        {
            if (cadena.Length == 0)
                return null;
            else
                return (cadena.Length > LONGITUD_MINIMA_CONTRASENIA_CONEXION 
                    && cadena.Length < LONGITUD_CONTRASENIA_CONEXION);
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
