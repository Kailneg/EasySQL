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
    /// Interaction logic for VSelect.xaml
    /// </summary>
    public partial class VSelect : Window
    {
        private const string CMB_OPCION_DEFECTO = "Elige tabla...";
        private const int NUM_CONDICIONES_MAX = 16;
        private Conexion conexionActual;
        private DbCommand comandoEnviar;
        private string textoComandoOriginal;
        private string datosCampos, datosWhere;
        private List<ColumnaValor> columnasElegidas, datosOrderBy;

        public VSelect() :
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

        public VSelect(Conexion actual)
        {
            InitializeComponent();

            this.conexionActual = actual;
            this.Title += " || Base de datos \"" + actual.BaseDatos + "\"";
            // Obtiene el comando SQL correspondiente
            this.comandoEnviar = Operacion.ComandoSelect(actual);
            lblComando.Content = comandoEnviar.CommandText;
            this.textoComandoOriginal = comandoEnviar.CommandText;
            columnasElegidas = new List<ColumnaValor>();

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
            stackWhere.Children.Clear();
            stackCamposActualizar.Children.Clear();
            stackOrderBy.Children.Clear();
            datosCampos = "";
            datosWhere = "";
            columnasElegidas = null;
            datosOrderBy = null;
            ModificarComando("", "", "", null);
        }

        private void ModificarComando(string tabla, string datosCampos, string datosWhere, List<ColumnaValor> datosOrderBy)
        {
            string comando = textoComandoOriginal;
            // Primero se asigna el valor al campo de clase para guardarlo en caso de que tenga datos
            if (!String.IsNullOrWhiteSpace(datosCampos))
                this.datosCampos = datosCampos;
            if (!String.IsNullOrWhiteSpace(datosWhere))
                this.datosWhere = datosWhere;
            if (datosOrderBy != null)
                this.datosOrderBy = datosOrderBy;

            if (this.datosOrderBy != null)
            {
                string orderByParseado = parsearDatosOrderBy(this.datosOrderBy);
                comando = comando.Replace(Operacion.PARAMS[3], orderByParseado);
            }
            else
            {
                // Cortar comando sobrante order by
                comando = comando.Substring(0, comando.IndexOf(Operacion.PARAMS[3]));
            }

            if (!String.IsNullOrWhiteSpace(this.datosWhere))
            {
                comando = comando.Replace(Operacion.PARAMS[2], this.datosWhere);
            }
            else
            {
                // Cortar comando sobrante WHERE
                comando = comando.Substring(0, comando.IndexOf(Operacion.PARAMS[2]));
            }

            if (!String.IsNullOrWhiteSpace(tabla))
            {
                comando = comando.Replace(Operacion.PARAMS[1], tabla);
            }
            else
            {
                // Cortar comando sobrante WHERE
                comando = comando.Substring(0, comando.IndexOf(Operacion.PARAMS[1]));
            }

            if (!String.IsNullOrWhiteSpace(datosCampos))
            {
                comando = comando.Replace(Operacion.PARAMS[0], datosCampos);
            }
            else
            {
                comando = comando.Replace(Operacion.PARAMS[0], "*");
            }

            comandoEnviar.CommandText = comando;
            // Muestra el contenido del comando actual en el label
            lblComando.Content = comando;
        }

        private string parsearDatosOrderBy(List<ColumnaValor> datos)
        {
            string orderByParseado = "ORDER BY ";

            foreach (ColumnaValor filaDato in datos)
            {
                orderByParseado += filaDato.Columna + " ";
                orderByParseado += filaDato.Valor + ", ";
            }
            // para eliminar la última coma y espacio
            if (orderByParseado.Length > 0)
                return orderByParseado.Substring(0, orderByParseado.Length - 2);
            else
                return orderByParseado;
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
                ModificarComando(nombreTabla, "", "", null);
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
                Comun.GenerarCamposWhere(stackWhere, conexionActual, numCondiciones, nombreTabla,
                    cmbWhere_SelectionChanged, txtWhere_TextChanged);

                ModificarComando(cmbTablas.SelectedItem?.ToString(), datosCampos, "", null);
            }
        }

        private async void chkCampos_SelectionChanged(object sender, RoutedEventArgs e)
        {
            columnasElegidas = await Comun.ExtraerDatosCamposColumnas(stackCamposActualizar);
            string datosParseados = parsearDatosCampoValor(columnasElegidas);
            ModificarComando(cmbTablas.SelectedItem?.ToString(), datosParseados, datosWhere, null);
        }

        private async void txtCampos_TextChanged(object sender, TextChangedEventArgs e)
        {
            columnasElegidas = await Comun.ExtraerDatosCamposColumnas(stackCamposActualizar);
            string datosParseados = parsearDatosCampoValor(columnasElegidas);
            ModificarComando(cmbTablas.SelectedItem?.ToString(), datosParseados, datosWhere, null);
        }

        private async void txtWhere_TextChanged(object sender, TextChangedEventArgs e)
        {
            string condicionesNuevas = await Comun.ExtraerDatosWhere(stackWhere);
            ModificarComando(cmbTablas.SelectedItem?.ToString(), datosCampos, condicionesNuevas, null);
        }

        private async void cmbWhere_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string condicionesNuevas = await Comun.ExtraerDatosWhere(stackWhere);
            ModificarComando(cmbTablas.SelectedItem?.ToString(), datosCampos, condicionesNuevas, null);
        }

        private async void cmbOrderBySelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<ColumnaValor> nuevosOrderBy = await Comun.ExtraerDatosOrderBy(stackOrderBy);

            ModificarComando(cmbTablas.SelectedItem?.ToString(), datosCampos, datosWhere, nuevosOrderBy);
        }

        private void chkMarcarTodos_Click(object sender, RoutedEventArgs e)
        {
            bool marcado = chkMarcarTodos.IsChecked.Value;
            Comun.MarcarTodosCamposColumnas(stackCamposActualizar, marcado);
        }

        private void cmbNumOrderBy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!Comun.ElegidaTablaDefecto(cmbTablas))
            {
                // Si hay tabla elegida, se muestan los campos correspondientes
                int numCondiciones = (int)(cmbNumOrderBy.SelectedItem?? 0);
                string nombreTabla = cmbTablas.SelectedItem?.ToString();
                Comun.GenerarCamposOrderBy(stackOrderBy, conexionActual,
                    numCondiciones, columnasElegidas, nombreTabla, cmbOrderBySelectionChanged);

                ModificarComando(cmbTablas.SelectedItem?.ToString(), datosCampos, "", null);
            }
        }

        private void cmbNumOrderBy_DropDownOpened(object sender, EventArgs e)
        {
            // Calcular número condiciones ORDER BY disponibles
            cmbNumOrderBy.Items.Clear();

            for (int i = 0; i < columnasElegidas.Count + 1; i++)
            {
                cmbNumOrderBy.Items.Add(i);
            }
            cmbNumOrderBy.SelectedIndex = 0;
            datosOrderBy = null;
            ModificarComando(cmbTablas.SelectedItem?.ToString(), datosCampos, "", null);
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
