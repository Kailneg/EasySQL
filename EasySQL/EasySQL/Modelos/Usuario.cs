using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySQL.Modelos
{
    public class Usuario
    {
        public string Nombre { get; set; }
        public string Contrasenia { get; set; }

        public static bool ComprobarContrasenia(String contrasenia)
        {
            return contrasenia.Length >= 4;
        }

        public Usuario (string nombre, string contrasenia)
        {
            Nombre = nombre;
            Contrasenia = contrasenia;
        }
    }
}
