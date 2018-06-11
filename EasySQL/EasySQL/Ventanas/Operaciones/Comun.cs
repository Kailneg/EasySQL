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
        /// <summary>
        /// Opción por defecto al elegir una base de datos
        /// </summary>
        private const string CMB_OPCION_DEFECTO_DATABASE = "Elige base de datos...";

        /// <summary>
        /// Opción por defecto al elegir una tabla
        /// </summary>
        private const string CMB_OPCION_DEFECTO_TABLE = "Elige tabla...";

        /// <summary>
        /// Comprueba si está elegida la opción por defecto.
        /// </summary>
        /// <param name="combo">El ComboBox que se desea comprobar.</param>
        /// <returns>True si está elegida la opción por defecto.</returns>
        public static bool ElegidaTablaDefecto(ComboBox combo)
        {
            return combo.SelectedItem == null
                || combo.SelectedItem.Equals(CMB_OPCION_DEFECTO_TABLE);
        }

        /// <summary>
        /// Comprueba si está elegida la opción por defecto.
        /// </summary>
        /// <param name="combo">El ComboBox que se desea comprobar.</param>
        /// <returns>True si está elegida la opción por defecto.</returns>
        public static bool ElegidaBaseDatosDefecto(ComboBox combo)
        {
            return combo.SelectedItem == null
                || combo.SelectedItem.Equals(CMB_OPCION_DEFECTO_DATABASE);
        }

        /// <summary>
        /// Rellena un ComboBox con el nombre de las tablas de la BBDD actual.
        /// </summary>
        /// <param name="actual">La conexión actual.</param>
        /// <param name="aRellenar">El ComboBox a rellenar.</param>
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

        /// <summary>
        /// Rellena un ComboBox con el nombre de las BBDD de la conexión actual.
        /// </summary>
        /// <param name="actual">La conexión actual.</param>
        /// <param name="aRellenar">El ComboBox a rellenar.</param>
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

        /// <summary>
        /// Genera una serie de controles que se insertan en un contenedor.
        /// Usado para mostrar los campos de un SELECT
        /// </summary>
        /// <param name="contenedor">El contenedor donde serán los controles insertados.</param>
        /// <param name="actual">La conexión actual.</param>
        /// <param name="nombreTabla">Nombre de la tabla actual.</param>
        /// <param name="handlerEventosCheckboxs">Evento al que llamarán los checkboxes
        /// generados.</param>
        public static void GenerarCamposSelect(StackPanel contenedor, Conexion actual, string nombreTabla,
            Action<object, RoutedEventArgs> handlerEventosCheckboxs)
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
                // Se le asignan las columnas 1* 5* 4*
                ColumnDefinition gridCol0 = new ColumnDefinition();
                gridCol0.Width = new GridLength(1, GridUnitType.Star);
                ColumnDefinition gridCol1 = new ColumnDefinition();
                gridCol1.Width = new GridLength(5, GridUnitType.Star);
                ColumnDefinition gridCol2 = new ColumnDefinition();
                gridCol2.Width = new GridLength(4, GridUnitType.Star);

                contenedorHijo.ColumnDefinitions.Add(gridCol0);
                contenedorHijo.ColumnDefinitions.Add(gridCol1);
                contenedorHijo.ColumnDefinitions.Add(gridCol2);

                // Debe tener
                // chkElegir
                // txtColumna
                // txtTipoDato

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

                // Se asignan posiciones para los hijos del grid padre
                Grid.SetColumn(chkElegir, 0);
                Grid.SetColumn(txtColumna, 1);
                Grid.SetColumn(txtTipoDato, 2);

                // Se asignan eventos a los controles dinámicos
                chkElegir.Checked += new RoutedEventHandler(handlerEventosCheckboxs);
                chkElegir.Unchecked += new RoutedEventHandler(handlerEventosCheckboxs);

                // Se añaden los elementos a sus respectivas posiciones
                contenedorHijo.Children.Add(chkElegir);
                contenedorHijo.Children.Add(txtColumna);
                contenedorHijo.Children.Add(txtTipoDato);

                contenedor.Children.Add(contenedorHijo);
            }
        }

        /// <summary>
        /// Extrae los datos de un contenedor con controles generados.
        /// </summary>
        /// <param name="contenedor">El control del que extraer los datos.</param>
        /// <returns>Una lista de datos.</returns>
        public static async Task<List<ColumnaValor>> ExtraerDatosCamposSelect(StackPanel contenedor)
        {
            // Espera 5ms para que de tiempo a repintar los componentes
            await Task.Delay(5);
            // Comprobar los campos de las condiciones generados y extraer datos
            List<ColumnaValor> datos = new List<ColumnaValor>();
            var generados = contenedor.Children;


            // ||| Recorrer el grid |||
            // +Grid[i]
            //  -chkElegir[i][0] -> Comprobar
            //  -txtColumna[i][1] -> Obtener nombre
            //  -txtTipoDato[i][2]
            for (int i = 0; i < generados.Count; i++)
            {
                if (generados[i] is Grid)
                {
                    Grid grid = generados[i] as Grid;
                    CheckBox chkElegir = grid.Children[0] as CheckBox;
                    TextBox txtColumna = grid.Children[1] as TextBox;

                    // Por cada fila con chkElegir marcado, extraer datos columna y valor
                    if (chkElegir.IsChecked.Value)
                    {
                        string columna = txtColumna.Text?.ToString();
                        ColumnaValor filaDatos = new ColumnaValor(columna, "");

                        datos.Add(filaDatos);
                    }
                }
            }
            return datos;
        }

        /// <summary>
        /// Genera una serie de controles que se insertan en un contenedor.
        /// </summary>
        /// <param name="contenedor">El contenedor donde serán los controles insertados.</param>
        /// <param name="actual">La conexión actual.</param>
        /// <param name="nombreTabla">Nombre de la tabla actual.</param>
        /// <param name="handlerEventosCheckboxs">Evento al que llamarán 
        /// los checkboxes generados.</param>
        /// <param name="handlerEventosTextboxs">Evento al que llamarán 
        /// los textboxes generados.</param>
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

        /// <summary>
        /// Extrae los datos de un contenedor con controles generados.
        /// </summary>
        /// <param name="contenedor">El control del que extraer los datos.</param>
        /// <returns>Una lista de datos.</returns>
        public static async Task<List<ColumnaValor>> ExtraerDatosCamposColumnas(StackPanel contenedor)
        {
            // Espera 5ms para que de tiempo a repintar los componentes
            await Task.Delay(5);
            // Comprobar los campos de las condiciones generados y extraer datos
            List<ColumnaValor> datos = new List<ColumnaValor>();
            var generados = contenedor.Children;

            
            // ||| Recorrer el grid |||
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

                    // Por cada fila con chkElegir marcado, extraer datos columna y valor
                    if (chkElegir.IsChecked.Value)
                    {
                        string columna = txtColumna.Text?.ToString();
                        string valor = txtValor.Text?.ToString();
                        ColumnaValor filaDatos = new ColumnaValor(columna, valor);

                        datos.Add(filaDatos);
                    }
                }
            }
            return datos;
        }

        /// <summary>
        /// Genera una serie de controles que se insertan en un contenedor.
        /// </summary>
        /// <param name="contenedor">El contenedor donde serán los controles insertados.</param>
        /// <param name="actual">La conexión actual.</param>
        /// <param name="numCondiciones">Número de condiciones a generar.</param>
        /// <param name="nombreTabla">Nombre de la tabla actual.</param>
        /// <param name="handlerEventosCheckboxs">Evento al que llamarán 
        /// los checkboxes generados.</param>
        /// <param name="handlerEventosTextboxs">Evento al que llamarán 
        /// los textboxes generados.</param>
        public static void GenerarCamposWhere(StackPanel contenedor, Conexion actual, int numCondiciones, string nombreTabla,
            Action<object, SelectionChangedEventArgs> handlerEventosCombos, Action<object, TextChangedEventArgs> handlerEventosTextboxs)
        {
            string[] tipos_operadores = Operacion.TIPOS_CONDICIONES;
            string[] tipos_operadores_union = Operacion.TIPOS_CONDICIONES_UNION;
            List<string> nombre_columnas =
                Ayudante.MapearReaderALista(
                    Ayudante.ObtenerReaderColumnas(actual, nombreTabla));
            nombre_columnas.Insert(0, "");

            // Se vacía de contenido el stackpanel
            contenedor.Children.Clear();

            for (int i = 0; i < numCondiciones; i++)
            {
                // GRID PADRE
                Grid contenedorSuperior = new Grid();
                // Se le asignan las columnas 4* 2.5* 5*
                ColumnDefinition gridCol1 = new ColumnDefinition();
                gridCol1.Width = new GridLength(4, GridUnitType.Star);
                ColumnDefinition gridCol2 = new ColumnDefinition();
                gridCol2.Width = new GridLength(2.5, GridUnitType.Star);
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

        /// <summary>
        /// Extrae los datos de un contenedor con controles generados.
        /// </summary>
        /// <param name="contenedor">El control del que extraer los datos.</param>
        /// <returns>Una lista de datos.</returns>
        public static async Task<string> ExtraerDatosWhere(StackPanel contenedor)
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

        /// <summary>
        /// Genera una serie de controles que se insertan en un contenedor.
        /// </summary>
        /// <param name="contenedor">El contenedor donde serán los controles insertados.</param>
        /// <param name="actual">La conexión actual.</param>
        /// <param name="nombreTabla">Nombre de la tabla actual.</param>
        /// <param name="handlerEventosCheckboxs">Evento al que llamarán 
        /// los checkboxes generados.</param>
        /// <param name="handlerEventosTextboxs">Evento al que llamarán 
        /// los textboxes generados.</param>
        public static void GenerarCamposOrderBy(StackPanel contenedor, Conexion actual, int numCampos,
            List<ColumnaValor> camposElegidos, string nombreTabla, Action<object, SelectionChangedEventArgs> handlerEventosCombos)
        {
            string[] tipos_operadores = Operacion.TIPOS_ORDEN;

            // Se vacía de contenido el stackpanel
            contenedor.Children.Clear();

            for (int i = 0; i < numCampos; i++)
            {
                // GRID PADRE
                Grid contenedorSuperior = new Grid();
                // Se le asignan las columnas 1* 4* .5*
                ColumnDefinition gridCol1 = new ColumnDefinition();
                gridCol1.Width = new GridLength(1, GridUnitType.Star);
                ColumnDefinition gridCol2 = new ColumnDefinition();
                gridCol2.Width = new GridLength(4, GridUnitType.Star);
                ColumnDefinition gridCol3 = new ColumnDefinition();
                gridCol3.Width = new GridLength(1.5, GridUnitType.Star);

                contenedorSuperior.ColumnDefinitions.Add(gridCol1);
                contenedorSuperior.ColumnDefinitions.Add(gridCol2);
                contenedorSuperior.ColumnDefinitions.Add(gridCol3);

                // LBL POSICION
                Label lblPosicion = new Label();
                lblPosicion.Height = 25;
                lblPosicion.Content = i + 1 + "º";
                lblPosicion.VerticalAlignment = VerticalAlignment.Center;
                lblPosicion.HorizontalAlignment = HorizontalAlignment.Center;

                // CMB COLUMNA
                ComboBox cmbColumna = new ComboBox();
                cmbColumna.Height = 25;
                cmbColumna.Items.Add("");
                foreach (var columna in camposElegidos)
                {
                    cmbColumna.Items.Add(columna.Columna);
                }

                // CMB OPERADORES
                ComboBox cmbOperadores = new ComboBox();
                cmbOperadores.Height = 25;
                cmbOperadores.Margin = new Thickness(0, 5, 0, 5);
                foreach (var tipoDato in tipos_operadores)
                {
                    cmbOperadores.Items.Add(tipoDato);
                }

                // Se asignan posiciones para los hijos del grid padre
                Grid.SetColumn(lblPosicion, 0);
                Grid.SetColumn(cmbColumna, 1);
                Grid.SetColumn(cmbOperadores, 2);

                // Se asignan eventos a los controles dinámicos
                cmbColumna.SelectionChanged += new SelectionChangedEventHandler(handlerEventosCombos);
                cmbOperadores.SelectionChanged += new SelectionChangedEventHandler(handlerEventosCombos);

                // Se añaden los elementos a sus respectivas posiciones
                contenedorSuperior.Children.Add(lblPosicion);
                contenedorSuperior.Children.Add(cmbColumna);
                contenedorSuperior.Children.Add(cmbOperadores);

                contenedor.Children.Add(contenedorSuperior);
            }
        }

        /// <summary>
        /// Extrae los datos de un contenedor con controles generados.
        /// </summary>
        /// <param name="contenedor">El control del que extraer los datos.</param>
        /// <returns>Una lista de datos.</returns>
        public static async Task<List<ColumnaValor>> ExtraerDatosOrderBy(StackPanel contenedor)
        {
            // Espera 5ms para que de tiempo a repintar los componentes
            await Task.Delay(5);
            // Comprobar los campos de las condiciones generados y extraer datos
            var generados = contenedor.Children;

            List<ColumnaValor> datos = new List<ColumnaValor>();

            // Por cada campo a ordenar, emparejarlo con su sentido ASC|DESC
            // +Grid[i]
            //  -lblPosicion[i][0]
            //  -cmbColumna[i][1]
            //  -cmbOperadores[i][2]
            for (int i = 0; i < generados.Count; i++)
            {
                if (generados[i] is Grid)
                {
                    Grid grid = generados[i] as Grid;
                    ComboBox cmbColumna = grid.Children[1] as ComboBox;
                    ComboBox cmbOperadores = grid.Children[2] as ComboBox;

                    // Datos
                    string columna = cmbColumna.SelectedItem?.ToString();
                    string operador = cmbOperadores.SelectedItem?.ToString();
                    datos.Add(new ColumnaValor(columna, operador));
                }
            }
            return datos;
        }

        /// <summary>
        /// Marca o no todos los checkboxes del contenedor proporcionado.
        /// </summary>
        /// <param name="contenedor">El contenedor del que marcar los checkboxes.</param>
        /// <param name="marcado">True si se desean marcar, false los desmarca.</param>
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
