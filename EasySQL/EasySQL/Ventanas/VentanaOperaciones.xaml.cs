using EasySQL.Modelos;
using EasySQL.Ventanas.Operaciones;
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
        public VentanaOperaciones() : 
            this(
                    new Conexion()
                    {   Direccion = "localhost/SQLALE",
                        TipoActual = Conexion.TipoConexion.MicrosoftSQL,
                        UsuarioConexion = "Integrated Security"
                    }
                )
        { }
        public VentanaOperaciones(Conexion datosConexion)
        {
            InitializeComponent();
            this.conexionActual = datosConexion;
            MostrarDatosConexion();
        }

        private void btnAtras_Click(object sender, RoutedEventArgs e)
        {
            Atras();
        }

        private void btnCargar_Click(object sender, RoutedEventArgs e)
        {
            Cargar();
            
        }

        private void btnCreateDb_Click(object sender, RoutedEventArgs e)
        {
            CreateDB();
        }

        private void btnDropDb_Click(object sender, RoutedEventArgs e)
        {
            DropDB();
        }

        private void btnShowDbs_Click(object sender, RoutedEventArgs e)
        {
            ShowDBs();
        }

        private void btnCreateTable_Click(object sender, RoutedEventArgs e)
        {
            CreateTable();
        }

        private void btnDropTable_Click(object sender, RoutedEventArgs e)
        {
            DropTable();
        }

        private void btnShowTables_Click(object sender, RoutedEventArgs e)
        {
            ShowTables();
        }

        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            Select();
        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            Insert();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Update();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Delete();
        }
    }
}
