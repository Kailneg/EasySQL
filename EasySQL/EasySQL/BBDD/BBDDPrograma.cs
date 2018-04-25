using EasySQL.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySQL.BBDD
{
    public abstract class BBDDPrograma
    {
        private static BBDDPrograma instancia;
        public abstract List<Conexion> ObtenerConexiones(Usuario usuario);

        public static BBDDPrograma ObtenerInstancia()
        {
            if (instancia == null)
            {
                instancia = new BBDDProgramaImpl();
            }
            return instancia;
        }
    }
}
