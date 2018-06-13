using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EasySQL.Operaciones.Operacion
{
    public class OperacionSQL
    {
        /// <summary>
        /// Mensaje a devolver en caso que la consulta sea fallida.
        /// </summary>
        private const string MSJ_ERROR =
            "Error en la operación, compruebe los datos o la conexión con la base de datos.";

        /// <summary>
        /// Conexión única para evitar conexiones redundantes al usar ExecuteReader
        /// </summary>
        private static SqlConnection sqlConnReader;

        /// <summary>
        /// Implementa para una conexión SQL Server el método ExecuteTest como se especifica en Operacion.cs
        /// </summary>
        public static bool ExecuteTest(string cadenaConexion)
        {
            using (SqlConnection sqlCon = new SqlConnection(cadenaConexion))
            {
                try
                {
                    // Abre y cierra la conexión, si no salta excepción, la conexión es correcta.
                    sqlCon.Open();
                    sqlCon.Close();
                    return true;
                }
                catch (SqlException s)
                {
                    MessageBox.Show(s.Message);
                    return false;
                }
                catch (Exception s)
                {
                    MessageBox.Show(MSJ_ERROR);
                    Console.WriteLine(s.Message);
                    return false;
                }
            }
        }

        /// <summary>
        /// Implementa para una conexión SQL Server el método ExecuteScalar como se especifica en Operacion.cs
        /// </summary>
        public static object ExecuteScalar(string cadenaConexion, SqlCommand comando)
        {
            using (SqlConnection sqlCon = new SqlConnection(cadenaConexion))
            {
                try
                {
                    // 1. Abre the la conexión
                    sqlCon.Open();
                    comando.Connection = sqlCon;
                    // 2. Ejecuta y devuelve un objeto resultado
                    return comando.ExecuteScalar();
                }
                catch (SqlException s)
                {
                    MessageBox.Show(s.Message);
                    return Operacion.ERROR;
                }
                catch (Exception s)
                {
                    MessageBox.Show(MSJ_ERROR);
                    Console.WriteLine(s.Message);
                    return Operacion.ERROR;
                }
            }
        }

        /// <summary>
        /// Implementa para una conexión SQL Server el método ExecuteNonQuery como se especifica en Operacion.cs
        /// </summary>
        public static int ExecuteNonQuery(string cadenaConexion, SqlCommand comando)
        {
            using (SqlConnection sqlCon = new SqlConnection(cadenaConexion))
            {
                try
                {
                    // 1. Abre the la conexión
                    sqlCon.Open();
                    comando.Connection = sqlCon;
                    // 2. Ejecuta y devuelve el número de filas afectadas
                    return comando.ExecuteNonQuery();
                }
                catch (SqlException s)
                {
                    MessageBox.Show(s.Message);
                    return Operacion.ERROR;
                }
                catch (Exception s)
                {
                    MessageBox.Show(MSJ_ERROR);
                    Console.WriteLine(s.Message);
                    return Operacion.ERROR;
                }
            }
        }

        /// <summary>
        /// Implementa para una conexión SQL Server el método ExecuteReader como se especifica en Operacion.cs
        /// </summary>
        public static SqlDataReader ExecuteReader(string cadenaConexion, SqlCommand comando)
        {
            try
            {
                sqlConnReader = new SqlConnection(cadenaConexion);
                sqlConnReader.Open();
                comando.Connection = sqlConnReader;
                return comando.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            }
            catch (SqlException s)
            {
                MessageBox.Show(s.Message);
                return null;
            }
            catch (Exception s)
            {
                MessageBox.Show(MSJ_ERROR);
                Console.WriteLine(s.Message);
                return null;
            }
        }
    }
}
