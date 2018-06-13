using EasySQL.Modelos;
using EasySQL.Operaciones.Comandos.MicrosoftSQL;
using EasySQL.Operaciones.Comandos.MySQL;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySQL.Operaciones.Comandos
{
    public class Comando
    {
        public static readonly string[] PARAMS 
            = { "@1param", "@2param", "@3param", "@4param", "@5param" };
        public static readonly string[] TIPOS_CONDICIONES 
            = { "", "=", "<>", ">", "<", ">=", "<=", "LIKE", "IN", "IS NULL", "IS NOT NULL" };
        public static readonly string[] TIPOS_CONDICIONES_UNION = { "", "AND", "OR" };
        public static readonly string[] TIPOS_ORDEN = { "", "ASC", "DESC" };

        public static string[] TiposDatos(Conexion conexionActual)
        {
            if (conexionActual.TipoActual == Conexion.TipoConexion.MicrosoftSQL)
            {
                return ComandoMicrosoftSQL.TIPOS_DATOS;
            }
            else if (conexionActual.TipoActual == Conexion.TipoConexion.MySQL)
            {
                return ComandoMySQL.TIPOS_DATOS;
            }
            else return null;
        }

        public static DbCommand ShowDatabases(Conexion conexionActual)
        {
            if (conexionActual.TipoActual == Conexion.TipoConexion.MicrosoftSQL)
            {
                return ComandoMicrosoftSQL.ShowDatabases();
            }
            else if (conexionActual.TipoActual == Conexion.TipoConexion.MySQL)
            {
                return ComandoMySQL.ShowDatabases();
            }
            else return null;
        }

        public static DbCommand CreateDatabase(Conexion conexionActual)
        {
            if (conexionActual.TipoActual == Conexion.TipoConexion.MicrosoftSQL)
            {
                return ComandoMicrosoftSQL.CreateDatabase();
            }
            else if (conexionActual.TipoActual == Conexion.TipoConexion.MySQL)
            {
                return ComandoMySQL.CreateDatabase();
            }
            else return null;
        }

        public static DbCommand DropDatabase(Conexion conexionActual, bool forzar)
        {
            if (conexionActual.TipoActual == Conexion.TipoConexion.MicrosoftSQL)
            {
                return ComandoMicrosoftSQL.DropDatabase(forzar);
            }
            else if (conexionActual.TipoActual == Conexion.TipoConexion.MySQL)
            {
                return ComandoMySQL.ComandoDropDatabase(forzar);
            }
            else return null;
        }


        public static DbCommand CreateTable(Conexion conexionActual)
        {
            if (conexionActual.TipoActual == Conexion.TipoConexion.MicrosoftSQL)
            {
                return ComandoMicrosoftSQL.CreateTable();
            }
            else if (conexionActual.TipoActual == Conexion.TipoConexion.MySQL)
            {
                return ComandoMySQL.CreateTable();
            }
            else return null;
        }

        public static DbCommand DropTable(Conexion conexionActual)
        {
            if (conexionActual.TipoActual == Conexion.TipoConexion.MicrosoftSQL)
            {
                return ComandoMicrosoftSQL.DropTable();
            }
            else if (conexionActual.TipoActual == Conexion.TipoConexion.MySQL)
            {
                return ComandoMySQL.DropTable();
            }
            else return null;
        }

        public static DbCommand AlterTable(Conexion conexionActual)
        {
            if (conexionActual.TipoActual == Conexion.TipoConexion.MicrosoftSQL)
            {
                return ComandoMicrosoftSQL.AlterTable();
            }
            else if (conexionActual.TipoActual == Conexion.TipoConexion.MySQL)
            {
                return ComandoMySQL.AlterTable();
            }
            else return null;
        }

        public static DbCommand ShowTables(Conexion conexionActual)
        {
            if (conexionActual.TipoActual == Conexion.TipoConexion.MicrosoftSQL)
            {
                return ComandoMicrosoftSQL.ShowTables();
            }
            else if (conexionActual.TipoActual == Conexion.TipoConexion.MySQL)
            {
                return ComandoMySQL.ShowTables();
            }
            else return null;
        }

        public static DbCommand ShowColumnas(Conexion conexionActual)
        {
            if (conexionActual.TipoActual == Conexion.TipoConexion.MicrosoftSQL)
            {
                return ComandoMicrosoftSQL.ShowColumnas();
            }
            else if (conexionActual.TipoActual == Conexion.TipoConexion.MySQL)
            {
                return ComandoMySQL.ShowColumnas();
            }
            else return null;
        }

        public static DbCommand ShowTiposDatosColumnas(Conexion conexionActual)
        {
            if (conexionActual.TipoActual == Conexion.TipoConexion.MicrosoftSQL)
            {
                return ComandoMicrosoftSQL.ShowTiposDatosColumnas();
            }
            else if (conexionActual.TipoActual == Conexion.TipoConexion.MySQL)
            {
                return ComandoMySQL.ShowTiposDatosColumnas();
            }
            else return null;
        }

        public static DbCommand DeleteFrom(Conexion conexionActual)
        {
            if (conexionActual.TipoActual == Conexion.TipoConexion.MicrosoftSQL)
            {
                return ComandoMicrosoftSQL.DeleteFrom();
            }
            else if (conexionActual.TipoActual == Conexion.TipoConexion.MySQL)
            {
                return ComandoMySQL.DeleteFrom();
            }
            else return null;
        }

        public static DbCommand Update(Conexion conexionActual)
        {
            if (conexionActual.TipoActual == Conexion.TipoConexion.MicrosoftSQL)
            {
                return ComandoMicrosoftSQL.Update();
            }
            else if (conexionActual.TipoActual == Conexion.TipoConexion.MySQL)
            {
                return ComandoMySQL.Update();
            }
            else return null;
        }

        public static DbCommand Insert(Conexion conexionActual)
        {
            if (conexionActual.TipoActual == Conexion.TipoConexion.MicrosoftSQL)
            {
                return ComandoMicrosoftSQL.Insert();
            }
            else if (conexionActual.TipoActual == Conexion.TipoConexion.MySQL)
            {
                return ComandoMySQL.Insert();
            }
            else return null;
        }

        public static DbCommand Select(Conexion conexionActual)
        {
            if (conexionActual.TipoActual == Conexion.TipoConexion.MicrosoftSQL)
            {
                return ComandoMicrosoftSQL.Select();
            }
            else if (conexionActual.TipoActual == Conexion.TipoConexion.MySQL)
            {
                return ComandoMySQL.Select();
            }
            else return null;
        }
    }
}
