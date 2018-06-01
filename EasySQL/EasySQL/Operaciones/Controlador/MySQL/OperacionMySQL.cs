using EasySQL.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySQL.Operaciones.Controlador.MySQL
{
    public class OperacionMySQL
    {
        public static List<string> ObtenerBasesDatos(Conexion conexionActual)
        {
			//SqlCommand obtenerBBDDsCmd = new SqlCommand(obtenerBBDDs);
			//List<string> resultados = null;

			//// Obtiene el resultado
			//using (SqlDataReader lector =
			//	AyudanteSQL.ExecuteReader(conexionActual.CadenaConexion, obtenerBBDDsCmd))
			//{
			//	// Si el resultado es nulo, no existen bases de datos.
			//	if (lector != null)
			//	{
			//		resultados = ObtenerListaResultados(lector);
			//	}
			//}
			//return resultados;
            return null;
        }
    }
}
