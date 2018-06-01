using EasySQL.Modelos;
using EasySQL.Operaciones.Ayudante;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySQL.Operaciones.Controlador.MicrosoftSQL
{
    public class OperacionMicrosoftSQL
    {
        private static readonly string obtenerBBDDs = "SELECT name FROM master.sys.databases";
        private static readonly string createDatabase = "CREATE DATABASE ";
        private static readonly string dropDatabase = "DROP DATABASE ";
        private static readonly string showTables =
            "SELECT TABLE_NAME FROM <DATABASE_NAME>.INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = @param";
        private static readonly string createTable = "CREATE TABLE ";
        private static readonly string dropTable = "DROP TABLE ";

        public static List<string> ObtenerBasesDatos(Conexion conexionActual)
        {
            SqlCommand obtenerBBDDsCmd = new SqlCommand(obtenerBBDDs);
            List<string> resultados = null;

            // Obtiene el resultado
            using (SqlDataReader lector = 
                AyudanteSQL.ExecuteReader(conexionActual.CadenaConexion, obtenerBBDDsCmd))
            {
                // Si el resultado es nulo, no existen bases de datos.
                if (lector != null)
                {
                    resultados = ObtenerListaResultados(lector);
                }
            }
            return resultados;
        }

        private static List<string> ObtenerListaResultados(SqlDataReader lector)
        {
            List<string> resultado = new List<string>();

            while (lector.Read())
            {
                resultado.Add(lector[0].ToString());
            }
            return resultado;
        }

        public static DbCommand ComandoCreateDatabase()
        {
            return new SqlCommand(createDatabase);
        }

        public static DbCommand ComandoDropDatabase()
        {
            return new SqlCommand(dropDatabase);
        }

        public static DbCommand ComandoCreateTable()
        {
            return new SqlCommand(createTable);
        }

        public static DbCommand ComandoDropTable()
        {
            return new SqlCommand(dropTable);
        }

        public static DbCommand ComandoShowTables()
        {
            SqlCommand comando = new SqlCommand(showTables);
            comando.Parameters.Add("@param", SqlDbType.NVarChar);
            return comando;
        }
    }
}
