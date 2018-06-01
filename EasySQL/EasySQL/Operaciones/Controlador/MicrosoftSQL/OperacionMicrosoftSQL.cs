using EasySQL.Modelos;
using EasySQL.Operaciones.Ayudante;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySQL.Operaciones.Controlador.MicrosoftSQL
{
    public class OperacionMicrosoftSQL
    {
        public static readonly string obtenerBBDDs = "SELECT name FROM master.sys.databases";


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
    }
}
