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
    /// Interaction logic for VInsert.xaml
    /// </summary>
    public partial class VInsert : Window
    {
        private const string CMB_OPCION_DEFECTO = "Elige tabla...";
        private const int NUM_CONDICIONES_MAX = 16;
        private Conexion conexionActual;
        private DbCommand comandoEnviar;
        private string textoComandoOriginal;
        private string columnas, datosColumnas;

        public VInsert() :
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

        public VInsert(Conexion actual)
        {
            InitializeComponent();

            this.conexionActual = actual;
            this.Title += " || Base de datos \"" + actual.BaseDatos + "\"";
            // Obtiene el comando SQL correspondiente
            this.comandoEnviar = Operacion.ComandoInsert(actual);
            lblComando.Content = comandoEnviar.CommandText;
            this.textoComandoOriginal = comandoEnviar.CommandText;

            // Agrega y muestra la opción por defecto en el combobox Tablas.
            cmbTablas.Items.Add(CMB_OPCION_DEFECTO);
            cmbTablas.SelectedIndex = 0;
        }

        private void ReestablecerCampos()
        {
            stackCamposActualizar.Children.Clear();
            ModificarComando("", "", "");
        }

        private void ModificarComando(string tabla, string columnas, string datosColumnas)
        {
            string comando = textoComandoOriginal;
            if (!string.IsNullOrWhiteSpace(this.columnas))
                this.columnas = columnas;
            if (!String.IsNullOrWhiteSpace(datosColumnas))
                this.datosColumnas = columnas;
            comando = comando.Replace(Operacion.PARAMS[0], tabla);
            comando = comando.Replace(Operacion.PARAMS[1], columnas);
            comando = comando.Replace(Operacion.PARAMS[2], datosColumnas);
            comandoEnviar.CommandText = comando;
            // Muestra el contenido del comando actual en el label
            lblComando.Content = comando;
        }

        private string parsearDatosColumnaValor(List<ColumnaValor> datos, bool parsearColumnas)
        {
            string datosParseados = "";

            foreach (ColumnaValor filaDato in datos)
            {
                if (parsearColumnas)
                    datosParseados += filaDato.Columna + ", ";
                else
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

        private async void chkCampos_SelectionChanged(object sender, RoutedEventArgs e)
        {
            List<ColumnaValor> datosNuevos = await Comun.ExtraerDatosCamposColumnas(stackCamposActualizar);
            string columnas = parsearDatosColumnaValor(datosNuevos, true);
            string datosColumnas = parsearDatosColumnaValor(datosNuevos, false);
            ModificarComando(cmbTablas.SelectedItem?.ToString(), columnas, datosColumnas);
        }

        private async void txtCampos_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<ColumnaValor> datosNuevos = await Comun.ExtraerDatosCamposColumnas(stackCamposActualizar);
            string columnas = parsearDatosColumnaValor(datosNuevos, true);
            string datosColumnas = parsearDatosColumnaValor(datosNuevos, false);
            ModificarComando(cmbTablas.SelectedItem?.ToString(), columnas, datosColumnas);
        }

        private void chkMarcarTodos_Click(object sender, RoutedEventArgs e)
        {
            bool marcado = chkMarcarTodos.IsChecked.Value;
            Comun.MarcarTodosCamposColumnas(stackCamposActualizar, marcado);
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
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
