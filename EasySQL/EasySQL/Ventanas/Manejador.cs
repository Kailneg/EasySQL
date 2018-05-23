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
        public static void CambiarVentana(Window actual, Window objetivo)
        {
            actual.Close();
            objetivo.Show();
        }
    }
}
