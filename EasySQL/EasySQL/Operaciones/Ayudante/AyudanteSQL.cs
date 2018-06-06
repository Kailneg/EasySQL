using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EasySQL.Operaciones.Ayudante
{
    public class AyudanteSQL
    {
        public const int ERROR = Int32.MinValue;
        private static SqlConnection sqlCon;
        private const string MSJ_ERROR =
            "Error en la operación, compruebe los datos o la conexión con la base de datos.";

        public static bool ExecuteTest(string cadenaConexion)
        {
            using (sqlCon = new SqlConnection(cadenaConexion))
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

        public static object ExecuteScalar(string cadenaConexion, SqlCommand comando)
        {
            using (sqlCon = new SqlConnection(cadenaConexion))
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
                    return ERROR;
                }
                catch (Exception s)
                {
                    MessageBox.Show(MSJ_ERROR);
                    Console.WriteLine(s.Message);
                    return ERROR;
                }
            }
        }

        public static int ExecuteNonQuery(string cadenaConexion, SqlCommand comando)
        {
            using (sqlCon = new SqlConnection(cadenaConexion))
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
                    return ERROR;
                }
                catch (Exception s)
                {
                    MessageBox.Show(MSJ_ERROR);
                    Console.WriteLine(s.Message);
                    return ERROR;
                }
            }
        }

        public static SqlDataReader ExecuteReader(string cadenaConexion, SqlCommand comando)
        {
            try
            {
                SqlConnection sqlCon = new SqlConnection(cadenaConexion);
                sqlCon.Open();
                comando.Connection = sqlCon;
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
