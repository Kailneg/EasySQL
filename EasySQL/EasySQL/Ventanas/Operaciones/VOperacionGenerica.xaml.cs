using EasySQL.Modelos;
using EasySQL.Operaciones.Ayudante;
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
    public partial class VOperacionGenerica : Window
    {
        private Conexion actual;
        private DbCommand comando;

        public VOperacionGenerica(String labelDescripcion, Conexion actual, DbCommand comando)
        {
            InitializeComponent();
            lbl.Content = labelDescripcion;
            lblComando.Content = comando.CommandText;
            this.actual = actual;
            this.comando = comando;
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            if (txtbox.Text.Length > 0)
            {
                if (comando.Parameters.Count > 0)
                {
                    comando.Parameters["@param"].Value = txtbox.Text;
                }
                else
                {
                    comando.CommandText += txtbox.Text;
                }
                Ayudante.ExecuteNonQuery(actual, comando);
            } else
            {
                MessageBox.Show("Debes introducir un nombre");
            }
        }

        private void txtbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            lblComando.Content = comando.CommandText + txtbox.Text;
        }
    }
}
