using EasySQL.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EasySQL.BBDD
{
    public class ResultadoLogin
    {
        /// <summary>
        /// Tipos de resultados posibles.
        /// </summary>
        public enum TipoResultado { ACEPTADO, DENEGADO, ERROR }

        /// <summary>
        /// Resultado actual de la operación.
        /// </summary>
        public TipoResultado ResultadoActual { get; private set; }

        /// <summary>
        /// Usuario resultante de la operación.
        /// </summary>
        public Usuario UsuarioActual { get; private set; }

        /// <summary>
        /// Respuesta en caso de que la operación haya sido correcta.
        /// </summary>
        private const string RESPUESTA_ACEPTADO =
            "Usuario y contraseña correcto, accediendo...";

        /// <summary>
        /// Respuesta en caso de que la operación haya sido incorrecta.
        /// </summary>
        private const string RESPUESTA_DENEGADO =
            "No existe el usuario o la contraseña no es correcta.";

        /// <summary>
        /// Respuesta en caso de que la operación haya sido fallida.
        /// </summary>
        private const string RESPUESTA_ERROR =
            "Se ha producido un error al intentar conectar con la base de datos.";

        /// <summary>
        /// Construye un objeto tipo ResultadoLogin y asigna sus datos
        /// </summary>
        /// <param name="resultado">El resultado de la operación.</param>
        /// <param name="usuarioLogin">El usuario resultante.</param>
        public ResultadoLogin(TipoResultado resultado, Usuario usuarioLogin)
        {
            ResultadoActual = resultado;
            UsuarioActual = usuarioLogin;
        }

        /// <summary>
        /// Muestra un mensaje al usuario correspondiente al tipo de resultado
        /// que se haya producido.
        /// </summary>/// <summary>
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
                case TipoResultado.DENEGADO:
                    MessageBox.Show(RESPUESTA_DENEGADO);
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
