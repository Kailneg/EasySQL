using EasySQL.Modelos;
using EasySQL.Operaciones.Operacion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySQL.Operaciones.Comandos.MicrosoftSQL
{
    public class ComandoMicrosoftSQL
    {
        /// <summary>
        /// Define los tipos de datos disponibles para una BBDD SQL Server
        /// </summary>
        public static readonly string[] TIPOS_DATOS = 
            { "INT", "FLOAT", "NVARCHAR(50)", "NVARCHAR(MAX)", "DATETIME"  };

        /**
         * Las siguientes líneas definen los comandos SQL con la sintaxis específica de SQL Server.
         */
        private const string SHOW_DATABASES = "SELECT name FROM master.sys.databases";
        private const string CREATE_DATABASE = "CREATE DATABASE ";
        private const string DROP_DATABASE_FORCE = "ALTER DATABASE @1param SET SINGLE_USER WITH ROLLBACK IMMEDIATE; DROP DATABASE @1param";
        private const string DROP_DATABASE = "DROP DATABASE ";
        private const string SHOW_TABLES =
            "SELECT TABLE_NAME FROM @1param.INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'";
        private const string SHOW_COLUMNS =
            "SELECT COLUMN_NAME FROM @1param.INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '@2param'";
        private const string SHOW_COLUMNS_DATA_TYPE =
            "SELECT DATA_TYPE FROM @1param.INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '@2param'";
        private const string CREATE_TABLE = "CREATE TABLE ";
        private const string DROP_TABLE = "DROP TABLE ";
        private const string ALTER_TABLE = "ALTER TABLE ";
        private const string DELETE_FROM = "DELETE FROM @1param ";
        private const string UPDATE = "UPDATE @1param SET ";
        private const string INSERT = "INSERT INTO @1param (@2param) VALUES (@3param)";
        private const string SELECT = "SELECT @1param FROM @2param @3param @4param";

        /// <summary>
        /// Devuelve un comando SQL Server construido para mostrar bases de datos, 
        /// como se especifica en Comando.cs.
        /// </summary>
        public static DbCommand ShowDatabases()
        {
            return new SqlCommand(SHOW_DATABASES);
        }

        /// <summary>
        /// Devuelve un comando SQL Server construido para crear bases de datos, 
        /// como se especifica en Comando.cs.
        /// </summary>
        public static DbCommand CreateDatabase()
        {
            return new SqlCommand(CREATE_DATABASE);
        }

        /// <summary>
        /// Devuelve un comando SQL Server construido para eliminar bases de datos, 
        /// como se especifica en Comando.cs.
        /// </summary>
        public static DbCommand DropDatabase(bool forzar)
        {
            if (!forzar)
                return new SqlCommand(DROP_DATABASE);
            else
                return new SqlCommand(DROP_DATABASE_FORCE);
        }

        /// <summary>
        /// Devuelve un comando SQL Server construido para crear tables, 
        /// como se especifica en Comando.cs.
        /// </summary>
        public static DbCommand CreateTable()
        {
            return new SqlCommand(CREATE_TABLE);
        }

        /// <summary>
        /// Devuelve un comando SQL Server construido para borrar tablas, 
        /// como se especifica en Comando.cs.
        /// </summary>
        public static DbCommand DropTable()
        {
            return new SqlCommand(DROP_TABLE);
        }

        /// <summary>
        /// Devuelve un comando SQL Server construido para alterar tablas, 
        /// como se especifica en Comando.cs.
        /// </summary>
        public static DbCommand AlterTable()
        {
            return new SqlCommand(ALTER_TABLE);
        }

        /// <summary>
        /// Devuelve un comando SQL Server construido para mostrar tablas, 
        /// como se especifica en Comando.cs.
        /// </summary>
        public static DbCommand ShowTables()
        {
            return new SqlCommand(SHOW_TABLES);
        }

        /// <summary>
        /// Devuelve un comando SQL Server construido para mostrar columnas de tablas, 
        /// como se especifica en Comando.cs.
        /// </summary>
        public static DbCommand ShowColumnas()
        {
            return new SqlCommand(SHOW_COLUMNS);
        }

        /// <summary>
        /// Devuelve un comando SQL Server construido para mostrar los tipos de las columnas de tablas, 
        /// como se especifica en Comando.cs.
        /// </summary>
        public static DbCommand ShowTiposDatosColumnas()
        {
            return new SqlCommand(SHOW_COLUMNS_DATA_TYPE);
        }

        /// <summary>
        /// Devuelve un comando SQL Server construido para eliminar datos, 
        /// como se especifica en Comando.cs.
        /// </summary>
        public static DbCommand DeleteFrom()
        {
            return new SqlCommand(DELETE_FROM);
        }

        /// <summary>
        /// Devuelve un comando SQL Server construido para actualizar datos, 
        /// como se especifica en Comando.cs.
        /// </summary>
        public static DbCommand Update()
        {
            return new SqlCommand(UPDATE);
        }

        /// <summary>
        /// Devuelve un comando SQL Server construido para añadir datos, 
        /// como se especifica en Comando.cs.
        /// </summary>
        public static DbCommand Insert()
        {
            return new SqlCommand(INSERT);
        }

        /// <summary>
        /// Devuelve un comando SQL Server construido para mostrar datos, 
        /// como se especifica en Comando.cs.
        /// </summary>
        public static DbCommand Select()
        {
            return new SqlCommand(SELECT);
        }
    }
}
