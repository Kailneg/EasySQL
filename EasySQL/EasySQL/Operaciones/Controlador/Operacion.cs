using EasySQL.Modelos;
using EasySQL.Operaciones.Ayudante;
using EasySQL.Operaciones.Controlador;
using EasySQL.Operaciones.Controlador.MicrosoftSQL;
using EasySQL.Operaciones.Controlador.MySQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySQL.Operaciones.Controlador
{
    public class Operacion
    {
        public static List<string> ObtenerBasesDatos(Conexion conexionActual)
        {
            if (conexionActual.TipoActual == Conexion.TipoConexion.MicrosoftSQL)
            {
                return OperacionMicrosoftSQL.ObtenerBasesDatos(conexionActual);
            }
            else if (conexionActual.TipoActual == Conexion.TipoConexion.MySQL)
            {
                return OperacionMySQL.ObtenerBasesDatos(conexionActual);
            }
            else return null;
        }
    }
}
