using EasySQL.Modelos;
using EasySQL.Utils;
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
        /// <summary>
        /// Constructor para pruebas
        /// </summary>
        public VentanaOperaciones() :
            this(
                    new Conexion()
                    { Direccion = "localhost\\SQLALE",
                        TipoActual = Conexion.TipoConexion.MicrosoftSQL,
                        UsuarioConexion = Usuario.NombreIntegratedSecurity
                    }
                )
        { }

        /// <summary>
        /// Crea una nueva ventana de conexión e inicia sus componentes.
        /// Carga una conexión. 
        /// Constructor llamado desde ventana Conexion.
        /// </summary>
        /// <param name="conexion">La conexión activa del momento.</param>
        public VentanaOperaciones(Conexion datosConexion)
        {
            InitializeComponent();
            this.conexionActual = datosConexion;
            MostrarDatosConexion();
        }

        /// <summary>
        /// Captura el evento selección cambiada del ComboBox
        /// para seleccionar base de datos y ejecuta la lógica.
        /// </summary>
        private void cmbBaseDatos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SeleccionBBDDCambiada();
        }

        /// <summary>
        /// Captura el evento al pulsar sobre el ComboBox que selecciona
        /// la base de datos y ejecuta la lógica.
        /// </summary>
        private void cmbBaseDatos_DropDownOpened(object sender, EventArgs e)
        {
            MostrarBasesDatos();
        }

        /// <summary>
        /// Captura el evento click del botón Atras y ejecuta la lógica.
        /// </summary>
        private void btnAtras_Click(object sender, RoutedEventArgs e)
        {
            Atras();
        }

        /// <summary>
        /// Captura el evento click del botón Cargar y ejecuta la lógica.
        /// </summary>
        private void btnCargar_Click(object sender, RoutedEventArgs e)
        {
            Cargar();
        }

        /// <summary>
        /// Captura el evento click del botón Ayuda y ejecuta la lógica.
        /// </summary>
        private void btnAyuda_Click(object sender, RoutedEventArgs e)
        {
            Ayuda();
        }

        /// <summary>
        /// Captura el evento click del botón CREATE DATABASE y ejecuta la lógica.
        /// </summary>
        private void btnCreateDb_Click(object sender, RoutedEventArgs e)
        {
            CreateDB();
        }

        /// <summary>
        /// Captura el evento click del botón DROP DATABASE y ejecuta la lógica.
        /// </summary>
        private void btnDropDb_Click(object sender, RoutedEventArgs e)
        {
            DropDB();
        }


        /// <summary>
        /// Captura el evento click del botón CREATE TABLE y ejecuta la lógica.
        /// </summary>
        private void btnCreateTable_Click(object sender, RoutedEventArgs e)
        {
            CreateTable();
        }

        /// <summary>
        /// Captura el evento click del botón DROP TABLE y ejecuta la lógica.
        /// </summary>
        private void btnDropTable_Click(object sender, RoutedEventArgs e)
        {
            DropTable();
        }

        /// <summary>
        /// Captura el evento click del botón ALTER TABLE y ejecuta la lógica.
        /// </summary>
        private void btnAlterTable_Click(object sender, RoutedEventArgs e)
        {
            AlterTable();
        }

        /// <summary>
        /// Captura el evento click del botón SHOW TABLES y ejecuta la lógica.
        /// </summary>
        private void btnShowTables_Click(object sender, RoutedEventArgs e)
        {
            ShowTables();
        }

        /// <summary>
        /// Captura el evento click del botón SELECT y ejecuta la lógica.
        /// </summary>
        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            Select();
        }

        /// <summary>
        /// Captura el evento click del botón INSERT y ejecuta la lógica.
        /// </summary>
        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            Insert();
        }

        /// <summary>
        /// Captura el evento click del botón UPDATE y ejecuta la lógica.
        /// </summary>
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Update();
        }

        /// <summary>
        /// Captura el evento click del botón DELETE y ejecuta la lógica.
        /// </summary>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Delete();
        }

    }
}
