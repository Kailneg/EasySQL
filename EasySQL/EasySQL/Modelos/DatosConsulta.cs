using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySQL.Modelos
{
    [Serializable]
    public class DatosConsulta
    {
        /// <summary>
        /// Conexión que se ha utilizado al hacer la consulta.
        /// </summary>
        public Conexion Conexion { get; private set; }

        /// <summary>
        /// Datos de la consulta Select
        /// </summary>
        public DataTable Datos { get; private set; }

        /// <summary>
        /// Comando SQL realizado en la consulta.
        /// </summary>
        public string ComandoSQL { get; private set; }

        /// <summary>
        /// Fecha y hora del momento del empaquetamiento de datos.
        /// </summary>
        public DateTime FechaCreacion { get; private set; }

        /// <summary>
        /// Crea un objeto serializable con el que empaquetar datos de una
        /// consulta Select para almacenar en un fichero.
        /// </summary>
        /// <param name="conexionActual">La conexión que se está usando en el momento.</param>
        /// <param name="datosGuardar">Datos de la consulta Select</param>
        /// <param name="comandoSQL">Comando SQL realizado en la consulta.</param>
        public DatosConsulta (Conexion conexionActual, DataTable datosGuardar, string comandoSQL)
        {
            Conexion = conexionActual;
            Datos = datosGuardar;
            ComandoSQL = comandoSQL;
            FechaCreacion = DateTime.Now;
        }
    }
}
