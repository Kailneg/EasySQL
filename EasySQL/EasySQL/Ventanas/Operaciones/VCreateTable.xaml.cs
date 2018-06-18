using EasySQL.Modelos;
using EasySQL.Operaciones.Operacion;
using EasySQL.Operaciones.Comandos;
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
    /// Interaction logic for VCreateTable.xaml
    /// </summary>
    public partial class VCreateTable : Window
    {
        private Conexion conexionActual;
        private DbCommand comandoEnviar;
        private string textoComandoOriginal;
        private const int NUM_COL_MAX = 25;
        int numColumnas;

        public VCreateTable(Conexion actual)
        {
            InitializeComponent();
            this.conexionActual = actual;
            this.Title += " || Base de datos \"" + actual.BaseDatos + "\"";
            // Obtiene el comando SQL correspondiente
            this.comandoEnviar = Comando.CreateTable(actual);
            lblComando.Content = comandoEnviar.CommandText;
            this.textoComandoOriginal = comandoEnviar.CommandText;

            //Rellenar ComboBox campos
            for (int i = 1; i <= NUM_COL_MAX; i++)
            {
                cmbCampos.Items.Add(i);
            }
        }

        private async void CambioDetectado()
        {
            // Espera 5ms para que de tiempo a repintar los componentes
            await Task.Delay(5);
            // Mirar cada uno de los campos del formulario para escribir el comando SQL resultante.
            // Comando original: CREATE TABLE 
            string datos = textoComandoOriginal;
            List<string> primaryKeys = new List<string>();

            // Añado nombre tabla -> CREATE TABLE nombre (
            datos += Comprueba.EliminarResto(txtTabla.Text);
            datos += " (";

            // Añadir cada uno de los valores de los textbox, separado por coma junto al tipo de dato
            // -> CREATE TABLE nombre (columna1 tipodato1, columna2 tipodato2

            for (int i = 0; i < numColumnas; i++)
            {
                // Obtiene cada TextBox y ComboBox de cada fila
                CheckBox chkPk = stackCheckBoxes.Children[i] as CheckBox;
                TextBox txtColumna = stackTextBoxes.Children[i] as TextBox;
                ComboBox cmbTipoDato = stackComboBoxes.Children[i] as ComboBox;

                if (chkPk.IsChecked.Value)
                    primaryKeys.Add(txtColumna.Text);

                datos += Comprueba.EliminarResto(txtColumna.Text);
                datos += " ";
                if (cmbTipoDato.SelectedItem != null)
                    datos += Comprueba.EliminarResto(cmbTipoDato.SelectedItem.ToString());
                datos += ", ";
            }


            // -> CREATE TABLE nombre (columna1 tipodato1, columna2 tipodato2)
            // += CONSTRAINT PK_<nombreTabla> PRIMARY KEY (columna2marcado1, columna2marcada2)
            if (primaryKeys.Count > 0)
            {
                datos += " CONSTRAINT PK_" + txtTabla.Text + " PRIMARY KEY (";
                foreach (string pk in primaryKeys)
                {
                    datos += pk + ", ";
                }
                datos += ")";
            }

            // Elimina última coma y pone paréntesis de cierre
            if (datos.Contains(','))
                datos = datos.Remove(datos.LastIndexOf(','), 1);
            datos += ")";

            // Asigna datos a label y al comando
            string comandoResultante = Comprueba.EliminarResto(datos);
            lblComando.Content = comandoResultante;
            comandoEnviar.CommandText = comandoResultante;
        }

        private void cmbCampos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Por cada uno de los campos a añadir, añadir tantos textboxes como sean necesarios.

            if (cmbCampos.SelectedItem != null && cmbCampos.IsMouseCaptured)
            {
                numColumnas = Int32.Parse(cmbCampos.SelectedItem.ToString());
                string[] tiposDatos = Comando.TiposDatos(conexionActual);

                stackCheckBoxes.Children.Clear();
                for (int i = 0; i < numColumnas; i++)
                {
                    CheckBox check = new CheckBox();
                    check.Height = 25;
                    check.Margin = new Thickness(0, 5, 0, 5);
                    check.VerticalContentAlignment = VerticalAlignment.Center;
                    check.HorizontalAlignment = HorizontalAlignment.Center;
                    stackCheckBoxes.Children.Add(check);
                }

                stackTextBoxes.Children.Clear();
                for (int i = 0; i < numColumnas; i++)
                {
                    TextBox campo = new TextBox();
                    campo.Height = 25;
                    campo.Margin = new Thickness(0, 5, 0, 5);
                    stackTextBoxes.Children.Add(campo);
                }

                stackComboBoxes.Children.Clear();
                for (int i = 0; i < numColumnas; i++)
                {
                    ComboBox combo = new ComboBox();
                    combo.Height = 25;
                    combo.Margin = new Thickness(0, 5, 0, 5);
                    foreach (var tipoDato in tiposDatos)
                    {
                        combo.Items.Add(tipoDato);
                    }
                    stackComboBoxes.Children.Add(combo);
                }
            }
            
        }

        private void btnEjecutar_Click(object sender, RoutedEventArgs e)
        {
            int resultado = Operacion.ExecuteNonQuery(conexionActual, comandoEnviar);
            if (resultado != Operacion.ERROR)
            {
                Msj.Info("Tabla \"" + Comprueba.EliminarResto(txtTabla.Text) + "\" en base de datos " +
                    "\"" + conexionActual.BaseDatos +  "\" creada con éxito.");
            }
        }
        
        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            CambioDetectado();
        }

        private void Window_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            CambioDetectado();
        }
    }
}
