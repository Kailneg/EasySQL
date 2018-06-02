﻿using EasySQL.Modelos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySQL.Operaciones.Ayudante
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

        public static object ExecuteScalar(Conexion actual, DbCommand comando)
        {
            if (actual.TipoActual == Conexion.TipoConexion.MicrosoftSQL)
            {
                return AyudanteSQL.ExecuteScalar(actual.CadenaConexion, (SqlCommand) comando);
            }
            else if (actual.TipoActual == Conexion.TipoConexion.MySQL)
            {
                return AyudanteMySQL.ExecuteScalar(actual.CadenaConexion, (SqlCommand)comando);
            }
            else return null;
        }

        public static int ExecuteNonQuery(Conexion actual, DbCommand comando)
        {
            if (actual.TipoActual == Conexion.TipoConexion.MicrosoftSQL)
            {
                return AyudanteSQL.ExecuteNonQuery(actual.CadenaConexion, (SqlCommand)comando);
            }
            else if (actual.TipoActual == Conexion.TipoConexion.MySQL)
            {
                return AyudanteMySQL.ExecuteNonQuery(actual.CadenaConexion, (SqlCommand)comando);
            }
            else return -1;
        }

        public static IDataReader ExecuteReader(Conexion actual, DbCommand comando)
        {
            if (actual.TipoActual == Conexion.TipoConexion.MicrosoftSQL)
            {
                return AyudanteSQL.ExecuteReader(actual.CadenaConexion, (SqlCommand)comando);
            }
            else if (actual.TipoActual == Conexion.TipoConexion.MySQL)
            {
                return AyudanteMySQL.ExecuteReader(actual.CadenaConexion, (SqlCommand)comando);
            }
            else return null;
        }
        
        public static List<string> MapearReaderALista(IDataReader lector)
        {
            List<string> resultado = new List<string>();

            while (lector.Read())
            {
                resultado.Add(lector[0].ToString());
            }
            return resultado;
        }
    }
}
