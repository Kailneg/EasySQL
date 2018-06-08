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
    /// Interaction logic for VUpdate.xaml
    /// </summary>
    public partial class VUpdate : Window
    {
        private const string CMB_OPCION_DEFECTO = "Elige tabla...";
        private const int NUM_CONDICIONES_MAX = 16;
        private Conexion conexionActual;
        private DbCommand comandoEnviar;
        private string textoComandoOriginal;
        private string datosCampos, datosCondiciones;

        public VUpdate() :
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

        public VUpdate(Conexion actual)
        {
            InitializeComponent();

            this.conexionActual = actual;
            this.Title += " || Base de datos \"" + actual.BaseDatos + "\"";
            // Obtiene el comando SQL correspondiente
            this.comandoEnviar = Operacion.ComandoUpdate(actual);
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
            stackCamposActualizar.Children.Clear();
            ModificarComando("", "", "");
        }

        private void ModificarComando(string tabla, string datosCampos, string datosCondiciones)
        {
            string comando = textoComandoOriginal;
            if (!String.IsNullOrWhiteSpace(datosCampos))
                this.datosCampos = datosCampos;
            if (!String.IsNullOrWhiteSpace(datosCondiciones))
                this.datosCondiciones = datosCondiciones;
            comando = comando.Replace(Operacion.PARAM, tabla);
            comando += datosCampos + " " + datosCondiciones;
            comandoEnviar.CommandText = comando;
            // Muestra el contenido del comando actual en el label
            lblComando.Content = comando;
        }

        private string parsearDatosCampoValor(List<ColumnaValor> datos)
        {
            string datosParseados = "";

            foreach (ColumnaValor filaDato in datos)
            {
                datosParseados += filaDato.Columna + " = ";
                datosParseados += filaDato.Valor + ", ";
            }
            // para eliminar la última coma y espacio
            if (datosParseados.Length > 0)
                return datosParseados.Substring(0, datosParseados.Length - 2);
            else
                return datosParseados;
        }

        private void cmbTabla_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //ReestablecerCampos();
            if (!Comun.ElegidaTablaDefecto(cmbTablas))
            {
                string nombreTabla = cmbTablas.SelectedItem?.ToString();
                ModificarComando(nombreTabla, "", "");
                Comun.GenerarCamposColumnas(stackCamposActualizar, conexionActual, nombreTabla,
                    chkCampos_SelectionChanged, txtCampos_TextChanged);
            }
            else
            {
                ReestablecerCampos();
            }
        }

        private void cmbTabla_DropDownOpened(object sender, EventArgs e)
        {
            Comun.RellenarComboTablas(conexionActual, (ComboBox)sender);
        }

        private void cmbNumCondiciones_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!Comun.ElegidaTablaDefecto(cmbTablas))
            {
                // Si hay tabla elegida, se muestan los campos correspondientes
                int numCondiciones = (int)cmbNumCondiciones.SelectedItem;
                string nombreTabla = cmbTablas.SelectedItem?.ToString();
                Comun.GenerarCamposCondicionesWhere(stackCondiciones, conexionActual, numCondiciones, nombreTabla,
                    cmbCondiciones_SelectionChanged, txtCondiciones_TextChanged);

                ModificarComando(cmbTablas.SelectedItem?.ToString(), datosCampos, "");
            }
        }

        private async void chkCampos_SelectionChanged(object sender, RoutedEventArgs e)
        {
            List<ColumnaValor> datosNuevos = await Comun.ExtraerDatosCamposColumnas(stackCamposActualizar);
            string datosParseados = parsearDatosCampoValor(datosNuevos);
            ModificarComando(cmbTablas.SelectedItem?.ToString(), datosParseados, datosCondiciones);
        }

        private async void txtCampos_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<ColumnaValor> datosNuevos = await Comun.ExtraerDatosCamposColumnas(stackCamposActualizar);
            string datosParseados = parsearDatosCampoValor(datosNuevos);
            ModificarComando(cmbTablas.SelectedItem?.ToString(), datosParseados, datosCondiciones);
        }

        private async void txtCondiciones_TextChanged(object sender, TextChangedEventArgs e)
        {
            string datosNuevos = await Comun.ExtraerDatosCondicionesWhere(stackCondiciones);
            ModificarComando(cmbTablas.SelectedItem?.ToString(), datosCampos, datosNuevos);
        }

        private async void cmbCondiciones_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string datosNuevos = await Comun.ExtraerDatosCondicionesWhere(stackCondiciones);
            ModificarComando(cmbTablas.SelectedItem?.ToString(), datosCampos, datosNuevos);
        }

        private void chkMarcarTodos_Click(object sender, RoutedEventArgs e)
        {
            bool marcado = chkMarcarTodos.IsChecked.Value;
            Comun.MarcarTodosCamposColumnas(stackCamposActualizar, marcado);
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            // Comprobación WHERE
            if (!comandoEnviar.CommandText.Contains("WHERE"))
            {
                MessageBoxResult opcionElegida = MessageBox.Show("No se han elegido condiciones. \r\n" +
                    "Se realizará un borrado TOTAL de TODAS las filas. ¿Continuar?",
                    "Peligro", MessageBoxButton.YesNo, MessageBoxImage.Stop);

                if (opcionElegida.Equals(MessageBoxResult.No))
                    return;
            }
            int resultado = Ayudante.ExecuteNonQuery(conexionActual, comandoEnviar);
            if (resultado > 0)
            {
                MessageBox.Show(
                    resultado + " filas de la tabla \"" +
                    Comprueba.EliminarResto(cmbTablas.SelectedItem.ToString()) +
                    "\" en base de datos " + "\"" + conexionActual.BaseDatos +
                    "\" eliminadas con éxito.");
            }
            else
            {
                MessageBox.Show("Ninguna fila afectada.");
            }
        }
    }
}
