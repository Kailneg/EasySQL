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
    /// Lógica de interacción para VOperacionGenerica.xaml
    /// </summary>
    public partial class VCreateDatabase : Window
    {
        private Conexion conexionActual;
        private DbCommand comandoEnviar;
        private string textoComandoOriginal;

        public VCreateDatabase(Conexion actual)
        {
            InitializeComponent();
            this.conexionActual = actual;

            // Obtiene el comando SQL correspondiente
            this.comandoEnviar = Operacion.ComandoCreateDatabase(actual);
            lblComando.Content = comandoEnviar.CommandText;
            this.textoComandoOriginal = comandoEnviar.CommandText;
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            if (txtbox.Text.Length > 0)
            {
                if (Comprueba.ContieneSeparadorSQL(txtbox.Text))
                {
                    MessageBox.Show("Detectado separador SQL. Sólo se ejecutará: " + comandoEnviar.CommandText);
                }
                comandoEnviar.CommandText = textoComandoOriginal + Comprueba.EliminarResto(txtbox.Text);

                int resultado = Ayudante.ExecuteNonQuery(conexionActual, comandoEnviar);
                if (resultado == -1)
                {
                    MessageBox.Show("Base de datos \"" + Comprueba.EliminarResto(txtbox.Text) + "\" creada con éxito.");
                }
            } else
            {
                MessageBox.Show("Debes introducir un nombre");
            }
        }

        private void txtbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            lblComando.Content = textoComandoOriginal + Comprueba.EliminarResto(txtbox.Text);
        }
    }
}
