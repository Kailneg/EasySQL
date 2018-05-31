using EasySQL.Modelos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySQL.Utils
{
    public class Ayudante
    {
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

        public static object ExecuteScalar(Conexion actual, SqlCommand comando)
        {
            if (actual.TipoActual == Conexion.TipoConexion.MicrosoftSQL)
            {
                return AyudanteSQL.ExecuteScalar(actual.CadenaConexion, comando);
            }
            else if (actual.TipoActual == Conexion.TipoConexion.MySQL)
            {
                return AyudanteMySQL.ExecuteScalar(actual.CadenaConexion, comando);
            }
            else return null;
        }

        public static int ExecuteNonQuery(Conexion actual, SqlCommand comando)
        {
            if (actual.TipoActual == Conexion.TipoConexion.MicrosoftSQL)
            {
                return AyudanteSQL.ExecuteNonQuery(actual.CadenaConexion, comando);
            }
            else if (actual.TipoActual == Conexion.TipoConexion.MySQL)
            {
                return AyudanteMySQL.ExecuteNonQuery(actual.CadenaConexion, comando);
            }
            else return -1;
        }

        public static IDataReader ExecuteReader(Conexion actual, SqlCommand comando)
        {
            if (actual.TipoActual == Conexion.TipoConexion.MicrosoftSQL)
            {
                return AyudanteSQL.ExecuteReader(actual.CadenaConexion, comando);
            }
            else if (actual.TipoActual == Conexion.TipoConexion.MySQL)
            {
                return AyudanteMySQL.ExecuteReader(actual.CadenaConexion, comando);
            }
            else return null;
        }
    }
}
