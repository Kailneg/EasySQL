using EasySQL.BBDD;
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
        public VentanaInicio vi;

        public VentanaRegistro(VentanaInicio vi)
        {
            InitializeComponent();
            this.vi = vi;
        }

        private void Acceder() {
            Utils.Consola.NoImplementado();
            MessageBox.Show("Accediendo...");
            this.Close();
            VentanaConexion vc = new VentanaConexion(new Modelos.Usuario(txtBoxUsuario.Text, txtBoxContrasenia.Text));
            vi.Close();
            vc.Show();
        }

        private bool Guardar() {
            Utils.Consola.NoImplementado();
            if (ComprobarCampos())
            {
                MessageBox.Show("Guardando campos");
                bool resultado =
                BBDDPrograma.ObtenerInstancia().RegistrarUsuario(txtBoxUsuario.Text, txtBoxContrasenia.Text);
                if (resultado)
                {
                    MessageBox.Show("Usuario almacenado correctamente.");
                    return true;
                } else
                {
                    MessageBox.Show("La operación ha fallado.");
                    return false;
                }
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
