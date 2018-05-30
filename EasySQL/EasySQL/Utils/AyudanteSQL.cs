using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySQL.Utils
{
    public class AyudanteSQL
    {
        public static readonly int ERROR = Int32.MinValue;
        private static SqlConnection sqlCon;

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
                    Console.WriteLine(s);
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
                    Console.WriteLine(s);
                    return ERROR;
                }
            }
        }

        public static SqlDataReader ExecuteReader(string cadenaConexion, SqlCommand comando)
        {
            SqlConnection sqlCon = new SqlConnection(cadenaConexion);
            sqlCon.Open();
            comando.Connection = sqlCon;
            return comando.ExecuteReader();
        }
    }
}
