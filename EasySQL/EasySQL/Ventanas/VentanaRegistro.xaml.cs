using EasySQL.BBDD;
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
using static EasySQL.BBDD.ResultadoRegistro;

namespace EasySQL.Ventanas
{
    /// <summary>
    /// Interaction logic for VentanaRegistro.xaml
    /// </summary>
    public partial class VentanaRegistro : Window
    {
        public VentanaInicio vi;
        /// <summary>
        /// Utilizado para la función Guardar y Acceder
        /// </summary>
        private Usuario usuarioGuardado;

        public VentanaRegistro(VentanaInicio vi)
        {
            InitializeComponent();
            this.vi = vi;
        }

        private void Acceder() {
            Utils.Consola.NoImplementado();
            MessageBox.Show("Accediendo...");
            VentanaConexion vc = new VentanaConexion(usuarioGuardado);
            Manejador.CambiarVentana(this, vc);
        }

        private bool Guardar() {
            Utils.Consola.NoImplementado();
            if (ComprobarCampos())
            {
                ResultadoRegistro resultado =
                    BBDDPrograma.RegistrarUsuario(txtBoxUsuario.Text, txtBoxContrasenia.Text);
                MostrarMensaje(resultado.ResultadoActual);
                
                // Si se guarda, lo almacenamos temporalmente por si se desea acceder directamente
                if (resultado.ResultadoActual == TipoResultado.ACEPTADO)
                {
                    usuarioGuardado = resultado.UsuarioActual;
                }
                return (resultado.ResultadoActual == TipoResultado.ACEPTADO);
            }
            else
            {
                MessageBox.Show("Uno o más campos contienen errores");
                return false;
            }
        }


        private void btnGuardarAcceder_Click(object sender, RoutedEventArgs e)
        {
            // Primero se comprueba si hay un usuario ya guardado.
            // Si el guardado es igual a los datos de los input, se accede directamente sin guardar
            if (UsuarioGuardadoCorrespondeConInputs())
            {
                Acceder();
            }
            else
            {
                if (Guardar())
                {
                    Acceder();
                }
            }
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            Guardar();
        }

        private void btnAtras_Click(object sender, RoutedEventArgs e)
        {
            VentanaInicio vi = new VentanaInicio(usuarioGuardado);
            Manejador.CambiarVentana(this, vi);
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

        private bool UsuarioGuardadoCorrespondeConInputs()
        {
            if (this.usuarioGuardado != null)
            {
                if (this.usuarioGuardado.Nombre.Equals(txtBoxUsuario.Text) &&
                    this.usuarioGuardado.Contrasenia.Equals(txtBoxContrasenia.Text))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
