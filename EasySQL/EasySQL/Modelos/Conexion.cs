using EasySQL.BBDD;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySQL.Modelos
{
    public class Conexion
    {
        public int ID { get; set; }
        public enum TipoConexion { MicrosoftSQL, MySQL };
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public int Puerto { get; set; }
        public TipoConexion TipoActual { get; set; }
        public string UsuarioConexion { get; set; }
        public string ContraseniaConexion { get; set; }
        public Usuario Propietario { get; set; }

        /// <summary>
        /// Constructor vacío, usado a la hora de mapear desde BBDD
        /// </summary>
        public Conexion() {}

        /// <summary>
        /// Constructor con los parámetros necesarios para guardar una conexión en BBDD
        /// </summary>
        public Conexion(string nombre, string direccion, int puerto, string usuario, TipoConexion tipo, Usuario propietario) 
            : this(nombre, direccion, puerto, usuario, "", tipo, propietario) {}

        /// <summary>
        /// Constructor con los parámetros necesarios para guardar una conexión en BBDD con contraseña
        /// </summary>
        public Conexion(string nombre, string direccion, int puerto, string usuario, string contrasenia, 
            TipoConexion tipo, Usuario propietario)
        {
            // Campos obligatorios: nombre conexión, dirección, usuario, tipo conexión, puerto.
            // De la bbdd hay que traer el ID y el puerto.
            Nombre = nombre;
            Direccion = direccion;
            UsuarioConexion = usuario;
            ContraseniaConexion = contrasenia;
            TipoActual = tipo;
            Propietario = propietario;
            Puerto = puerto;
        }

        /// <summary>
        /// Acepta un objeto tipo conexión y crea una cadena de conexión bien formada
        /// </summary>
        /// <returns>Cadena de conexión válida</returns>
        public static string ObtenerCadenaConexion(Conexion c)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

            builder.DataSource = c.Direccion;
            //builder["Initial Catalog"] = "AdventureWorks;NewValue=Bad";
            if (c.UsuarioConexion.Equals(Usuario.NombreIntegratedSecurity))
            {
                builder.IntegratedSecurity = true;
            }
            else
            {
                builder.UserID = c.UsuarioConexion;
                builder.Password = c.ContraseniaConexion;
            }

            return builder.ToString();
        }
    }
}
