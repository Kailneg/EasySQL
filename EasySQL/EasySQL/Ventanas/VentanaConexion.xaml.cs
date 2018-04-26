using EasySQL.BBDD;
using EasySQL.Modelos;
using System;
using System.Collections.Generic;
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

namespace EasySQL.Ventanas
{
    /// <summary>
    /// Lógica de interacción para VentanaConexion.xaml
    /// </summary>
    public partial class VentanaConexion : Window
    {
        private Usuario usuarioActivo;
        private BBDDPrograma datosPrograma;

        public VentanaConexion(Usuario usuario)
        {
            InitializeComponent();
            if (usuario != null)
            {
                usuarioActivo = usuario;
                //datosPrograma.ObtenerConexiones(usuarioActivo);
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            VentanaInicio vi = new VentanaInicio(usuarioActivo);
            this.Close();
            vi.Show();
        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            txtBoxNombre.Text = "";
            txtBoxDireccion.Text = "";
            txtBoxPuerto.Text = "";
            txtBoxUsuario.Text = "";
            txtBoxContrasenia.Text = "";
            chkGuardarContrasenia.IsChecked = false;
            rbtnMicrosoft.IsChecked = false;
            rbtnMySQL.IsChecked = false;
            rbtnPostgreSQL.IsChecked = false;
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            string nombreConex = txtBoxNombre.Text;
            string nombreDirecc = txtBoxDireccion.Text;
            //string no
        }

        /// <summary>
        /// Se comprueban todos los campos obligatorios y que estos tengan datos correctos.
        /// </summary>
        private void ComprobarDatos() {

            //Campos obligatorios: nombre conexión, dirección, usuario, tipo conexión.

        }
    }
}
