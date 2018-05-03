using EasySQL.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EasySQL.Ventanas
{
    /// <summary>
    /// Lógica de interacción para VentanaOperaciones.xaml
    /// </summary>
    public partial class VentanaOperaciones : Window
    {
        private Conexion datosConexion;
        public VentanaOperaciones(Conexion datosConexion)
        {
            InitializeComponent();
            this.datosConexion = datosConexion;
        }

        private void btnAtras_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            VentanaConexion vc = new VentanaConexion(null);
        }

        private void btnCargar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCreateDb_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDropDb_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCreateTable_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDropTable_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnGuardar212_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnShowDbs_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnShowTables_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
