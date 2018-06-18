using EasySQL.Modelos;
using EasySQL.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EasySQL.BBDD
{
    public class ResultadoRegistro
    {
        /// <summary>
        /// Tipos de resultados posibles.
        /// </summary>
        public enum TipoResultado { ACEPTADO ,ERROR_CONEXION, DUPLICADO }

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
            "El usuario se ha registrado correctamente en la base de datos.";

        /// <summary>
        /// Respuesta en caso de que la operación haya sido incorrecta.
        /// </summary>
        private const string RESPUESTA_DUPLICADO =
            "El nombre de usuario elegido ya se encuentra registrado.";

        /// <summary>
        /// Respuesta en caso de que la operación haya sido fallida.
        /// </summary>
        private const string RESPUESTA_ERROR =
            "Ha surgido un error al registrar el usuario.";

        /// <summary>
        /// Construye un objeto tipo ResultadoRegistro y asigna sus datos
        /// </summary>
        /// <param name="resultado">El resultado de la operación.</param>
        /// <param name="usuarioLogin">El usuario resultante.</param>
        public ResultadoRegistro(TipoResultado resultado, Usuario usuarioLogin)
        {
            this.ResultadoActual = resultado;
            this.UsuarioActual = usuarioLogin;
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
                    Msj.Info(RESPUESTA_ACEPTADO);
                    break;
                case TipoResultado.ERROR_CONEXION:
                    Msj.Aviso(RESPUESTA_ERROR);
                    break;
                case TipoResultado.DUPLICADO:
                    Msj.Error(RESPUESTA_DUPLICADO);
                    break;
                default:
                    break;
            }
        }
    }
}
