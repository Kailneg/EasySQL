using EasySQL.Modelos;
using EasySQL.Operaciones.Controlador;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySQL.Operaciones.Ayudante
{
    public static class Ayudante
    {
        public const int ERROR = Int32.MinValue;

        public static bool ExecuteTest(Conexion actual)
        {
            if (actual.TipoActual == Conexion.TipoConexion.MicrosoftSQL)
            {
                return AyudanteSQL.ExecuteTest(actual.CadenaConexion);
            }
            else if (actual.TipoActual == Conexion.TipoConexion.MySQL)
            {
                return AyudanteMySQL.ExecuteTest(actual.CadenaConexion);
            }
            else return false;
        }

        public static object ExecuteScalar(Conexion actual, DbCommand comando)
        {
            if (actual.TipoActual == Conexion.TipoConexion.MicrosoftSQL)
            {
                return AyudanteSQL.ExecuteScalar(actual.CadenaConexion, (SqlCommand) comando);
            }
            else if (actual.TipoActual == Conexion.TipoConexion.MySQL)
            {
                return AyudanteMySQL.ExecuteScalar(actual.CadenaConexion, (SqlCommand)comando);
            }
            else return null;
        }

        public static int ExecuteNonQuery(Conexion actual, DbCommand comando)
        {
            if (actual.TipoActual == Conexion.TipoConexion.MicrosoftSQL)
            {
                return AyudanteSQL.ExecuteNonQuery(actual.CadenaConexion, (SqlCommand)comando);
            }
            else if (actual.TipoActual == Conexion.TipoConexion.MySQL)
            {
                return AyudanteMySQL.ExecuteNonQuery(actual.CadenaConexion, (SqlCommand)comando);
            }
            else return -1;
        }

        public static IDataReader ExecuteReader(Conexion actual, DbCommand comando)
        {
            if (actual.TipoActual == Conexion.TipoConexion.MicrosoftSQL)
            {
                return AyudanteSQL.ExecuteReader(actual.CadenaConexion, (SqlCommand)comando);
            }
            else if (actual.TipoActual == Conexion.TipoConexion.MySQL)
            {
                return AyudanteMySQL.ExecuteReader(actual.CadenaConexion, (SqlCommand)comando);
            }
            else return null;
        }

        /// <summary>
        /// Ejecuta un reader para obtener los nombres de bases de datos.
        /// </summary>
        /// <param name="conexionActual">La conexión activa del momento.</param>
        /// <returns>Un objeto IDataReader con los datos pertinentes</returns>
        public static IDataReader ObtenerReaderBasesDatos(Conexion conexionActual)
        {
            // Obtener comando BBDDs
            DbCommand comando = Operacion.ComandoShowDatabases(conexionActual);
            // Comprobación que evita conexiones redundantes
            if (conexionActual.BaseDatos != null)
                conexionActual.BaseDatos = null;
            return ExecuteReader(conexionActual, comando);
        }

        /// <summary>
        /// Ejecuta un reader para obtener los nombres de tablas.
        /// </summary>
        /// <param name="conexionActual">La conexión activa del momento.</param>
        /// <returns>Un objeto IDataReader con los datos pertinentes</returns>
        public static IDataReader ObtenerReaderTablas(Conexion conexionActual)
        {
            // Obtener comando tablas, reemplazar los parámetros de BBDD y nombreTabla
            DbCommand comando = Operacion.ComandoShowTables(conexionActual);
            comando.CommandText = comando.CommandText.Replace(Operacion.PARAM, conexionActual.BaseDatos);
            return ExecuteReader(conexionActual, comando);
        }

        /// <summary>
        /// Ejecuta un reader para obtener los nombres de columnas.
        /// </summary>
        /// <param name="conexionActual">La conexión activa del momento.</param>
        /// <param name="nombreTabla">Nombre de la tabla de la que se quieren las columnas</param>
        /// <returns>Un objeto IDataReader con los datos pertinentes</returns>
        public static IDataReader ObtenerReaderColumnas(Conexion conexionActual, string nombreTabla)
        {
            // Obtener comando columnas, reemplazar los parámetros de BBDD y nombreTabla
            DbCommand comando = Operacion.ComandoShowColumnas(conexionActual);
            comando.CommandText = comando.CommandText.Replace(Operacion.PARAM, conexionActual.BaseDatos);
            comando.CommandText = comando.CommandText.Replace(Operacion.PARAM2, nombreTabla);
            return ExecuteReader(conexionActual, comando);
        }

        public static List<string> MapearReaderALista(IDataReader lector)
        {
            List<string> resultado = new List<string>();

            if (lector != null)
            {
                using (lector)
                {
                    while (lector.Read())
                    {
                        resultado.Add(lector[0].ToString());
                    }
                    lector.Close();
                }
                lector = null;
            }
            return resultado;
        }
    }
}
