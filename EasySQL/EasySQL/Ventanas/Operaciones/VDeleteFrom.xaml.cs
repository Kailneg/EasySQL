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

        private void ReestablecerCampos()
        {
            // Si no, resetea el label
            cmbNumCondiciones.SelectedIndex = 0;
            stackCondiciones.Children.Clear();
            // Comprobar que la tabla no sea la por defecto
            if (!Comprueba.ElegidaOpcionDefecto(cmbTablas, CMB_OPCION_DEFECTO))
                comandoEnviar.CommandText = textoComandoOriginal + cmbTablas.SelectedItem.ToString();
            else
                comandoEnviar.CommandText = textoComandoOriginal;

            // Muestra el contenido del comando actual en el label
            lblComando.Content = comandoEnviar.CommandText;
        }

        private void GenerarCamposCondiciones()
        {
            int numCondiciones = (int)cmbNumCondiciones.SelectedItem;
            string[] tipos_operadores = Operacion.TIPOS_OPERADORES;
            string[] tipos_operadores_union = Operacion.TIPOS_OPERADORES_UNION;
            string nombreTabla = cmbTablas.SelectedItem.ToString();
            List<string> nombre_columnas =
                Ayudante.MapearReaderALista(
                    Ayudante.ObtenerReaderColumnas(conexionActual, nombreTabla));

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
            // Elimina el último ComboBox AND OR
            int tamanio = stackCondiciones.Children.Count;
            if (tamanio > 0)
                stackCondiciones.Children.RemoveAt(tamanio - 1);
        }

        private void ExtraerCondicionesGeneradas()
        {
            // Comprobar los campos de las condiciones generados y extraer datos
            string datos = "WHERE ";
            var generados = stackCondiciones.Children;

            // Por cada condicion existente, extraerlas y emparejarlas con un AND u OR
            // +Grid[i]
            //  -cmbColumna[i][0]
            //  -cmbOperadores[i][1]
            //  -txtValor[i][2]
            // +cmbAndOr[i]
            for (int i = 0; i < generados.Count; i++)
            {
                string columna, operador, valor, andOr;
                if (generados[i] is Grid)
                {
                    Grid grid = generados[i] as Grid;
                    ComboBox cmbColumna = grid.Children[0] as ComboBox;
                    ComboBox cmbOperadores = grid.Children[1] as ComboBox;
                    TextBox txtValor = grid.Children[2] as TextBox;

                    // Test
                    Console.WriteLine(cmbColumna.SelectedItem?.ToString());
                    Console.WriteLine(cmbOperadores.SelectedItem?.ToString());
                    Console.WriteLine(txtValor.Text?.ToString());
                }
                else if (generados[i] is ComboBox)
                {
                    ComboBox cmbAndOr = generados[i] as ComboBox;
                    Console.WriteLine(cmbAndOr.SelectedItem?.ToString());
                }
            }
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

        private void cmbTabla_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ReestablecerCampos();
        }

        private void cmbTabla_DropDownOpened(object sender, EventArgs e)
        {
            List<string> nombresTablas =
                Ayudante.MapearReaderALista(Ayudante.ObtenerReaderTablas(conexionActual));
            // Rellena el combobox con las bases de datos o tablas pertinentes
            nombresTablas.Insert(0, CMB_OPCION_DEFECTO);
            cmbTablas.Items.Clear();
            Rellena.ComboBox(cmbTablas, nombresTablas);
            cmbTablas.SelectedIndex = 0;
        }

        private void cmbNumCondiciones_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!Comprueba.ElegidaOpcionDefecto(cmbTablas, CMB_OPCION_DEFECTO))
            {
                // Si hay tabla elegida, se muestan los campos correspondientes
                GenerarCamposCondiciones();
            }
        }

        private void cmbGenerado_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ExtraerCondicionesGeneradas();
        }

        private void txtGenerado_TextChanged(object sender, TextChangedEventArgs e)
        {
            ExtraerCondicionesGeneradas();
        }

    }
}
