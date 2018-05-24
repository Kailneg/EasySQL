using EasySQL.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EasySQL.BBDD
{
    public class ResultadoConexiones
    {
        public enum TipoResultado { ACEPTADO, NO_EXISTE, ERROR }

        public TipoResultado ResultadoActual { get; private set; }
        public Usuario UsuarioActual { get; private set; }
        public List<Conexion> ConexionesUsuario { get; private set; }

        private readonly static string RESPUESTA_ACEPTADO =
            "Conexiones correctamente recuperadas.";
        private readonly static string RESPUESTA_NO_EXISTE =
            "No existen conexiones almacenadas en la base de datos.";
        private readonly static string RESPUESTA_ERROR =
            "Se ha producido un error al intentar conectar con la base de datos.";

        public ResultadoConexiones(TipoResultado resultado, List<Conexion> conexiones)
        {
            ResultadoActual = resultado;
            ConexionesUsuario = conexiones;
        }

        public TipoResultado MostrarMensaje(TipoResultado r)
        {
            switch (r)
            {
                case TipoResultado.ACEPTADO:
                    MessageBox.Show(ConexionesUsuario?.Count + RESPUESTA_ACEPTADO);
                    break;
                case TipoResultado.NO_EXISTE:
                    MessageBox.Show(RESPUESTA_NO_EXISTE);
                    break;
                case TipoResultado.ERROR:
                    MessageBox.Show(RESPUESTA_ERROR);
                    break;
                default:
                    break;
            }
            return r;
        }
    }
}
