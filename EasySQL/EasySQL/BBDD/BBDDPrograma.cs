using EasySQL.Modelos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EasySQL.BBDD.ResultadoRegistro;

namespace EasySQL.BBDD
{
    public class BBDDPrograma
    {
        public static ResultadoRegistro RegistrarUsuario(string usuario, string contrasenia)
        {
            return BBDDProgramaImpl.ObtenerInstancia().RegistrarUsuario(usuario, contrasenia);
        }

        public static ResultadoLogin LoginUsuario(string usuario, string contrasenia)
        {
            return BBDDProgramaImpl.ObtenerInstancia().LoginUsuario(usuario, contrasenia);
        }

        public static ObservableCollection<Conexion> ObtenerConexionesUsuario(Usuario usuario)
        {
            return BBDDProgramaImpl.ObtenerInstancia().ObtenerConexionesUsuario(usuario);
        }

        public static int ObtenerIDUsuario(Usuario usuario)
        {
            return BBDDProgramaImpl.ObtenerInstancia().ObtenerIDUsuario(usuario);
        }

        public static bool EliminarConexion(Conexion eliminar)
        {
            return BBDDProgramaImpl.ObtenerInstancia().EliminarConexion(eliminar);
        }
    }
}
