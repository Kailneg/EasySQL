using EasySQL.Modelos;
using System;
using System.Collections.Generic;
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

    }
}
