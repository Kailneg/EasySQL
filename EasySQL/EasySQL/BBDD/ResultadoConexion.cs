﻿using EasySQL.Modelos;
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
        public enum TipoResultado { ACEPTADO, DUPLICADO, ERROR }

        public TipoResultado ResultadoActual { get; private set; }
        public Conexion ConexionGuardar { get; private set; }

        private const string RESPUESTA_ACEPTADO =
            "Se ha almacenado correctamente la conexion";
        private const string RESPUESTA_DUPLICADO =
            "La conexión ya se encuentra almacenada en la base de datos.";
        private const string RESPUESTA_ERROR =
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
