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
        public Conexion Conexion { get; private set; }
        public DataTable Datos { get; private set; }
        public string ComandoSQL { get; private set; }
        public DateTime FechaCreacion { get; private set; }

        public DatosConsulta (Conexion conexionActual, DataTable datosGuardar, string comandoSQL)
        {
            Conexion = conexionActual;
            Datos = datosGuardar;
            ComandoSQL = comandoSQL;
            FechaCreacion = DateTime.Now;
        }
    }
}
