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
        public const string PARAM = "@param";
        public const string PARAM2 = "@2param";
        public static readonly string[] TIPOS_OPERADORES 
            = { "=", "<>", ">", "<", ">=", "<=", "LIKE", "IN" };
        public static readonly string[] TIPOS_OPERADORES_UNION = { "AND", "OR" };

        public static string[] TiposDatos(Conexion conexionActual)
        {
            if (conexionActual.TipoActual == Conexion.TipoConexion.MicrosoftSQL)
            {
                return OperacionMicrosoftSQL.TIPOS_DATOS;
            }
            else if (conexionActual.TipoActual == Conexion.TipoConexion.MySQL)
            {
                return null;
            }
            else return null;
        }

        public static DbCommand ComandoShowDatabases(Conexion conexionActual)
        {
            if (conexionActual.TipoActual == Conexion.TipoConexion.MicrosoftSQL)
            {
                return OperacionMicrosoftSQL.ComandoShowDatabases();
            }
            else if (conexionActual.TipoActual == Conexion.TipoConexion.MySQL)
            {
                return null;
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

        public static DbCommand ComandoDropDatabase(Conexion conexionActual, bool forzar)
        {
            if (conexionActual.TipoActual == Conexion.TipoConexion.MicrosoftSQL)
            {
                return OperacionMicrosoftSQL.ComandoDropDatabase(forzar);
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

        public static DbCommand ComandoAlterTable(Conexion conexionActual)
        {
            if (conexionActual.TipoActual == Conexion.TipoConexion.MicrosoftSQL)
            {
                return OperacionMicrosoftSQL.ComandoAlterTable();
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

        public static DbCommand ComandoShowColumnas(Conexion conexionActual)
        {
            if (conexionActual.TipoActual == Conexion.TipoConexion.MicrosoftSQL)
            {
                return OperacionMicrosoftSQL.ComandoShowColumnas();
            }
            else if (conexionActual.TipoActual == Conexion.TipoConexion.MySQL)
            {
                return null;
            }
            else return null;
        }

        public static DbCommand ComandoDeleteFrom(Conexion conexionActual)
        {
            if (conexionActual.TipoActual == Conexion.TipoConexion.MicrosoftSQL)
            {
                return OperacionMicrosoftSQL.ComandoDeleteFrom();
            }
            else if (conexionActual.TipoActual == Conexion.TipoConexion.MySQL)
            {
                return null;
            }
            else return null;
        }
    }
}
