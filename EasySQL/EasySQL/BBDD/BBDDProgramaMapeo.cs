﻿using EasySQL.Modelos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EasySQL.Modelos.Conexion;

namespace EasySQL.BBDD
{
    public class BBDDProgramaMapeo
    {
        /// <summary>
        /// Genera una lista de conexiones bien formadas dado un dataReader con conexiones
        /// y un usuario.
        /// </summary>
        /// <param name="lector">DataReader con los datos de las conexiones.</param>
        /// <param name="usuario">Usuario propietario de esas conexiones.</param>
        /// <returns>Una lista de las conexiones pertenecientes al usuario.</returns>
        public static List<Conexion> ExtraerConexiones(SqlDataReader lector, Usuario usuario)
        {
            // SELECT id_conexion, id_tipo_conexion, nombre, direccion, puerto, usuario, contrasenia
            List<Conexion> conexiones = new List<Conexion>();

            while (lector.Read())
            {
                int id_conexion = (int)lector[0];
                TipoConexion tipo = (TipoConexion)(int)lector[1];
                string nombreCon = (string)lector[2];
                string direccion = (string)lector[3];
                int puerto = (int)lector[4];
                string nombreUsuario = (string)lector[5];
                string contraseniaUsuario = (string)lector[6];

                Conexion c = new Conexion()
                {
                    ID = id_conexion,
                    TipoActual = tipo,
                    Nombre = nombreCon,
                    Direccion = direccion,
                    Puerto = puerto,
                    UsuarioConexion = nombreUsuario,
                    ContraseniaConexion = contraseniaUsuario,
                    Propietario = usuario
                };

                conexiones.Add(c);
            }
            return conexiones;
        }
    }
}
