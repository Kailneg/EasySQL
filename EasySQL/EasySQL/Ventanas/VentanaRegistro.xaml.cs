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
    /// Interaction logic for VentanaRegistro.xaml
    /// </summary>
    public partial class VentanaRegistro : Window
    {
        public VentanaRegistro()
        {
            InitializeComponent();
        }

        private void Acceder() {
            MessageBox.Show("Accediendo...");
        }

        private bool Guardar() {
            if (ComprobarCampos())
            {
                MessageBox.Show("Guardando campos");
                return true;
            }
            else
            {
                MessageBox.Show("Uno o más campos contienen errores");
                return false;
            }
        }


        private void btnAcceder_Click(object sender, RoutedEventArgs e)
        {
            if (Guardar())
            {
                Acceder();
            }
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            Guardar();
        }

        private void btnAtras_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void txtBoxUsuario_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox datos = (TextBox)sender;
            Colorea.BordeCorrectoError(datos, Comprueba.Usuario(datos.Text));
        }

        private void txtBoxContrasenia_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox datos = (TextBox)sender;
            Colorea.BordeCorrectoError(datos, Comprueba.Usuario(datos.Text));
        }

        private void txtBoxRepetirContrasenia_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox datos = (TextBox)sender;
            Colorea.BordeCorrectoError(datos, this.ComprobarContrasenias());
        }
        
        private bool ComprobarCampos()
        {
            return ((Comprueba.Usuario(txtBoxUsuario.Text) ?? false)
                && (Comprueba.Contrasenia(txtBoxContrasenia.Text) ?? false)
                && ComprobarContrasenias());
        }


        private bool ComprobarContrasenias()
        {
            return (Comprueba.Contrasenia(txtBoxContrasenia.Text) ?? false)
                && txtBoxRepetirContrasenia.Text.Equals(txtBoxContrasenia.Text);
        }
    }
}
