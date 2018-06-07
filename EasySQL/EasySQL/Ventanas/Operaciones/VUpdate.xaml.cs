using EasySQL.Modelos;
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
        private const int NUM_CONDICIONES_MAX = 10;
        private Conexion conexionActual;
        private DbCommand comandoEnviar;
        private string textoComandoOriginal;

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
            this.comandoEnviar = Operacion.ComandoDeleteFrom(actual);
            //lblComando.Content = comandoEnviar.CommandText;
            this.textoComandoOriginal = comandoEnviar.CommandText;

            // Agrega y muestra la opción por defecto en el combobox Tablas.
            cmbTablas.Items.Add(CMB_OPCION_DEFECTO);
            cmbTablas.SelectedIndex = 0;
        }

        private void cmbTabla_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            //ReestablecerCampos();
            if (!Comun.ElegidaTablaDefecto(cmbTablas))
            {
                string nombreTabla = cmbTablas.SelectedItem?.ToString();
                Comun.GenerarCamposColumnas(stackCamposActualizar, conexionActual, nombreTabla,
                    chkGenerado_SelectionChanged, txtGenerado_TextChanged);
            }
        }

        private void cmbTabla_DropDownOpened(object sender, EventArgs e)
        {
            Comun.RellenarComboTablas(conexionActual, (ComboBox)sender);
        }

        //private void ReestablecerCampos()
        //{
        //    // Si no, resetea el label
        //    cmbNumCondiciones.SelectedIndex = 0;
        //    stackCondiciones.Children.Clear();
        //    // Comprobar que la tabla no sea la por defecto
        //    if (!Comun.ElegidaTablaDefecto(cmbTablas))
        //        ModificarComando(cmbTablas.SelectedItem.ToString(), "");
        //    else
        //        ModificarComando("", "");
        //}

        private async void chkGenerado_SelectionChanged(object sender, RoutedEventArgs e)
        {
            //string datos = await Comun.ExtraerDatosCondicionesWhere(stackCondiciones);
            //ModificarComando(cmbTablas.SelectedItem?.ToString(), datos);
            Console.WriteLine("Checkbox: " + ((CheckBox)sender).IsChecked.Value);
        }

        private async void txtGenerado_TextChanged(object sender, TextChangedEventArgs e)
        {
            //string datos = await Comun.ExtraerDatosCondicionesWhere(stackCondiciones);
            //ModificarComando(cmbTablas.SelectedItem?.ToString(), datos);
            Console.WriteLine("Textbox: " + ((TextBox)sender).Text);
        }
    }
}
