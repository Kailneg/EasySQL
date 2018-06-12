using EasySQL.BBDD;
using EasySQL.Utils;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySQL.Modelos
{
    [Serializable]
    public class Conexion
    {
        /// <summary>
        /// ID de la conexión en la BBDD.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Tipos de conexiones posibles para una Conexión.
        /// </summary>
        public enum TipoConexion { MicrosoftSQL, MySQL };

        /// <summary>
        /// Nombre de la conexión.
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Dirección de la conexión.
        /// </summary>
        public string Direccion { get; set; }

        /// <summary>
        /// Nombre de la Base de datos.
        /// </summary>
        public string BaseDatos { get; set; }

        /// <summary>
        /// Puerto de la conexión.
        /// </summary>
        public int Puerto { get; set; }

        /// <summary>
        /// Tipo de la conexión actual.
        /// </summary>
        public TipoConexion TipoActual { get; set; }

        /// <summary>
        /// Usuario de la conexión.
        /// </summary>
        public string UsuarioConexion { get; set; }

        /// <summary>
        /// Contraseña de la conexión.
        /// </summary>
        public string ContraseniaConexion { get; set; }

        /// <summary>
        /// Usuario propietario (almacenado en BBDD) de la conexión.
        /// </summary>
        public Usuario Propietario { get; set; }

        /// <summary>
        /// Cadena de conexión bien formada para la conexión con BBDD.
        /// </summary>
        public string CadenaConexion {
            get
            {
                if (TipoActual == TipoConexion.MicrosoftSQL)
                    return ObtenerCadenaConexionSQL();
                else if (TipoActual == TipoConexion.MySQL)
                    return ObtenerCadenaConexionMySQL();
                else
                    return null;
            }
        }

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
        /// de tipo Microsoft SQL
        /// </summary>
        /// <returns>Cadena de conexión válida Microsoft SQL</returns>
        private string ObtenerCadenaConexionSQL()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

            builder.DataSource = Direccion;

            if (BaseDatos != null)
                builder.InitialCatalog = BaseDatos;
            else
                builder.InitialCatalog = "";

            if (UsuarioConexion.Equals(Usuario.NombreIntegratedSecurity))
            {
                builder.IntegratedSecurity = true;
            }
            else
            {
                builder.UserID = UsuarioConexion;
                builder.Password = ContraseniaConexion;
            }

            return builder.ToString();
        }

        /// <summary>
        /// Acepta un objeto tipo conexión y crea una cadena de conexión bien formada
        /// de tipo MySQL
        /// </summary>
        /// <returns>Cadena de conexión válida Microsoft SQL</returns>
        private string ObtenerCadenaConexionMySQL()
        {
            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();

            builder.Server = Direccion;
            if (BaseDatos != null)
                builder.Database = BaseDatos;
            else
                builder.Database = "";
            
            builder.UserID = UsuarioConexion;
            builder.Password = ContraseniaConexion;

            return builder.ToString();
        }
    }
}
