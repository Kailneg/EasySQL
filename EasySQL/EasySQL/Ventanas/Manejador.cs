using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EasySQL.Ventanas
{
    public class Manejador
    {
        /// <summary>
        /// Cambia la ventana actual por otra.
        /// </summary>
        /// <param name="actual">La ventana actual.</param>
        /// <param name="objetivo">La ventana que se desea abrir.</param>
        public static void CambiarVentana(Window actual, Window objetivo)
        {
            actual.Close();
            objetivo.Show();
        }
    }
}
