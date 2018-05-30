using EasySQL.Modelos;
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
        public enum TipoResultado { ACEPTADO ,ERROR_CONEXION, DUPLICADO }

        public TipoResultado ResultadoActual { get; private set; }
        public Usuario UsuarioActual { get; private set; }

        private readonly static string RESPUESTA_ACEPTADO =
            "El usuario se ha registrado correctamente en la base de datos.";
        private readonly static string RESPUESTA_ERROR =
            "Ha surgido un error al registrar el usuario.";
        private readonly static string RESPUESTA_DUPLICADO =
            "El nombre de usuario elegido ya se encuentra registrado.";

        public ResultadoRegistro(TipoResultado resultado, Usuario usuarioLogin)
        {
            this.ResultadoActual = resultado;
            this.UsuarioActual = usuarioLogin;
        }

        public void MostrarMensaje()
        {
            switch (ResultadoActual)
            {
                case TipoResultado.ACEPTADO:
                    MessageBox.Show(RESPUESTA_ACEPTADO);
                    break;
                case TipoResultado.ERROR_CONEXION:
                    MessageBox.Show(RESPUESTA_ERROR);
                    break;
                case TipoResultado.DUPLICADO:
                    MessageBox.Show(RESPUESTA_DUPLICADO);
                    break;
                default:
                    break;
            }
        }
    }
}
