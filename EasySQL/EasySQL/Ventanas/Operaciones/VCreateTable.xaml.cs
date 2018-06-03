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
    /// Interaction logic for VCreateTable.xaml
    /// </summary>
    public partial class VCreateTable : Window
    {
        private Conexion conexionActual;
        private DbCommand comandoEnviar;
        private string textoComandoOriginal;

        public VCreateTable() :
            this(
                    new Conexion()
                    {
                        Direccion = "localhost\\SQLALE",
                        TipoActual = Conexion.TipoConexion.MicrosoftSQL,
                        UsuarioConexion = Usuario.NombreIntegratedSecurity,
                        BaseDatos = "Pruebas"
                    }
                )
        { }

        public VCreateTable(Conexion actual)
        {
            InitializeComponent();
            this.conexionActual = actual;
            this.Title += " || Base de datos \"" + actual.BaseDatos + "\"";
            // Obtiene el comando SQL correspondiente
            this.comandoEnviar = Operacion.ComandoCreateTable(actual);
            lblComando.Content = comandoEnviar.CommandText;
            this.textoComandoOriginal = comandoEnviar.CommandText;

            //Rellenar ComboBox campos
            for (int i = 1; i <= 10; i++)
            {
                cmbCampos.Items.Add(i);
            }
        }

        private void txtbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            lblComando.Content = textoComandoOriginal + Comprueba.EliminarResto(txtbox.Text);
        }

        private void cmbCampos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Por cada uno de los campos a añadir, añadir tantos textboxes como sean necesarios.

            if (cmbCampos.SelectedItem != null && cmbCampos.IsMouseCaptured)
            {
                int numCampos = Int32.Parse(cmbCampos.SelectedItem.ToString());

                stackTextBoxes.Children.Clear();
                for (int i = 0; i < numCampos; i++)
                {
                    TextBox campo = new TextBox();
                    campo.Height = 25;
                    campo.Margin = new Thickness(0, 5, 0, 5);
                    stackTextBoxes.Children.Add(campo);
                }

                stackComboBoxes.Children.Clear();
                for (int i = 0; i < numCampos; i++)
                {
                    ComboBox combo = new ComboBox();
                    combo.Height = 25;
                    combo.Margin = new Thickness(0, 5, 0, 5);
                    foreach (var tipoDato in Operacion.TiposDatos(conexionActual))
                    {
                        combo.Items.Add(tipoDato);
                    }
                    stackComboBoxes.Children.Add(combo);
                }
            }
            
        }

        private void btnEjecutar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
