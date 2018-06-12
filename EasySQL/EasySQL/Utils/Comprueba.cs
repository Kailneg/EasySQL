using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace EasySQL.Utils
{
    public class Comprueba
    {
        /**
         * En los siguientes campos se definen las longititudes
         * máximas que deberían tener los campos.
         */
        private static readonly int LONGITUD_NOMBRE = 30;
        private static readonly int LONGITUD_DIRECCION = 50;
        private static readonly int LONGITUD_USUARIO_PROGRAMA = 30;
        private static readonly int LONGITUD_CONTRASENIA_PROGRAMA = 30;
        private static readonly int LONGITUD_USUARIO_CONEXION = 50;
        private static readonly int LONGITUD_CONTRASENIA_CONEXION = 50;
        private static readonly int LONGITUD_MINIMA = 0;
        private static readonly int LONGITUD_MINIMA_CONTRASENIA_CONEXION = 3;
        private static readonly string SEPARADOR_SQL = ";";

        /// <summary>
        /// Comprueba si la cadena Nombre tiene el formato correcto.
        /// </summary>
        /// <param name="cadena">La cadena a comprobar.</param>
        /// <returns>True si la cadena tiene el formato correcto.</returns>
        public static bool Nombre(string cadena)
        {
            return (cadena.Length > LONGITUD_MINIMA && cadena.Length < LONGITUD_NOMBRE);
        }

        /// <summary>
        /// Comprueba si la cadena Direccion tiene el formato correcto.
        /// </summary>
        /// <param name="cadena">La cadena a comprobar.</param>
        /// <returns>True si la cadena tiene el formato correcto.</returns>
        public static bool Direccion(string cadena)
        {
            return (cadena.Length > LONGITUD_MINIMA && cadena.Length < LONGITUD_DIRECCION);
        }

        /// <summary>
        /// Comprueba si la cadena Puerto tiene el formato correcto.
        /// </summary>
        /// <param name="cadena">La cadena a comprobar.</param>
        /// <returns>True si la cadena tiene el formato correcto.</returns>
        public static bool? Puerto(string cadena)
        {
            if (cadena.Length == 0)
                return null;
            else
                return SoloNumeros(cadena);
        }

        /// <summary>
        /// Comprueba si la cadena UsuarioPrograma tiene el formato correcto.
        /// </summary>
        /// <param name="cadena">La cadena a comprobar.</param>
        /// <returns>True si la cadena tiene el formato correcto.</returns>
        public static bool? UsuarioPrograma(string cadena)
        {
            if (cadena.Length == 0)
                return null;
            else
                return SoloLetras(cadena, LONGITUD_USUARIO_PROGRAMA);
        }

        /// <summary>
        /// Comprueba si la cadena ContraseniaPrograma tiene el formato correcto.
        /// </summary>
        /// <param name="cadena">La cadena a comprobar.</param>
        /// <returns>True si la cadena tiene el formato correcto.</returns>
        public static bool? ContraseniaPrograma(string cadena)
        {
            if (cadena.Length == 0)
                return null;
            else
                return SoloLetras(cadena, LONGITUD_CONTRASENIA_PROGRAMA);
        }

        /// <summary>
        /// Comprueba si la cadena UsuarioConexion tiene el formato correcto.
        /// </summary>
        /// <param name="cadena">La cadena a comprobar.</param>
        /// <returns>True si la cadena tiene el formato correcto.</returns>
        public static bool UsuarioConexion(string cadena)
        {
            return (cadena.Length > LONGITUD_MINIMA && cadena.Length < LONGITUD_USUARIO_CONEXION);
        }

        /// <summary>
        /// Comprueba si la cadena ContraseniaConexion tiene el formato correcto.
        /// </summary>
        /// <param name="cadena">La cadena a comprobar.</param>
        /// <returns>True si la cadena tiene el formato correcto.</returns>
        public static bool? ContraseniaConexion(string cadena)
        {
            if (cadena.Length == 0)
                return null;
            else
                return (cadena.Length > LONGITUD_MINIMA_CONTRASENIA_CONEXION 
                    && cadena.Length < LONGITUD_CONTRASENIA_CONEXION);
        }

        /// <summary>
        /// Comprueba si la cadena tiene el formato correcto sólo numeros.
        /// </summary>
        /// <param name="cadena">La cadena a comprobar.</param>
        /// <returns>True si la cadena tiene sólo números.</returns>
        public static bool SoloNumeros(string cadena)
        {
            return new Regex("^[0-9]+$").IsMatch(cadena);
        }

        /// <summary>
        /// Comprueba si la cadena tiene el formato correcto sólo letras.
        /// </summary>
        /// <param name="cadena">La cadena a comprobar.</param>
        /// <returns>True si la cadena tiene sólo letras.</returns>
        public static bool SoloLetras(string cadena, int longitudMaxima)
        {
            if (cadena.Length > 0 && cadena.Length <= longitudMaxima)
            {
                return new Regex("^[a-zA-Z]+$").IsMatch(cadena);
            }
            else return false;
        }

        /// <summary>
        /// Comprueba si la cadena contiene un separador de sentencias SQL ";".
        /// </summary>
        /// <param name="cadena">La cadena a comprobar.</param>
        /// <returns>Si lo tiene, el trozo de cadena que exista antes del separador.</returns>
        public static string EliminarResto(string parametro)
        {
            if (ContieneSeparadorSQL(parametro))
            {
                return parametro.Substring(0, parametro.IndexOf(SEPARADOR_SQL) + 1);
            }
            return parametro;
        }

        /// <summary>
        /// Comprueba si la cadena contiene un separador de SQL ";".
        /// </summary>
        /// <param name="parametro">La cadena a comprobar.</param>
        /// <returns>True si contiene un separador de sentencias SQL ";"</returns>
        public static bool ContieneSeparadorSQL(string parametro)
        {
            // Intento inyección SQL
            return parametro.Contains(SEPARADOR_SQL);
        }

    }
}
