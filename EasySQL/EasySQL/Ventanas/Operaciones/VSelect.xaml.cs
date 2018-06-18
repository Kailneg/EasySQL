using EasySQL.Modelos;
using EasySQL.Operaciones.Operacion;
using EasySQL.Operaciones.Comandos;
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
    /// Interaction logic for VSelect.xaml
    /// </summary>
    public partial class VSelect : Window
    {
        private const string CMB_OPCION_DEFECTO = "Elige tabla...";
        private const int NUM_CONDICIONES_MAX = 16;
        private Conexion conexionActual;
        private DbCommand comandoEnviar;
        private string textoComandoOriginal;
        private string datosSelect, datosWhere;
        private List<ColumnaValor> extraerSelect, datosOrderBy;

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
            this.comandoEnviar = Comando.Select(actual);
            lblComando.Content = comandoEnviar.CommandText;
            this.textoComandoOriginal = comandoEnviar.CommandText;
            extraerSelect = new List<ColumnaValor>();

            // Agrega y muestra la opción por defecto en el combobox Tablas.
            cmbTablas.Items.Add(CMB_OPCION_DEFECTO);
            cmbTablas.SelectedIndex = 0;

            // Agrega valores y muestra la opción por defecto en el combobox numCondiciones.
            for (int i = 0; i < NUM_CONDICIONES_MAX; i++)
            {
                cmbNumWhere.Items.Add(i);
            }
            cmbNumWhere.SelectedIndex = 0;
        }

        private void ReestablecerCampos()
        {
            // Si no, resetea el label
            cmbNumWhere.SelectedIndex = 0;
            stackWhere.Children.Clear();
            stackCamposActualizar.Children.Clear();
            stackOrderBy.Children.Clear();
            datosSelect = "";
            datosWhere = "";
            extraerSelect = new List<ColumnaValor>();
            datosOrderBy = null;
            ModificarComando("", "", "", null);
        }

        private void ModificarComando(string tabla, string datosSelect, string datosWhere, List<ColumnaValor> datosOrderBy)
        {
            string comando = textoComandoOriginal;
            // Primero se asigna el valor al campo de clase para guardarlo en caso de que tenga datos
            if (!String.IsNullOrWhiteSpace(datosSelect))
                this.datosSelect = datosSelect;
            if (!String.IsNullOrWhiteSpace(datosWhere))
                this.datosWhere = datosWhere;
            if (datosOrderBy != null)
                this.datosOrderBy = datosOrderBy;

            if (!String.IsNullOrWhiteSpace(datosSelect))
            {
                comando = comando.Replace(Comando.PARAMS[0], datosSelect);
            }
            else
            {
                comando = comando.Replace(Comando.PARAMS[0], "*");
            }

            if (!String.IsNullOrWhiteSpace(tabla))
            {
                comando = comando.Replace(Comando.PARAMS[1], tabla);
            }
            else
            {
                comando = comando.Replace(Comando.PARAMS[1], "");
            }

            if (!String.IsNullOrWhiteSpace(this.datosWhere))
            {
                comando = comando.Replace(Comando.PARAMS[2], this.datosWhere);
            }
            else
            {
                comando = comando.Replace(Comando.PARAMS[2], "");
            }

            if (this.datosOrderBy?.Count > 0)
            {
                string orderByParseado = parsearDatosOrderBy(this.datosOrderBy);
                comando = comando.Replace(Comando.PARAMS[3], orderByParseado);
            }
            else
            {
                comando = comando.Replace(Comando.PARAMS[3], "");
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

        private string parsearDatosSelect(List<ColumnaValor> datos)
        {
            string datosParseados = "";

            foreach (ColumnaValor filaDato in datos)
            {
                datosParseados += filaDato.Columna + ", ";
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
                Comun.GenerarCamposSelect(stackCamposActualizar, conexionActual, nombreTabla,
                    chkSelect_SelectionChanged);
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
                int numCondiciones = (int)cmbNumWhere.SelectedItem;
                string nombreTabla = cmbTablas.SelectedItem?.ToString();
                this.datosWhere = "";
                Comun.GenerarCamposWhere(stackWhere, conexionActual, numCondiciones, nombreTabla,
                    cmbWhere_SelectionChanged, txtWhere_TextChanged);

                ModificarComando(cmbTablas.SelectedItem?.ToString(), datosSelect, "", null);
            }
        }

        private async void chkSelect_SelectionChanged(object sender, RoutedEventArgs e)
        {
            extraerSelect = await Comun.ExtraerDatosCamposSelect(stackCamposActualizar);
            string datosSelect = parsearDatosSelect(extraerSelect);
            ModificarComando(cmbTablas.SelectedItem?.ToString(), datosSelect, datosWhere, null);
        }

        private async void txtWhere_TextChanged(object sender, TextChangedEventArgs e)
        {
            string condicionesNuevas = await Comun.ExtraerDatosWhere(stackWhere);
            ModificarComando(cmbTablas.SelectedItem?.ToString(), datosSelect, condicionesNuevas, null);
        }

        private async void cmbWhere_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string condicionesNuevas = await Comun.ExtraerDatosWhere(stackWhere);
            ModificarComando(cmbTablas.SelectedItem?.ToString(), datosSelect, condicionesNuevas, null);
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
                this.datosOrderBy = new List<ColumnaValor>();
                Comun.GenerarCamposOrderBy(stackOrderBy, conexionActual,
                    numCondiciones, extraerSelect, nombreTabla, cmbGeneradoOrderBySelectionChanged);
                ModificarComando(cmbTablas.SelectedItem?.ToString(), datosSelect, "", null);
            }
        }

        private void cmbNumOrderBy_DropDownOpened(object sender, EventArgs e)
        {
            // Calcular número condiciones ORDER BY disponibles
            cmbNumOrderBy.Items.Clear();

            if (!Comun.ElegidaTablaDefecto(cmbTablas))
            {
                for (int i = 0; i < extraerSelect.Count + 1; i++)
                {
                    cmbNumOrderBy.Items.Add(i);
                }
                cmbNumOrderBy.SelectedIndex = 0;
                //datosOrderBy = null;
                //ModificarComando(cmbTablas.SelectedItem?.ToString(), datosSelect, "", datosOrderBy);
            }
        }

        private async void cmbGeneradoOrderBySelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<ColumnaValor> nuevosOrderBy = await Comun.ExtraerDatosOrderBy(stackOrderBy);
            ModificarComando(cmbTablas.SelectedItem?.ToString(), datosSelect, datosWhere, nuevosOrderBy);
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            object comprobarComando = Operacion.ExecuteScalar(conexionActual, comandoEnviar);
            if (comprobarComando != null)
            {
                int resultado = 0;
                Int32.TryParse(comprobarComando.ToString(), out resultado);
                if (resultado != Operacion.ERROR)
                {
                    // Al menos hay una fila que mostrar
                    IDataReader readerSelect = Operacion.ExecuteReader(conexionActual, comandoEnviar);
                    DataTable datosMostrar = new DataTable();
                    datosMostrar.Load(readerSelect);
                    DatosConsulta paqueteDatos = new DatosConsulta(conexionActual, datosMostrar, comandoEnviar.CommandText);
                    VMostrarDatos vmd = new VMostrarDatos(paqueteDatos);
                    vmd.Show();
                }
            }
            else
            {
                Msj.Aviso("Ninguna fila encontrada.");
            }
        }
    }
}
