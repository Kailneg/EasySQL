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
    /// Lógica de interacción para VAlterTable.xaml
    /// </summary>
    public partial class VAlterTable : Window
    {
        private const string CMB_OPCION_DEFECTO = "Elige tabla...";
        private const string CMB_OPERACION_DEFECTO = "Elige operación...";
        private const string CMB_OPERACION_AÑADIR = "Añadir columna";
        private const string CMB_OPERACION_ELIMINAR = "Eliminar columna";
        private Conexion conexionActual;
        private DbCommand comandoEnviar;
        private string textoComandoOriginal;

        public VAlterTable(Conexion actual)
        {
            InitializeComponent();
            this.conexionActual = actual;
            this.Title += " || Base de datos \"" + actual.BaseDatos + "\"";
            // Obtiene el comando SQL correspondiente
            this.comandoEnviar = Operacion.ComandoAlterTable(actual);
            lblComando.Content = comandoEnviar.CommandText;
            this.textoComandoOriginal = comandoEnviar.CommandText;

            // Agrega y muestra la opción por defecto en el combobox.
            cmbTablas.Items.Add(CMB_OPCION_DEFECTO);
            cmbTablas.SelectedIndex = 0;

            // Agrega tipos operaciones muestra la opción por defecto en el combobox.
            cmbTipoOperacion.Items.Add(CMB_OPERACION_DEFECTO);
            cmbTipoOperacion.Items.Add(CMB_OPERACION_AÑADIR);
            cmbTipoOperacion.Items.Add(CMB_OPERACION_ELIMINAR);
            cmbTipoOperacion.SelectedIndex = 0;
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            int resultado = Ayudante.ExecuteNonQuery(conexionActual, comandoEnviar);
            if (resultado == -1)
            {
                MessageBox.Show("Tabla \"" + Comprueba.EliminarResto(cmbTablas.SelectedItem.ToString()) 
                    + "\" en base de datos " + "\"" + conexionActual.BaseDatos + "\" modificada con éxito.");
            }
        }

        private void cmbTablas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Comprobar que la tabla no sea la por defecto
            if (cmbTablas.SelectedItem != null
                && !cmbTablas.SelectedItem.Equals(CMB_OPCION_DEFECTO))
            {
                // Actualiza label descriptivo
                lblComando.Content = textoComandoOriginal + cmbTablas.SelectedItem;
            }
            else
            {
                lblComando.Content = textoComandoOriginal;
            }
            cmbTipoOperacion.SelectedIndex = 0;
            TipoOperacionCambiada();
        }

        private void cmbTablas_DropDownOpened(object sender, EventArgs e)
        {
            List<string> nombresTablas = new List<string>();

            // Ejecuta un reader para obtener las o tablas pertinentes
           
            // Obtener comando tables, reemplazar el parametro con el nombre de la bbdd actual
            DbCommand comando = Operacion.ComandoShowTables(conexionActual);
            comando.CommandText = comando.CommandText.Replace(Operacion.PARAM, conexionActual.BaseDatos);

            using (IDataReader lector = Ayudante.ExecuteReader(conexionActual, comando))
            {
                // Si el resultado es nulo, no existen bases de datos.
                if (lector != null)
                {
                    nombresTablas = Ayudante.MapearReaderALista(lector);
                }
            }
            // Rellena el combobox con las bases de datos o tablas pertinentes
            nombresTablas.Insert(0, CMB_OPCION_DEFECTO);
            cmbTablas.Items.Clear();
            Rellena.ComboBox(cmbTablas, nombresTablas);
            cmbTablas.SelectedIndex = 0;
        }

        private void cmbTipoOperacion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TipoOperacionCambiada();
            
        }

        private void TipoOperacionCambiada()
        {
            // Comprobar que la tabla no sea la por defecto
            if (cmbTipoOperacion.SelectedItem != null
                && !cmbTipoOperacion.SelectedItem.Equals(CMB_OPERACION_DEFECTO))
            {
                // Si se desea añadir columna
                if (cmbTipoOperacion.SelectedItem.Equals(CMB_OPERACION_AÑADIR))
                {
                    OperacionAñadir();
                }
                // Si se desea eliminar columna
                else if (cmbTipoOperacion.SelectedItem.Equals(CMB_OPERACION_ELIMINAR))
                {
                    OperacionEliminar();
                }
            }
            else
            {
                // Se ocultan labels
                lblNombreColumna.Visibility = Visibility.Hidden;
                lblTipoDato.Visibility = Visibility.Hidden;
                separador.Visibility = Visibility.Hidden;
                // Se elimina posible eleccion anterior
                gridTipoOperacion.Children.Clear();
            }
        }

        private void OperacionAñadir()
        {
            // Se muestran labels
            lblNombreColumna.Visibility = Visibility.Visible;
            lblTipoDato.Visibility = Visibility.Visible;
            separador.Visibility = Visibility.Visible;

            // Se crean los controles dinámicos
            TextBox campo = new TextBox();
            campo.Height = 25;
            
            ComboBox combo = new ComboBox();
            combo.Height = 25;
            foreach (var tipoDato in Operacion.TiposDatos(conexionActual))
            {
                combo.Items.Add(tipoDato);
            }

            // Se resetea el grid que los contiene
            gridTipoOperacion.Children.Clear();
            gridTipoOperacion.ColumnDefinitions.Clear();

            // Se le asignan las columnas 3* 2*
            ColumnDefinition gridCol1 = new ColumnDefinition();
            gridCol1.Width = new GridLength(3, GridUnitType.Star);
            ColumnDefinition gridCol2 = new ColumnDefinition();
            gridCol2.Width = new GridLength(3, GridUnitType.Star);

            gridTipoOperacion.ColumnDefinitions.Add(gridCol1);
            gridTipoOperacion.ColumnDefinitions.Add(gridCol2);

            // Se asignan posiciones de los elementos
            Grid.SetColumn(campo, 0);
            Grid.SetColumn(combo, 1);

            // Se añaden los elementos al Grid
            gridTipoOperacion.Children.Add(campo);
            gridTipoOperacion.Children.Add(combo);
        }

        private void OperacionEliminar()
        {
            // Se muestan labels
            lblNombreColumna.Visibility = Visibility.Visible;
            lblTipoDato.Visibility = Visibility.Hidden;
            separador.Visibility = Visibility.Visible;

            // Se crean los controles dinámicos
            ComboBox combo = new ComboBox();
            combo.Height = 25;
            foreach (var tipoDato in ObtenerColumnas())
            {
                combo.Items.Add(tipoDato);
            }

            // Se resetea el grid que los contiene
            gridTipoOperacion.Children.Clear();
            gridTipoOperacion.ColumnDefinitions.Clear();

            // Se le asignan la columna 1*
            ColumnDefinition gridCol1 = new ColumnDefinition();
            gridCol1.Width = new GridLength(1, GridUnitType.Star);

            gridTipoOperacion.ColumnDefinitions.Add(gridCol1);

            // Se asignan posiciones de los elementos
            Grid.SetColumn(combo, 0);
            Grid.SetColumn(combo, 1);

            // Se añaden los elementos al Grid
            gridTipoOperacion.Children.Add(combo);
        }

        private List<string> ObtenerColumnas()
        {
            List<string> nombresColumnas = new List<string>();

            // Ejecuta un reader para obtener los nombres de columnas

            // Obtener comando tables, reemplazar el parametro con el nombre de la bbdd actual
            DbCommand comando = Operacion.ComandoShowColumnas(conexionActual);
            comando.CommandText = comando.CommandText.Replace(Operacion.PARAM, conexionActual.BaseDatos);
            comando.CommandText = comando.CommandText.Replace(Operacion.PARAM2, cmbTablas.SelectedItem.ToString());

            using (IDataReader lector = Ayudante.ExecuteReader(conexionActual, comando))
            {
                // Si el resultado es nulo, no existen bases de datos.
                if (lector != null)
                {
                    nombresColumnas = Ayudante.MapearReaderALista(lector);
                }
            }
            
            return nombresColumnas;
        }
    }
}
