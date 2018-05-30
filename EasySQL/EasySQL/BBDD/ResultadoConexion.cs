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
        public enum TipoResultado { ACEPTADO, DUPLICADA, ERROR }

        public TipoResultado ResultadoActual { get; private set; }
        public Conexion ConexionGuardar { get; private set; }

        private readonly static string RESPUESTA_ACEPTADO =
            "Se ha almacenado correctamente la conexion";
        private readonly static string RESPUESTA_DUPLICADA =
            "La conexión ya se encuentra almacenada en la base de datos.";
        private readonly static string RESPUESTA_ERROR =
            "Se ha producido un error al intentar conectar con la base de datos.";

        public ResultadoConexion(TipoResultado resultado, Conexion conexiones)
        {
            ResultadoActual = resultado;
            ConexionGuardar = conexiones;
        }

        public void MostrarMensaje()
        {
            switch (ResultadoActual)
            {
                case TipoResultado.ACEPTADO:
                    MessageBox.Show(RESPUESTA_ACEPTADO);
                    break;
                case TipoResultado.DUPLICADA:
                    MessageBox.Show(RESPUESTA_DUPLICADA);
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
