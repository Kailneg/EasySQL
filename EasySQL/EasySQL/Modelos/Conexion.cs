using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySQL.Modelos
{
    public class Conexion
    {
        public int ID { get; set; }
        public enum TipoConexion { MicrosoftSQL, MySQL };
        public String Nombre { get; set; }
        public String Direccion { get; set; }
        public int Puerto { get; set; }
        public TipoConexion TipoActual { get; set; }
        public String UsuarioConexion { get; set; }
        public String ContraseniaConexion { get; set; }
        public Usuario Propietario { get; set; }
    }
}
