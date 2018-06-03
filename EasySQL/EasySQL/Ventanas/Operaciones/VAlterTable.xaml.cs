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
            cmbDatos.Items.Add(CMB_OPCION_DEFECTO);
            cmbDatos.SelectedIndex = 0;

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
                MessageBox.Show("Tabla \"" + Comprueba.EliminarResto(cmbDatos.SelectedItem.ToString()) 
                    + "\" en base de datos " + "\"" + conexionActual.BaseDatos + "\" modificada con éxito.");
            }
        }

        private void cmbTablas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Comprobar que la tabla no sea la por defecto
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

        private void cmbTablas_DropDownOpened(object sender, EventArgs e)
        {
            List<string> nombres_cmbox = new List<string>();

            // Ejecuta un reader para obtener las o tablas pertinentes
           
            // Obtener comando tables, reemplazar el parametro con el nombre de la bbdd actual
            DbCommand comando = Operacion.ComandoShowTables(conexionActual);
            comando.CommandText = comando.CommandText.Replace(Operacion.PARAM, conexionActual.BaseDatos);

            using (IDataReader lector = Ayudante.ExecuteReader(conexionActual, comando))
            {
                // Si el resultado es nulo, no existen bases de datos.
                if (lector != null)
                {
                    nombres_cmbox = Ayudante.MapearReaderALista(lector);
                }
            }
            // Rellena el combobox con las bases de datos o tablas pertinentes
            nombres_cmbox.Insert(0, CMB_OPCION_DEFECTO);
            cmbDatos.Items.Clear();
            Rellena.ComboBox(cmbDatos, nombres_cmbox);
            cmbDatos.SelectedIndex = 0;
        }

        private void cmbTipoOperacion_SelectionChanged(object sender, SelectionChangedEventArgs e)
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
                // Se elimina posible eleccion anterior
                gridTipoOperacion.Children.Clear();
            }
        }

        private void OperacionAñadir()
        {
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
            // Se crean los controles dinámicos
            ComboBox combo = new ComboBox();
            combo.Height = 25;
            foreach (var tipoDato in Operacion.TiposDatos(conexionActual))
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
    }
}
