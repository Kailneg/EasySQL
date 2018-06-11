using EasySQL.Modelos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EasySQL.BBDD.ResultadoRegistro;

namespace EasySQL.BBDD
{
    public class BBDDPrograma
    {
        /// <summary>
        /// Registra un usuario en la BBDD del programa.
        /// </summary>
        /// <param name="usuario">Nombre del usuario a registrar</param>
        /// <param name="contrasenia">Contraseña del usuario.</param>
        /// <returns>Un objeto tipo ResultadoRegistro con el resultado de la operación</returns>
        public static ResultadoRegistro RegistrarUsuario(string usuario, string contrasenia)
        {
            return BBDDProgramaImpl.ObtenerInstancia().RegistrarUsuario(usuario, contrasenia);
        }

        /// <summary>
        /// Logea un usuario contra la BBDD del programa.
        /// </summary>
        /// <param name="usuario">Nombre del usuario a registrar</param>
        /// <param name="contrasenia">Contraseña del usuario.</param>
        /// <returns>Un objeto tipo ResultadoLogin con el resultado de la operación</returns>
        public static ResultadoLogin LoginUsuario(string usuario, string contrasenia)
        {
            return BBDDProgramaImpl.ObtenerInstancia().LoginUsuario(usuario, contrasenia);
        }

        /// <summary>
        /// Registra una conexión en la BBDD del programa.
        /// </summary>
        /// <param name="guardar">Conexión a registrar.</param>
        /// <returns>Un objeto tipo ResultadoConexion con el resultado de la operación</returns>
        public static ResultadoConexion RegistrarConexion(Conexion guardar)
        {
            return BBDDProgramaImpl.ObtenerInstancia().RegistrarConexion(guardar);
        }

        /// <summary>
        /// Obtiene las conexiones de un usuario desde la BBDD del programa.
        /// </summary>
        /// <param name="usuario">El usuario del que obtener las conexiones.</param>
        /// <returns>Una lista observable con las conexiones del usuario.</returns>
        public static ObservableCollection<Conexion> ObtenerConexionesUsuario(Usuario usuario)
        {
            return BBDDProgramaImpl.ObtenerInstancia().ObtenerConexionesUsuario(usuario);
        }

        /// <summary>
        /// Obtiene el ID de la BBDD de un usuario dado.
        /// </summary>
        /// <param name="usuario">El usuario del que obtener la ID.</param>
        /// <returns>Número entero con la ID del usuario.</returns>
        public static int ObtenerIDUsuario(Usuario usuario)
        {
            return BBDDProgramaImpl.ObtenerInstancia().ObtenerIDUsuario(usuario);
        }

        /// <summary>
        /// Obtiene el ID de la BBDD de una conexión dada.
        /// </summary>
        /// <param name="conexion">La conexión del que obtener la ID.</param>
        /// <returns>Número entero con la ID de la conexión.</returns>
        public static int ObtenerIDConexion(Conexion conexion)
        {
            return BBDDProgramaImpl.ObtenerInstancia().ObtenerIDConexion(conexion);
        }

        /// <summary>
        /// Obtiene el puerto por defecto de un tipo de conexión desde la BBDD
        /// </summary>
        /// <param name="tipo">Tipo de conexión del que se quiere saber su puerto por defecto.</param>
        /// <returns>Un entero con el puerto por defecto.</returns>
        public static int ObtenerPuertoDefecto(Conexion.TipoConexion tipo)
        {
            return BBDDProgramaImpl.ObtenerInstancia().ObtenerPuertoDefecto(tipo);
        }

        /// <summary>
        /// Elimina una conexión de la base de datos.
        /// </summary>
        /// <param name="eliminar">La conexión a eliminar.</param>
        /// <returns>True si se ha eliminado la conexión.</returns>
        public static bool EliminarConexion(Conexion eliminar)
        {
            return BBDDProgramaImpl.ObtenerInstancia().EliminarConexion(eliminar);
        }
    }
}
