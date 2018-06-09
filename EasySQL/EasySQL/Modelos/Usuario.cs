using EasySQL.BBDD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySQL.Modelos
{
    [Serializable]
    public class Usuario
    {
        /// <summary>
        /// Nombre a usar para el usuario cuando este marque la opción
        /// Integrated Security en ventana Conexión
        /// </summary>
        public const string NombreIntegratedSecurity = "Integrated Security";

        /// <summary>
        /// Número que se corresponde con la columna ID de la base de datos.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Nombre del usuario
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Contraseña del usuario
        /// </summary>
        public string Contrasenia { get; set; }

        /// <summary>
        /// Construye un objecto tipo Usuario y le asigna sus valores.
        /// </summary>
        /// <param name="nombre">Nombre del usuario.</param>
        /// <param name="contrasenia">Contraseña del usuario.</param>
        public Usuario (string nombre, string contrasenia)
        {
            Nombre = nombre;
            Contrasenia = contrasenia;
            ID = BBDDPrograma.ObtenerIDUsuario(this);
        }
    }
}
