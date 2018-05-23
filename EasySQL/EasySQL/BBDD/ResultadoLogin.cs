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
        public enum TipoResultado { ACEPTADO, DENEGADO }

        public TipoResultado ResultadoActual { get; private set; }
        public Usuario UsuarioActual { get; private set; }

        private readonly static string RESPUESTA_ACEPTADO =
            "Usuario y contraseña correcto, accediendo...";
        private readonly static string RESPUESTA_DENEGADO =
            "No existe el usuario o la contraseña no es correcta.";

        public ResultadoLogin(TipoResultado resultado, Usuario usuarioLogin)
        {
            ResultadoActual = resultado;
            UsuarioActual = usuarioLogin;
        }

        public static TipoResultado MostrarMensaje(TipoResultado r)
        {
            switch (r)
            {
                case TipoResultado.ACEPTADO:
                    MessageBox.Show(RESPUESTA_ACEPTADO);
                    break;
                case TipoResultado.DENEGADO:
                    MessageBox.Show(RESPUESTA_DENEGADO);
                    break;
                default:
                    break;
            }
            return r;
        }
    }
}
