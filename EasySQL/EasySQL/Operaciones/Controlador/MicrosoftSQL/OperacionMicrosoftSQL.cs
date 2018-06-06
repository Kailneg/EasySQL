﻿using EasySQL.Modelos;
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
        public static readonly string[] TIPOS_DATOS = 
            { "INT", "FLOAT", "NVARCHAR(50)", "NVARCHAR(MAX)", "DATETIME"  };
        private const string SHOW_DATABASES = "SELECT name FROM master.sys.databases";
        private const string CREATE_DATABASE = "CREATE DATABASE ";
        private const string DROP_DATABASE = "DROP DATABASE ";
        private const string SHOW_TABLES =
            "SELECT TABLE_NAME FROM @param.INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'";
        private const string SHOW_COLUMNS =
            "SELECT COLUMN_NAME FROM @param.INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '@2param'";
        private const string CREATE_TABLE = "CREATE TABLE ";
        private const string DROP_TABLE = "DROP TABLE ";
        private const string ALTER_TABLE = "ALTER TABLE ";
        private const string DELETE_FROM = "DELETE FROM @param WHERE ";

        public static DbCommand ComandoShowDatabases()
        {
            return new SqlCommand(SHOW_DATABASES);
        }

        public static DbCommand ComandoCreateDatabase()
        {
            return new SqlCommand(CREATE_DATABASE);
        }

        public static DbCommand ComandoDropDatabase()
        {
            return new SqlCommand(DROP_DATABASE);
        }

        public static DbCommand ComandoCreateTable()
        {
            return new SqlCommand(CREATE_TABLE);
        }

        public static DbCommand ComandoDropTable()
        {
            return new SqlCommand(DROP_TABLE);
        }

        public static DbCommand ComandoAlterTable()
        {
            return new SqlCommand(ALTER_TABLE);
        }

        public static DbCommand ComandoShowTables()
        {
            return new SqlCommand(SHOW_TABLES);
        }

        public static DbCommand ComandoShowColumnas()
        {
            return new SqlCommand(SHOW_COLUMNS);
        }

        public static DbCommand ComandoDeleteFrom()
        {
            return new SqlCommand(DELETE_FROM);
        }
    }
}
