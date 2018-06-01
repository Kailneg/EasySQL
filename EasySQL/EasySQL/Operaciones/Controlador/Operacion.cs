using EasySQL.Modelos;
using EasySQL.Operaciones.Ayudante;
using EasySQL.Operaciones.Controlador;
using EasySQL.Operaciones.Controlador.MicrosoftSQL;
using EasySQL.Operaciones.Controlador.MySQL;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySQL.Operaciones.Controlador
{
    public class Operacion
    {
        public static List<string> ObtenerBasesDatos(Conexion conexionActual)
        {
            if (conexionActual.TipoActual == Conexion.TipoConexion.MicrosoftSQL)
            {
                return OperacionMicrosoftSQL.ObtenerBasesDatos(conexionActual);
            }
            else if (conexionActual.TipoActual == Conexion.TipoConexion.MySQL)
            {
                return OperacionMySQL.ObtenerBasesDatos(conexionActual);
            }
            else return null;
        }

        public static DbCommand ComandoCreateDatabase(Conexion conexionActual)
        {
            if (conexionActual.TipoActual == Conexion.TipoConexion.MicrosoftSQL)
            {
                return OperacionMicrosoftSQL.ComandoCreateDatabase();
            }
            else if (conexionActual.TipoActual == Conexion.TipoConexion.MySQL)
            {
                return null;
            }
            else return null;
        }

        public static DbCommand ComandoDropDatabase(Conexion conexionActual)
        {
            if (conexionActual.TipoActual == Conexion.TipoConexion.MicrosoftSQL)
            {
                return OperacionMicrosoftSQL.ComandoDropDatabase();
            }
            else if (conexionActual.TipoActual == Conexion.TipoConexion.MySQL)
            {
                return null;
            }
            else return null;
        }

        public static DbCommand ComandoCreateTable(Conexion conexionActual)
        {
            if (conexionActual.TipoActual == Conexion.TipoConexion.MicrosoftSQL)
            {
                return OperacionMicrosoftSQL.ComandoCreateTable();
            }
            else if (conexionActual.TipoActual == Conexion.TipoConexion.MySQL)
            {
                return null;
            }
            else return null;
        }

        public static DbCommand ComandoDropTable(Conexion conexionActual)
        {
            if (conexionActual.TipoActual == Conexion.TipoConexion.MicrosoftSQL)
            {
                return OperacionMicrosoftSQL.ComandoDropTable();
            }
            else if (conexionActual.TipoActual == Conexion.TipoConexion.MySQL)
            {
                return null;
            }
            else return null;
        }

        public static DbCommand ComandoShowTables(Conexion conexionActual)
        {
            if (conexionActual.TipoActual == Conexion.TipoConexion.MicrosoftSQL)
            {
                return OperacionMicrosoftSQL.ComandoShowTables();
            }
            else if (conexionActual.TipoActual == Conexion.TipoConexion.MySQL)
            {
                return null;
            }
            else return null;
        }
    }
}
