using EasySQL.Modelos;
using EasySQL.Operaciones.Ayudante;
using EasySQL.Operaciones.Controlador;
using EasySQL.Utils;
using System;
using System.Collections.Generic;
using System.Data.Common;
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

namespace EasySQL.Ventanas.Operaciones
{
    /// <summary>
    /// Lógica de interacción para VOperacionGenerica.xaml
    /// </summary>
    public partial class VGenericaDrop : Window
    {
        private const string CMB_OPCION_DEFECTO_DATABASE = "Elige base de datos...";
        private const string CMB_OPCION_DEFECTO_TABLE = "Elige tabla...";
        private const string CLICK_ERROR_DATABASE = "Debes elegir una base de datos";
        private const string CLICK_ERROR_TABLE = "Debes elegir una tabla";
        private const string CLICK_OK_DATABASE = "Base de datos ";
        private const string CLICK_OK_TABLE = "Tabla ";
        private static string CMB_OPCION_DEFECTO;
        private static string CLICK_ERROR;
        private static string CLICK_OK;
        private Conexion conexionActual;
        private DbCommand comandoEnviar;
        private string textoComandoOriginal;
        private enum Modo { DROP_DATABASE, DROP_TABLE };

        public VGenericaDrop(string descripcion, Conexion actual, DbCommand comando)
        {
            InitializeComponent();
            lbl.Content = descripcion;
            lblComando.Content = comando.CommandText;
            this.conexionActual = actual;
            this.comandoEnviar = comando;
            this.textoComandoOriginal = comando.CommandText;
            this.Title = textoComandoOriginal;
            if (textoComandoOriginal.Contains("DATABASE"))
                cambiarModo(Modo.DROP_DATABASE);
            else if (textoComandoOriginal.Contains("TABLE"))
                cambiarModo(Modo.DROP_TABLE);
        }

        private void cambiarModo(Modo actual)
        {
            if (actual.Equals(Modo.DROP_DATABASE))
            {
                CMB_OPCION_DEFECTO = CMB_OPCION_DEFECTO_DATABASE;
                CLICK_ERROR = CLICK_ERROR_DATABASE;
                CLICK_OK = CLICK_OK_DATABASE;
            }
            else if (actual.Equals(Modo.DROP_TABLE))
            {
                CMB_OPCION_DEFECTO = CMB_OPCION_DEFECTO_TABLE;
                CLICK_ERROR = CLICK_ERROR_TABLE;
                CLICK_OK = CLICK_OK_TABLE;
            }
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            if (cmbDatos.SelectedItem != null
                   && !cmbDatos.SelectedItem.Equals(CMB_OPCION_DEFECTO))
            {
                comandoEnviar.CommandText = textoComandoOriginal + cmbDatos.SelectedItem;
                int resultado = Ayudante.ExecuteNonQuery(conexionActual, comandoEnviar);
                if (resultado == -1)
                {
                    MessageBox.Show(CLICK_OK + cmbDatos.SelectedItem + " eliminada con con éxito.");
                }
            }
            else
            {
                MessageBox.Show(CLICK_ERROR);
            }
        }

        private void cmbDatos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Comprobar que la base de datos no sea la por defecto
            if (cmbDatos.SelectedItem != null
                && !cmbDatos.SelectedItem.Equals(CMB_OPCION_DEFECTO))
            {
                // Actualiza label descriptivo
                lblComando.Content = textoComandoOriginal + cmbDatos.SelectedItem;
                // Asigna nombre SOLO PARA TABLE
                //conexionActual.BaseDatos = cmbDatos.SelectedItem.ToString();
            }
            else
            {
                lblComando.Content = textoComandoOriginal;
            }
        }

        private void cmbDatos_DropDownOpened(object sender, EventArgs e)
        {
            // Mostrar bases de datos
            cmbDatos.Items.Clear();
            List<string> nombres_bbdd = Operacion.ObtenerBasesDatos(conexionActual);
            nombres_bbdd.Insert(0, CMB_OPCION_DEFECTO);
            Rellena.ComboBox(cmbDatos, nombres_bbdd);
            cmbDatos.SelectedIndex = 0;
        }
    }
}
