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
    /// Interaction logic for VDeleteFrom.xaml
    /// </summary>
    public partial class VDeleteFrom : Window
    {
        private const string CMB_OPCION_DEFECTO = "Elige tabla...";
        private const int NUM_CONDICIONES_MAX = 10;
        private Conexion conexionActual;
        private DbCommand comandoEnviar;
        private TextBox txtBoxGenerado;
        private ComboBox cmbGenerado;
        private string textoComandoOriginal;

        /// <summary>
        /// Constructor para pruebas
        /// </summary>
        public VDeleteFrom() :
            this(
                    new Conexion()
                    {
                        Direccion = "localhost\\SQLALE",
                        TipoActual = Conexion.TipoConexion.MicrosoftSQL,
                        UsuarioConexion = Usuario.NombreIntegratedSecurity,
                        BaseDatos = "pruebas"
                    }
                )
        { }

        public VDeleteFrom(Conexion actual)
        {
            InitializeComponent();
            this.conexionActual = actual;
            this.Title += " || Base de datos \"" + actual.BaseDatos + "\"";
            // Obtiene el comando SQL correspondiente
            this.comandoEnviar = Operacion.ComandoDeleteFrom(actual);
            lblComando.Content = comandoEnviar.CommandText;
            this.textoComandoOriginal = comandoEnviar.CommandText;

            // Agrega y muestra la opción por defecto en el combobox Tablas.
            cmbTablas.Items.Add(CMB_OPCION_DEFECTO);
            cmbTablas.SelectedIndex = 0;

            // Agrega valores y muestra la opción por defecto en el combobox numCondiciones.
            for (int i = 0; i < NUM_CONDICIONES_MAX; i++)
            {
                cmbNumCondiciones.Items.Add(i);
            }
            cmbNumCondiciones.SelectedIndex = 0;
        }

        private void DatosCambiados()
        {
            // Si no, resetea el label
            comandoEnviar.CommandText = textoComandoOriginal + cmbTablas.SelectedItem.ToString();
            lblComando.Content = comandoEnviar.CommandText;
            cmbNumCondiciones.SelectedIndex = 0;
        }

        private void cmbTabla_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Comprobar que la tabla no sea la por defecto
            if (!Comprueba.ElegidaOpcionDefecto(cmbTablas, CMB_OPCION_DEFECTO))
            {
                // Se refleja la elección de la nueva tabla
                DatosCambiados();
            }
            else
            {
                // Si no, resetea el label
                lblComando.Content = textoComandoOriginal;
            }
        }

        private void cmbTabla_DropDownOpened(object sender, EventArgs e)
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

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            int resultado = Ayudante.ExecuteNonQuery(conexionActual, comandoEnviar);
            if (resultado == -1)
            {
                MessageBox.Show(
                    "Datos de la tabla \"" + 
                    Comprueba.EliminarResto(cmbTablas.SelectedItem.ToString()) +
                    "\" en base de datos " + "\"" + conexionActual.BaseDatos + 
                    "\" modificada con éxito.");
            }
        }

        private void ModificarNumCondiciones()
        {
            int numCondiciones = (int)cmbNumCondiciones.SelectedItem;
            string[] tipos_operadores = Operacion.TIPOS_OPERADORES;
            string[] tipos_operadores_union = Operacion.TIPOS_OPERADORES_UNION;
            List<string> nombre_columnas = ObtenerColumnas();

            // Se vacía de contenido el stackpanel
            stackCondiciones.Children.Clear();

            // Por cada condicion generar 1 Grid (2combos 1 textbox) 1Combo
            Grid contenedorSuperior;
            ComboBox cmbColumna;
            ComboBox cmbOperadores;
            TextBox txtValor;
            ComboBox cmbAndOr;

            for (int i = 0; i < numCondiciones; i++)
            {
                // GRID PADRE
                contenedorSuperior = new Grid();
                // Se le asignan las columnas 4* 2* 5*
                ColumnDefinition gridCol1 = new ColumnDefinition();
                gridCol1.Width = new GridLength(4, GridUnitType.Star);
                ColumnDefinition gridCol2 = new ColumnDefinition();
                gridCol2.Width = new GridLength(2, GridUnitType.Star);
                ColumnDefinition gridCol3 = new ColumnDefinition();
                gridCol3.Width = new GridLength(5, GridUnitType.Star);

                contenedorSuperior.ColumnDefinitions.Add(gridCol1);
                contenedorSuperior.ColumnDefinitions.Add(gridCol2);
                contenedorSuperior.ColumnDefinitions.Add(gridCol3);

                // CMB COLUMNA
                cmbColumna = new ComboBox();
                cmbColumna.Height = 25;
                foreach (var nombre in nombre_columnas)
                {
                    cmbColumna.Items.Add(nombre);
                }
                
                // CMB OPERADORES
                cmbOperadores = new ComboBox();
                cmbOperadores.Height = 25;
                cmbOperadores.Margin = new Thickness(0, 5, 0, 5);
                foreach (var tipoDato in tipos_operadores)
                {
                    cmbOperadores.Items.Add(tipoDato);
                }

                // TXTVALOR
                txtValor = new TextBox();
                cmbOperadores.Height = 25;
                
                // CMB AND OR
                cmbAndOr = new ComboBox();
                cmbAndOr.Margin = new Thickness(0, 5, 0, 5);
                cmbAndOr.HorizontalContentAlignment = HorizontalAlignment.Center;
                foreach (var tipoDato in tipos_operadores_union)
                {
                    cmbAndOr.Items.Add(tipoDato);
                }

                // Se asignan posiciones para los hijos del grid padre
                Grid.SetColumn(cmbColumna, 0);
                Grid.SetColumn(cmbOperadores, 1);
                Grid.SetColumn(txtValor, 2);

                // Se asignan eventos a los controles dinámicos
                cmbColumna.SelectionChanged += cmbGenerado_SelectionChanged;
                cmbOperadores.SelectionChanged += cmbGenerado_SelectionChanged;
                txtValor.TextChanged += txtGenerado_TextChanged;
                cmbAndOr.SelectionChanged += cmbGenerado_SelectionChanged;

                // Se añaden los elementos a sus respectivas posiciones
                contenedorSuperior.Children.Add(cmbColumna);
                contenedorSuperior.Children.Add(cmbOperadores);
                contenedorSuperior.Children.Add(txtValor);

                stackCondiciones.Children.Add(contenedorSuperior);
                stackCondiciones.Children.Add(cmbAndOr);
            }
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

        private void cmbNumCondiciones_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbNumCondiciones.SelectedItem != null)
                ModificarNumCondiciones();
        }

        private void cmbGenerado_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DatosCambiados();
        }

        private void txtGenerado_TextChanged(object sender, TextChangedEventArgs e)
        {
            DatosCambiados();
        }

    }
}
