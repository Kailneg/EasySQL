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

        public static async Task<string> ExtraerDatosCondicionesWhere(StackPanel contenedor)
        {
            // Espera 5ms para que de tiempo a repintar los componentes
            await Task.Delay(5);
            // Comprobar los campos de las condiciones generados y extraer datos
            string datos = "";
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
    }
}
