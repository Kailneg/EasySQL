using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EasySQL.Utils
{
    public static class Msj
    {
        public static void Info(string descripcion)
        {
            MessageBox.Show(
                descripcion, "Información", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public static void Aviso(string descripcion)
        {
            MessageBox.Show(
                descripcion, "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        public static void Error(string descripcion)
        {
            MessageBox.Show(
                descripcion, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
