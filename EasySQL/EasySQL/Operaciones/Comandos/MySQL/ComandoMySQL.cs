using EasySQL.Modelos;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySQL.Operaciones.Comandos.MySQL
{
    public class ComandoMySQL
    {
        /// <summary>
        /// Define los tipos de datos disponibles para una BBDD MySQL
        /// </summary>
        public static readonly string[] TIPOS_DATOS =
            { "TINYINT", "INT", "BIGINT", "FLOAT",
            "NVARCHAR(50)", "NVARCHAR(500)", "NVARCHAR(15000)", "DATE", "DATETIME"  };

        /**
         * Las siguientes líneas definen los comandos SQL con la sintaxis específica de MySQL.
         */
        private const string SHOW_DATABASES = "SHOW DATABASES";
        private const string CREATE_DATABASE = "CREATE DATABASE ";
        private const string DROP_DATABASE_FORCE = "ALTER DATABASE @1param SET SINGLE_USER WITH ROLLBACK IMMEDIATE; DROP DATABASE @1param";
        private const string DROP_DATABASE = "DROP DATABASE ";
        private const string SHOW_TABLES = "SHOW TABLES FROM @1param";
        private const string SHOW_COLUMNS =
            "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS " +
            "WHERE TABLE_SCHEMA = '@1param' AND TABLE_NAME = '@2param'";
        private const string SHOW_COLUMNS_DATA_TYPE =
            "SELECT DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS " +
            "WHERE TABLE_SCHEMA = '@1param' AND TABLE_NAME = '@2param'";
        private const string CREATE_TABLE = "CREATE TABLE ";
        private const string DROP_TABLE = "DROP TABLE ";
        private const string ALTER_TABLE = "ALTER TABLE ";
        private const string DELETE_FROM = "DELETE FROM @1param ";
        private const string UPDATE = "UPDATE @1param SET ";
        private const string INSERT = "INSERT INTO @1param (@2param) VALUES (@3param)";
        private const string SELECT = "SELECT @1param FROM @2param @3param @4param";

        /// <summary>
        /// Devuelve un comando MySQL construido para mostrar bases de datos, 
        /// como se especifica en Comando.cs.
        /// </summary>
        public static DbCommand ShowDatabases()
        {
            return new MySqlCommand(SHOW_DATABASES);
        }

        /// <summary>
        /// Devuelve un comando MySQL construido para crear bases de datos, 
        /// como se especifica en Comando.cs.
        /// </summary>
        public static DbCommand CreateDatabase()
        {
            return new MySqlCommand(CREATE_DATABASE);
        }

        /// <summary>
        /// Devuelve un comando MySQL construido para eliminar bases de datos, 
        /// como se especifica en Comando.cs.
        /// </summary>
        public static DbCommand ComandoDropDatabase(bool forzar)
        {
            if (!forzar)
                return new MySqlCommand(DROP_DATABASE);
            else
                return new MySqlCommand(DROP_DATABASE_FORCE);
        }

        /// <summary>
        /// Devuelve un comando MySQL construido para crear tables, 
        /// como se especifica en Comando.cs.
        /// </summary>
        public static DbCommand CreateTable()
        {
            return new MySqlCommand(CREATE_TABLE);
        }

        /// <summary>
        /// Devuelve un comando MySQL construido para borrar tablas, 
        /// como se especifica en Comando.cs.
        /// </summary>
        public static DbCommand DropTable()
        {
            return new MySqlCommand(DROP_TABLE);
        }

        /// <summary>
        /// Devuelve un comando MySQL construido para alterar tablas, 
        /// como se especifica en Comando.cs.
        /// </summary>
        public static DbCommand AlterTable()
        {
            return new MySqlCommand(ALTER_TABLE);
        }

        /// <summary>
        /// Devuelve un comando MySQL construido para mostrar tablas, 
        /// como se especifica en Comando.cs.
        /// </summary>
        public static DbCommand ShowTables()
        {
            return new MySqlCommand(SHOW_TABLES);
        }

        /// <summary>
        /// Devuelve un comando MySQL construido para mostrar columnas de tablas, 
        /// como se especifica en Comando.cs.
        /// </summary>
        public static DbCommand ShowColumnas()
        {
            return new MySqlCommand(SHOW_COLUMNS);
        }

        /// <summary>
        /// Devuelve un comando MySQL construido para mostrar los tipos de las columnas de tablas, 
        /// como se especifica en Comando.cs.
        /// </summary>
        public static DbCommand ShowTiposDatosColumnas()
        {
            return new MySqlCommand(SHOW_COLUMNS_DATA_TYPE);
        }

        /// <summary>
        /// Devuelve un comando MySQL construido para eliminar datos, 
        /// como se especifica en Comando.cs.
        /// </summary>
        public static DbCommand DeleteFrom()
        {
            return new MySqlCommand(DELETE_FROM);
        }

        /// <summary>
        /// Devuelve un comando MySQL construido para actualizar datos, 
        /// como se especifica en Comando.cs.
        /// </summary>
        public static DbCommand Update()
        {
            return new MySqlCommand(UPDATE);
        }

        /// <summary>
        /// Devuelve un comando MySQL construido para añadir datos, 
        /// como se especifica en Comando.cs.
        /// </summary>
        public static DbCommand Insert()
        {
            return new MySqlCommand(INSERT);
        }

        /// <summary>
        /// Devuelve un comando MySQL construido para mostrar datos, 
        /// como se especifica en Comando.cs.
        /// </summary>
        public static DbCommand Select()
        {
            return new MySqlCommand(SELECT);
        }
    }
}
