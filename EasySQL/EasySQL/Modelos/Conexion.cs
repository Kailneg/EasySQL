using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySQL.Modelos
{
    public class Conexion
    {
        public String Nombre { get; set; }
        public String Direccion { get; set; }
        public int Puerto { get; set; }
        public int TipoConexion { get; set; }
        public String UsuarioConexion { get; set; }
        public String ContraseniaConexion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public Usuario Propietario { get; set; }
    }
}
