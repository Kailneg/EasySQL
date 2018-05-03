using EasySQL.Modelos;
using EasySQL.Utils;
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
    /// Interaction logic for VentanaInicio.xaml
    /// </summary>
    public partial class VentanaInicio : Window
    {
        public VentanaInicio()
        {
            InitializeComponent();  
        }

        public VentanaInicio(Usuario usuario) : this()
        {
            if (usuario != null)
            {
                RefrescarTexboxes(usuario);
            }
        }

        private void RefrescarTexboxes(Usuario usuario)
        {
            txtBoxUsuario.Text = usuario.Nombre;
            txtBoxContrasenia.Text = usuario.Contrasenia;
        }

        private void btnAcceder_Click(object sender, RoutedEventArgs e)
        {
            Utils.Consola.NoImplementado();
            Usuario enviar = new Usuario(txtBoxUsuario.Text, txtBoxContrasenia.Text);
            VentanaConexion vc = new VentanaConexion(enviar);
            this.Close();
            vc.Show();
        }

        private void btnInvitado_Click(object sender, RoutedEventArgs e)
        {
            VentanaConexion vc = new VentanaConexion(null);
            this.Close();
            vc.Show();
        }

        private void btnRegistro_Click(object sender, RoutedEventArgs e)
        {
            VentanaRegistro vr = new VentanaRegistro();
            vr.ShowDialog();
        }

        private void txtBoxUsuario_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox datos = (TextBox)sender;
            Colorea.BordeCorrectoErrorDefecto(datos, Comprueba.Usuario(datos.Text));
        }

        private void txtBoxContrasenia_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox datos = (TextBox)sender;
            Colorea.BordeCorrectoErrorDefecto(datos, Comprueba.Contrasenia(datos.Text));
        }


    }
}
