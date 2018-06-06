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
            if (!Comun.ElegidaTablaDefecto(cmbTablas))
                ModificarComando(cmbTablas.SelectedItem.ToString(), "");
            else
                ModificarComando("", "");
        }

        private void ModificarComando(string tabla, string datos)
        {
            string comando = textoComandoOriginal;
            comando = comando.Replace(Operacion.PARAM, tabla);
            // Se trata de un DELETE * FROM
            if (String.IsNullOrWhiteSpace(datos))
            {
                comando = comando.Replace("WHERE", "");
            }
            else
            {
                comando += datos;
            }
            
            comandoEnviar.CommandText = comando;
            // Muestra el contenido del comando actual en el label
            lblComando.Content = comando;
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

        private void cmbTabla_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ReestablecerCampos();
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
                    cmbGenerado_SelectionChanged, txtGenerado_TextChanged);

                ModificarComando(cmbTablas.SelectedItem?.ToString(), "");
            } 
        }

        private async void cmbGenerado_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string datos = await Comun.ExtraerDatosCondicionesWhere(stackCondiciones);
            ModificarComando(cmbTablas.SelectedItem?.ToString(), datos);
        }

        private async void txtGenerado_TextChanged(object sender, TextChangedEventArgs e)
        {
            string datos = await Comun.ExtraerDatosCondicionesWhere(stackCondiciones);
            ModificarComando(cmbTablas.SelectedItem?.ToString(), datos);
        }

    }
}
