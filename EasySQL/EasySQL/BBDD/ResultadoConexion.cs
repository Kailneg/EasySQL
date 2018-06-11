using EasySQL.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EasySQL.BBDD
{
    public class ResultadoConexion
    {
        /// <summary>
        /// Tipos de resultados posibles.
        /// </summary>
        public enum TipoResultado { ACEPTADO, DUPLICADO, ERROR }

        /// <summary>
        /// Resultado actual de la operación.
        /// </summary>
        public TipoResultado ResultadoActual { get; private set; }

        /// <summary>
        /// Conexión resultante de la operación.
        /// </summary>
        public Conexion ConexionGuardar { get; private set; }

        /// <summary>
        /// Respuesta en caso de que la operación haya sido correcta.
        /// </summary>
        private const string RESPUESTA_ACEPTADO =
            "Se ha almacenado correctamente la conexion";

        /// <summary>
        /// Respuesta en caso de que la operación haya sido incorrecta.
        /// </summary>
        private const string RESPUESTA_DUPLICADO =
            "La conexión ya se encuentra almacenada en la base de datos.";

        /// <summary>
        /// Respuesta en caso de que la operación haya sido fallida.
        /// </summary>
        private const string RESPUESTA_ERROR =
            "Se ha producido un error al intentar conectar con la base de datos.";

        /// <summary>
        /// Construye un objeto tipo ResultadoConexion y asigna sus datos
        /// </summary>
        /// <param name="resultado">El resultado de la operación.</param>
        /// <param name="conexiones">La conexión resultante.</param>
        public ResultadoConexion(TipoResultado resultado, Conexion conexiones)
        {
            ResultadoActual = resultado;
            ConexionGuardar = conexiones;
        }

        /// <summary>
        /// Muestra un mensaje al usuario correspondiente al tipo de resultado
        /// que se haya producido.
        /// </summary>
        public void MostrarMensaje()
        {
            switch (ResultadoActual)
            {
                case TipoResultado.ACEPTADO:
                    MessageBox.Show(RESPUESTA_ACEPTADO);
                    break;
                case TipoResultado.DUPLICADO:
                    MessageBox.Show(RESPUESTA_DUPLICADO);
                    break;
                case TipoResultado.ERROR:
                    MessageBox.Show(RESPUESTA_ERROR);
                    break;
                default:
                    break;
            }
        }
    }
}
