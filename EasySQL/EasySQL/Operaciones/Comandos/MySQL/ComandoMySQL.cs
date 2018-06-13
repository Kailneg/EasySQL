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
        public static readonly string[] TIPOS_DATOS =
            { "INT", "FLOAT", "NVARCHAR(50)", "NVARCHAR(500)", "DATE"  };
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

        public static DbCommand ShowDatabases()
        {
            return new MySqlCommand(SHOW_DATABASES);
        }

        public static DbCommand CreateDatabase()
        {
            return new MySqlCommand(CREATE_DATABASE);
        }

        public static DbCommand ComandoDropDatabase(bool forzar)
        {
            if (!forzar)
                return new MySqlCommand(DROP_DATABASE);
            else
                return new MySqlCommand(DROP_DATABASE_FORCE);
        }

        public static DbCommand CreateTable()
        {
            return new MySqlCommand(CREATE_TABLE);
        }

        public static DbCommand DropTable()
        {
            return new MySqlCommand(DROP_TABLE);
        }

        public static DbCommand AlterTable()
        {
            return new MySqlCommand(ALTER_TABLE);
        }

        public static DbCommand ShowTables()
        {
            return new MySqlCommand(SHOW_TABLES);
        }

        public static DbCommand ShowColumnas()
        {
            return new MySqlCommand(SHOW_COLUMNS);
        }

        public static DbCommand ShowTiposDatosColumnas()
        {
            return new MySqlCommand(SHOW_COLUMNS_DATA_TYPE);
        }

        public static DbCommand DeleteFrom()
        {
            return new MySqlCommand(DELETE_FROM);
        }

        public static DbCommand Update()
        {
            return new MySqlCommand(UPDATE);
        }

        public static DbCommand Insert()
        {
            return new MySqlCommand(INSERT);
        }

        public static DbCommand Select()
        {
            return new MySqlCommand(SELECT);
        }
    }
}
