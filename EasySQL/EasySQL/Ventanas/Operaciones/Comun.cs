using EasySQL.Modelos;
using EasySQL.Operaciones.Ayudante;
using EasySQL.Operaciones.Controlador;
using EasySQL.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace EasySQL.Ventanas.Operaciones
{
    public class Comun
    {
        private const string CMB_OPCION_DEFECTO_DATABASE = "Elige base de datos...";
        private const string CMB_OPCION_DEFECTO_TABLE = "Elige tabla...";

        public static bool ElegidaTablaDefecto(ComboBox combo)
        {
            return combo.SelectedItem == null
                || combo.SelectedItem.Equals(CMB_OPCION_DEFECTO_TABLE);
        }

        public static bool ElegidaBaseDatosDefecto(ComboBox combo)
        {
            return combo.SelectedItem == null
                || combo.SelectedItem.Equals(CMB_OPCION_DEFECTO_DATABASE);
        }

        public static void RellenarComboTablas(Conexion actual, ComboBox aRellenar)
        {
            List<string> nombresTablas =
                Ayudante.MapearReaderALista(Ayudante.ObtenerReaderTablas(actual));
            // Rellena el combobox con las bases de datos o tablas pertinentes
            nombresTablas.Insert(0, CMB_OPCION_DEFECTO_TABLE);
            aRellenar.Items.Clear();
            Rellena.ComboBox(aRellenar, nombresTablas);
            aRellenar.SelectedIndex = 0;
        }

        public static void RellenarComboBasesDatos(Conexion actual, ComboBox aRellenar)
        {
            List<string> nombresBBDD =
                Ayudante.MapearReaderALista(Ayudante.ObtenerReaderBasesDatos(actual));
            // Rellena el combobox con las bases de datos o tablas pertinentes
            nombresBBDD.Insert(0, CMB_OPCION_DEFECTO_DATABASE);
            aRellenar.Items.Clear();
            Rellena.ComboBox(aRellenar, nombresBBDD);
            aRellenar.SelectedIndex = 0;
        }

        public static void GenerarCamposCondicionesWhere(StackPanel contenedor, Conexion actual, int numCondiciones, string nombreTabla,
            Action<object, SelectionChangedEventArgs> handlerEventosCombos, Action<object, TextChangedEventArgs> handlerEventosTextboxs)
        {
            string[] tipos_operadores = Operacion.TIPOS_OPERADORES;
            string[] tipos_operadores_union = Operacion.TIPOS_OPERADORES_UNION;
            List<string> nombre_columnas =
                Ayudante.MapearReaderALista(
                    Ayudante.ObtenerReaderColumnas(actual, nombreTabla));

            // Se vacía de contenido el stackpanel
            contenedor.Children.Clear();

            for (int i = 0; i < numCondiciones; i++)
            {
                // GRID PADRE
                Grid contenedorSuperior = new Grid();
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
                ComboBox cmbColumna = new ComboBox();
                cmbColumna.Height = 25;
                foreach (var nombre in nombre_columnas)
                {
                    cmbColumna.Items.Add(nombre);
                }

                // CMB OPERADORES
                ComboBox cmbOperadores = new ComboBox();
                cmbOperadores.Height = 25;
                cmbOperadores.Margin = new Thickness(0, 5, 0, 5);
                foreach (var tipoDato in tipos_operadores)
                {
                    cmbOperadores.Items.Add(tipoDato);
                }

                // TXTVALOR
                TextBox txtValor = new TextBox();
                cmbOperadores.Height = 25;

                // CMB AND OR
                ComboBox cmbAndOr = new ComboBox();
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
                cmbColumna.SelectionChanged += new SelectionChangedEventHandler(handlerEventosCombos);
                cmbOperadores.SelectionChanged += new SelectionChangedEventHandler(handlerEventosCombos);
                txtValor.TextChanged += new TextChangedEventHandler(handlerEventosTextboxs);
                cmbAndOr.SelectionChanged += new SelectionChangedEventHandler(handlerEventosCombos);

                // Se añaden los elementos a sus respectivas posiciones
                contenedorSuperior.Children.Add(cmbColumna);
                contenedorSuperior.Children.Add(cmbOperadores);
                contenedorSuperior.Children.Add(txtValor);

                contenedor.Children.Add(contenedorSuperior);
                contenedor.Children.Add(cmbAndOr);
            }
            // Elimina el último ComboBox AND OR
            int tamanio = contenedor.Children.Count;
            if (tamanio > 0)
                contenedor.Children.RemoveAt(tamanio - 1);
        }

        public static async Task<string> ExtraerDatosCondicionesWhere(StackPanel contenedor)
        {
            // Espera 5ms para que de tiempo a repintar los componentes
            await Task.Delay(5);
            // Comprobar los campos de las condiciones generados y extraer datos
            string datos = " WHERE ";
            var generados = contenedor.Children;

            // Por cada condicion existente, extraerlas y emparejarlas con un AND u OR
            // +Grid[i]
            //  -cmbColumna[i][0]
            //  -cmbOperadores[i][1]
            //  -txtValor[i][2]
            // +cmbAndOr[i]
            for (int i = 0; i < generados.Count; i++)
            {
                if (generados[i] is Grid)
                {
                    Grid grid = generados[i] as Grid;
                    ComboBox cmbColumna = grid.Children[0] as ComboBox;
                    ComboBox cmbOperadores = grid.Children[1] as ComboBox;
                    TextBox txtValor = grid.Children[2] as TextBox;

                    // Datos
                    datos += cmbColumna.SelectedItem?.ToString() + " ";
                    datos += cmbOperadores.SelectedItem?.ToString() + " ";
                    datos += txtValor.Text?.ToString() + " ";
                }
                else if (generados[i] is ComboBox)
                {
                    ComboBox cmbAndOr = generados[i] as ComboBox;
                    datos += cmbAndOr.SelectedItem?.ToString() + " ";
                }
            }
            return datos;
        }

        public static void GenerarCamposColumnas(StackPanel contenedor, Conexion actual, string nombreTabla,
            Action<object, RoutedEventArgs> handlerEventosCheckboxs, Action<object, TextChangedEventArgs> handlerEventosTextboxs)
        {
            List<string> nombre_columnas =
                Ayudante.MapearReaderALista(
                    Ayudante.ObtenerReaderColumnas(actual, nombreTabla));
            List<string> tipo_datos =
                Ayudante.MapearReaderALista(
                    Ayudante.ObtenerReaderTiposDatosColumnas(actual, nombreTabla));

            int numCamposGenerar = nombre_columnas.Count;

            // Se vacía de contenido el stackpanel
            contenedor.Children.Clear();

            for (int i = 0; i < numCamposGenerar; i++)
            {
                // GRID PADRE
                Grid contenedorHijo = new Grid();
                contenedorHijo.Margin = new Thickness(0, 5, 0, 0);
                // Se le asignan las columnas 1* 4* 2* 5*
                ColumnDefinition gridCol0 = new ColumnDefinition();
                gridCol0.Width = new GridLength(1, GridUnitType.Star);
                ColumnDefinition gridCol1 = new ColumnDefinition();
                gridCol1.Width = new GridLength(4, GridUnitType.Star);
                ColumnDefinition gridCol2 = new ColumnDefinition();
                gridCol2.Width = new GridLength(2, GridUnitType.Star);
                ColumnDefinition gridCol3 = new ColumnDefinition();
                gridCol3.Width = new GridLength(5, GridUnitType.Star);

                contenedorHijo.ColumnDefinitions.Add(gridCol0);
                contenedorHijo.ColumnDefinitions.Add(gridCol1);
                contenedorHijo.ColumnDefinitions.Add(gridCol2);
                contenedorHijo.ColumnDefinitions.Add(gridCol3);

                // Debe tener
                // chkElegir
                // txtColumna
                // txtTipoDato
                // txtValor

                // CHK ELEGIR
                CheckBox chkElegir = new CheckBox();
                chkElegir.HorizontalAlignment = HorizontalAlignment.Center;
                chkElegir.VerticalAlignment = VerticalAlignment.Center;

                // TXT COLUMNA
                TextBox txtColumna = new TextBox();
                txtColumna.Height = 25;
                txtColumna.IsReadOnly = true;
                txtColumna.Text = nombre_columnas[i];
                txtColumna.VerticalContentAlignment = VerticalAlignment.Center;
                txtColumna.HorizontalContentAlignment = HorizontalAlignment.Center;

                // TXT TIPOS DATOS
                TextBox txtTipoDato = new TextBox();
                txtTipoDato.Height = 25;
                txtTipoDato.IsReadOnly = true;
                txtTipoDato.Text = tipo_datos[i];
                txtTipoDato.Margin = new Thickness(5, 0, 5, 0);
                txtTipoDato.VerticalContentAlignment = VerticalAlignment.Center;
                txtTipoDato.HorizontalContentAlignment = HorizontalAlignment.Center;

                // TXT VALOR
                TextBox txtValor = new TextBox();
                txtValor.Height = 25;
                txtValor.VerticalContentAlignment = VerticalAlignment.Center;

                // Se asignan posiciones para los hijos del grid padre
                Grid.SetColumn(chkElegir, 0);
                Grid.SetColumn(txtColumna, 1);
                Grid.SetColumn(txtTipoDato, 2);
                Grid.SetColumn(txtValor, 3);

                // Se asignan eventos a los controles dinámicos
                chkElegir.Checked += new RoutedEventHandler(handlerEventosCheckboxs);
                chkElegir.Unchecked += new RoutedEventHandler(handlerEventosCheckboxs);
                txtColumna.TextChanged += new TextChangedEventHandler(handlerEventosTextboxs);
                txtTipoDato.TextChanged += new TextChangedEventHandler(handlerEventosTextboxs);
                txtValor.TextChanged += new TextChangedEventHandler(handlerEventosTextboxs);

                // Se añaden los elementos a sus respectivas posiciones
                contenedorHijo.Children.Add(chkElegir);
                contenedorHijo.Children.Add(txtColumna);
                contenedorHijo.Children.Add(txtTipoDato);
                contenedorHijo.Children.Add(txtValor);

                contenedor.Children.Add(contenedorHijo);
            }
        }

        public static async Task<string> ExtraerDatosCamposColumnas(StackPanel contenedor)
        {
            // Espera 5ms para que de tiempo a repintar los componentes
            await Task.Delay(5);
            // Comprobar los campos de las condiciones generados y extraer datos
            string datos = "";
            var generados = contenedor.Children;

            // Por cada condicion existente, extraerlas y emparejarlas con un AND u OR
            // +Grid[i]
            //  -chkElegir[i][0] -> Comprobar
            //  -txtColumna[i][1] -> Obtener nombre
            //  -txtTipoDato[i][2]
            //  -txtValor[i][3] -> Obtener datos
            for (int i = 0; i < generados.Count; i++)
            {
                if (generados[i] is Grid)
                {
                    Grid grid = generados[i] as Grid;
                    CheckBox chkElegir = grid.Children[0] as CheckBox;
                    TextBox txtColumna = grid.Children[1] as TextBox;
                    TextBox txtValor = grid.Children[3] as TextBox;

                    // Datos
                    if (chkElegir.IsChecked.Value)
                    {
                        datos += txtColumna.Text?.ToString() + " = ";
                        datos += txtValor.Text?.ToString() + ", ";
                    }
                }
            }
            if (datos.Length > 0)
                return datos.Substring(0, datos.Length - 2); // para eliminar la última coma y espacio
            else
                return datos;
        }

        public static void MarcarTodosCamposColumnas(StackPanel contenedor, bool marcado)
        {
            var generados = contenedor.Children;

            // Por cada condicion existente, extraerlas y emparejarlas con un AND u OR
            // +Grid[i]
            //  -chkElegir[i][0] -> Marcar
            //  -txtColumna[i][1] 
            //  -txtTipoDato[i][2]
            //  -txtValor[i][3]
            for (int i = 0; i < generados.Count; i++)
            {
                if (generados[i] is Grid)
                {
                    Grid grid = generados[i] as Grid;
                    CheckBox chkElegir = grid.Children[0] as CheckBox;

                    chkElegir.IsChecked = marcado;
                }
            }
        }
    }
}
