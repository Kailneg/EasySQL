﻿using EasySQL.Modelos;
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

        public static DbCommand ComandoShowTiposDatosColumnas(Conexion conexionActual)
        {
            if (conexionActual.TipoActual == Conexion.TipoConexion.MicrosoftSQL)
            {
                return OperacionMicrosoftSQL.ComandoShowTiposDatosColumnas();
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

        public static DbCommand ComandoUpdate(Conexion conexionActual)
        {
            if (conexionActual.TipoActual == Conexion.TipoConexion.MicrosoftSQL)
            {
                return OperacionMicrosoftSQL.ComandoUpdate();
            }
            else if (conexionActual.TipoActual == Conexion.TipoConexion.MySQL)
            {
                return null;
            }
            else return null;
        }

        public static DbCommand ComandoInsert(Conexion conexionActual)
        {
            if (conexionActual.TipoActual == Conexion.TipoConexion.MicrosoftSQL)
            {
                return OperacionMicrosoftSQL.ComandoInsert();
            }
            else if (conexionActual.TipoActual == Conexion.TipoConexion.MySQL)
            {
                return null;
            }
            else return null;
        }

        public static DbCommand ComandoSelect(Conexion conexionActual)
        {
            if (conexionActual.TipoActual == Conexion.TipoConexion.MicrosoftSQL)
            {
                return OperacionMicrosoftSQL.ComandoSelect();
            }
            else if (conexionActual.TipoActual == Conexion.TipoConexion.MySQL)
            {
                return null;
            }
            else return null;
        }
    }
}
