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
        public enum TipoResultado { ACEPTADO, DENEGADO, ERROR }

        public TipoResultado ResultadoActual { get; private set; }
        public Usuario UsuarioActual { get; private set; }

        private const string RESPUESTA_ACEPTADO =
            "Usuario y contraseña correcto, accediendo...";
        private const string RESPUESTA_DENEGADO =
            "No existe el usuario o la contraseña no es correcta.";
        private const string RESPUESTA_ERROR =
            "Se ha producido un error al intentar conectar con la base de datos.";

        public ResultadoLogin(TipoResultado resultado, Usuario usuarioLogin)
        {
            ResultadoActual = resultado;
            UsuarioActual = usuarioLogin;
        }

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
