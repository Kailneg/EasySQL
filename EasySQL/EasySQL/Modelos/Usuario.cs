using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySQL.Modelos
{
    public class Usuario
    {
        public String Nombre { get; set; }
        public String Contrasenia { get; set; }
        public DateTime FechaCreacion { get; set; }

        public static bool ComprobarContrasenia(String contrasenia)
        {
            return contrasenia.Length >= 4;
        }
    }
}
