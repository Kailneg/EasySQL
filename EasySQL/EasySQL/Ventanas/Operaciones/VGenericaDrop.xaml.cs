using EasySQL.Modelos;
using EasySQL.Operaciones.Ayudante;
using EasySQL.Operaciones.Controlador;
using EasySQL.Utils;
using System;
using System.Collections.Generic;
using System.Data;
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
        private const string DESCRIPCION_DATABASE = "Elige nombre de la BBDD a eliminar:";
        private const string DESCRIPCION_TABLE = "Elige nombre de la tabla a eliminar:";
        private const string CMB_OPCION_DEFECTO_DATABASE = "Elige base de datos...";
        private const string CMB_OPCION_DEFECTO_TABLE = "Elige tabla...";
        private const string CLICK_ERROR_DATABASE = "Debes elegir una base de datos";
        private const string CLICK_ERROR_TABLE = "Debes elegir una tabla";
        private const string CLICK_OK_DATABASE = "Base de datos ";
        private const string CLICK_OK_TABLE = "Tabla ";
        private static string DESCRIPCION;
        private static string CMB_OPCION_DEFECTO;
        private static string CLICK_ERROR;
        private static string CLICK_OK;
        private Conexion conexionActual;
        private DbCommand comandoEnviar;
        private string textoComandoOriginal;
        private enum Modo { DROP_DATABASE, DROP_TABLE };
        private Modo modoActual;

        public VGenericaDrop(Conexion actual, DbCommand comando)
        {
            InitializeComponent();
            lblComando.Content = comando.CommandText;
            this.conexionActual = actual;
            this.comandoEnviar = comando;
            this.textoComandoOriginal = comando.CommandText;
            this.Title = textoComandoOriginal;
            if (textoComandoOriginal.Contains("DATABASE"))
                modoActual = Modo.DROP_DATABASE;
            else if (textoComandoOriginal.Contains("TABLE"))
                modoActual = Modo.DROP_TABLE;
            cambiarModo();
            lblDescripcion.Content = DESCRIPCION;
            // Agrega y muestra la opción por defecto en el combobox.
            cmbDatos.Items.Add(CMB_OPCION_DEFECTO);
            cmbDatos.SelectedIndex = 0;
        }

        private void cambiarModo()
        {
            if (modoActual.Equals(Modo.DROP_DATABASE))
            {
                CMB_OPCION_DEFECTO = CMB_OPCION_DEFECTO_DATABASE;
                CLICK_ERROR = CLICK_ERROR_DATABASE;
                CLICK_OK = CLICK_OK_DATABASE;
                DESCRIPCION = DESCRIPCION_DATABASE;
            }
            else if (modoActual.Equals(Modo.DROP_TABLE))
            {
                CMB_OPCION_DEFECTO = CMB_OPCION_DEFECTO_TABLE;
                CLICK_ERROR = CLICK_ERROR_TABLE;
                CLICK_OK = CLICK_OK_TABLE;
                DESCRIPCION = DESCRIPCION_TABLE;
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
                    MessageBox.Show(CLICK_OK + "\"" + cmbDatos.SelectedItem + "\"" + " eliminada con con éxito.");
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
            }
            else
            {
                lblComando.Content = textoComandoOriginal;
            }
        }

        private void cmbDatos_DropDownOpened(object sender, EventArgs e)
        {
            List<string> nombresCmb = new List<string>();

            // Ejecuta un reader para obtener las bases de datos o tablas pertinentes
            if (modoActual.Equals(Modo.DROP_DATABASE))
            {
                // Obtener lista nombres BBDD
                nombresCmb = Ayudante.MapearReaderALista(
                    Ayudante.ObtenerReaderBasesDatos(conexionActual));
            }
            else if (modoActual.Equals(Modo.DROP_TABLE))
            {
                // Obtener lista nombres tablas
                nombresCmb = Ayudante.MapearReaderALista(
                    Ayudante.ObtenerReaderTablas(conexionActual));
            }
            // Rellena el combobox con las bases de datos o tablas pertinentes
            nombresCmb.Insert(0, CMB_OPCION_DEFECTO);
            cmbDatos.Items.Clear();
            Rellena.ComboBox(cmbDatos, nombresCmb);
            cmbDatos.SelectedIndex = 0;
        }
    }
}
