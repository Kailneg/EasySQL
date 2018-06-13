using EasySQL.Modelos;
using EasySQL.Operaciones.Comandos;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySQL.Operaciones.Operacion
{
    public static class Operacion
    {
        /// <summary>
        /// Constante que se devuelve cuando en la operación SQL se ha producido un error
        /// </summary>
        public const int ERROR = Int32.MinValue;

        /// <summary>
        /// Ejecuta un test de conexión correcta contra una conexión dada.
        /// </summary>
        /// <param name="test">La conexión a probar.</param>
        /// <returns>True si el test ha sido correcto.</returns>
        public static bool ExecuteTest(Conexion test)
        {
            if (test.TipoActual == Conexion.TipoConexion.MicrosoftSQL)
            {
                return OperacionSQL.ExecuteTest(test.CadenaConexion);
            }
            else if (test.TipoActual == Conexion.TipoConexion.MySQL)
            {
                return OperacionMySQL.ExecuteTest(test.CadenaConexion);
            }
            else return false;
        }

        /// <summary>
        /// Ejecuta una consulta y devuelve el primer resultado devuelto por la consulta.
        /// </summary>
        /// <param name="actual">La conexión contra la que se realizará la consulta.</param>
        /// <param name="comando">El comando a ejecutar de manera escalar.</param>
        /// <returns>El primer resultado de la consulta.</returns>
        public static object ExecuteScalar(Conexion actual, DbCommand comando)
        {
            if (actual.TipoActual == Conexion.TipoConexion.MicrosoftSQL)
            {
                return OperacionSQL.ExecuteScalar(actual.CadenaConexion, (SqlCommand) comando);
            }
            else if (actual.TipoActual == Conexion.TipoConexion.MySQL)
            {
                return OperacionMySQL.ExecuteScalar(actual.CadenaConexion, (MySqlCommand)comando);
            }
            else return null;
        }

        /// <summary>
        /// Ejecuta una sentencia SQL contra una conexión y devuelve el número de filas afectadas.
        /// </summary>
        /// <param name="actual">La conexión contra la que se ejecutará la sentencia.</param>
        /// <param name="comando">El comando a ejecutar.</param>
        /// <returns>El número de filas afectadas.</returns>
        public static int ExecuteNonQuery(Conexion actual, DbCommand comando)
        {
            if (actual.TipoActual == Conexion.TipoConexion.MicrosoftSQL)
            {
                return OperacionSQL.ExecuteNonQuery(actual.CadenaConexion, (SqlCommand)comando);
            }
            else if (actual.TipoActual == Conexion.TipoConexion.MySQL)
            {
                return OperacionMySQL.ExecuteNonQuery(actual.CadenaConexion, (MySqlCommand)comando);
            }
            else return -1;
        }

        /// <summary>
        /// Ejecuta una sentencia SQL contra una conexión y devuelve un DataReader con el
        /// que iterar sobre los resultados obtenidos.
        /// </summary>
        /// <param name="actual">La conexión contra la que se ejecutará la sentencia.</param>
        /// <param name="comando">El comando a ejecutar.</param>
        /// <returns>Un objeto IDataReader con el que acceder a los datos.</returns>
        public static IDataReader ExecuteReader(Conexion actual, DbCommand comando)
        {
            if (actual.TipoActual == Conexion.TipoConexion.MicrosoftSQL)
            {
                return OperacionSQL.ExecuteReader(actual.CadenaConexion, (SqlCommand)comando);
            }
            else if (actual.TipoActual == Conexion.TipoConexion.MySQL)
            {
                return OperacionMySQL.ExecuteReader(actual.CadenaConexion, (MySqlCommand)comando);
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
            DbCommand comando = Comando.ShowDatabases(conexionActual);
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
            DbCommand comando = Comando.ShowTables(conexionActual);
            comando.CommandText = comando.CommandText.Replace(Comando.PARAMS[0], conexionActual.BaseDatos);
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
            DbCommand comando = Comando.ShowColumnas(conexionActual);
            comando.CommandText = comando.CommandText.Replace(Comando.PARAMS[0], conexionActual.BaseDatos);
            comando.CommandText = comando.CommandText.Replace(Comando.PARAMS[1], nombreTabla);
            return ExecuteReader(conexionActual, comando);
        }

        /// <summary>
        /// Ejecuta un reader para obtener los nombres de columnas.
        /// </summary>
        /// <param name="conexionActual">La conexión activa del momento.</param>
        /// <param name="nombreTabla">Nombre de la tabla de la que se quieren las columnas</param>
        /// <returns>Un objeto IDataReader con los datos pertinentes</returns>
        public static IDataReader ObtenerReaderTiposDatosColumnas(Conexion conexionActual, string nombreTabla)
        {
            // Obtener comando columnas, reemplazar los parámetros de BBDD y nombreTabla
            DbCommand comando = Comando.ShowTiposDatosColumnas(conexionActual);
            comando.CommandText = comando.CommandText.Replace(Comando.PARAMS[0], conexionActual.BaseDatos);
            comando.CommandText = comando.CommandText.Replace(Comando.PARAMS[1], nombreTabla);
            return ExecuteReader(conexionActual, comando);
        }

        /// <summary>
        /// Extrae los datos de la primera columna encontrada en un DataReader
        /// y los añade a una lista de string.
        /// </summary>
        /// <param name="lector">El DataReader donde extraer los datos.</param>
        /// <returns>Una lista de string con los datos de la primera columna del DataReader.</returns>
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
