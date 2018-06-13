using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EasySQL.Operaciones.Operacion
{
    public class OperacionMySQL
    {
        private const string MSJ_ERROR =
            "Error en la operación, compruebe los datos o la conexión con la base de datos.";
        private static MySqlConnection sqlConnReader;

        public static bool ExecuteTest(string cadenaConexion)
        {
            using (MySqlConnection sqlCon = new MySqlConnection(cadenaConexion))
            {
                try
                {
                    // Abre y cierra la conexión, si no salta excepción, la conexión es correcta.
                    sqlCon.Open();
                    sqlCon.Close();
                    return true;
                }
                catch (MySqlException s)
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

        public static object ExecuteScalar(string cadenaConexion, MySqlCommand comando)
        {
            using (MySqlConnection sqlCon = new MySqlConnection(cadenaConexion))
            {
                try
                {
                    // 1. Abre the la conexión
                    sqlCon.Open();
                    comando.Connection = sqlCon;
                    // 2. Ejecuta y devuelve un objeto resultado
                    return comando.ExecuteScalar();
                }
                catch (MySqlException s)
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

        public static int ExecuteNonQuery(string cadenaConexion, MySqlCommand comando)
        {
            using (MySqlConnection sqlCon = new MySqlConnection(cadenaConexion))
            {
                try
                {
                    // 1. Abre the la conexión
                    sqlCon.Open();
                    comando.Connection = sqlCon;
                    // 2. Ejecuta y devuelve el número de filas afectadas
                    return comando.ExecuteNonQuery();
                }
                catch (MySqlException s)
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

        public static MySqlDataReader ExecuteReader(string cadenaConexion, MySqlCommand comando)
        {
            try
            {
                sqlConnReader = new MySqlConnection(cadenaConexion);
                sqlConnReader.Open();
                comando.Connection = sqlConnReader;
                return comando.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            }
            catch (MySqlException s)
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

