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
        /// <summary>
        /// Utilizado para la función Guardar y Acceder
        /// </summary>
        private Usuario usuarioGuardado;

        /// <summary>
        /// Crea una nueva ventana de registro e inicia sus componentes.
        /// </summary>
        public VentanaRegistro()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Cambia a la ventana Conexión y le pasa el usuario actual.
        /// </summary>
        private void Acceder() {
            MessageBox.Show("Accediendo...");
            VentanaConexion vc = new VentanaConexion(usuarioGuardado);
            Manejador.CambiarVentana(this, vc);
        }

        /// <summary>
        /// Comprueba si los datos de los inputs son correctos,
        /// luego guarda el usuario actual en base de datos.
        /// </summary>
        /// <returns>True si se ha podido almacenar el usuario en la base de datos.</returns>
        private bool Guardar() {
            if (ComprobarCampos())
            {
                ResultadoRegistro resultado =
                    BBDDPrograma.RegistrarUsuario(txtBoxUsuario.Text, pwdBoxContrasenia.Password);
                resultado.MostrarMensaje();
                
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

        /// <summary>
        /// Guarda los datos del usuario en base de datos y accede directamente a
        /// ventana Conexión usando dichos datos.
        /// </summary>
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

        /// <summary>
        /// Llama al método Guardar()
        /// </summary>
        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            Guardar();
        }

        /// <summary>
        /// Vuelve a ventana Inicio
        /// </summary>
        private void btnAtras_Click(object sender, RoutedEventArgs e)
        {
            VentanaInicio vi = new VentanaInicio(usuarioGuardado);
            Manejador.CambiarVentana(this, vi);
        }

        /// <summary>
        /// Comprueba que el input del usuario tenga datos correctos
        /// </summary>
        private void txtBoxUsuario_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox datos = (TextBox)sender;
            Colorea.BordeCorrectoError(datos, Comprueba.UsuarioPrograma(datos.Text));
        }

        /// <summary>
        /// Comprueba que el input de la contraseña tenga datos correctos
        /// </summary>
        private void pwdBoxContrasenia_LostFocus(object sender, RoutedEventArgs e)
        {
            PasswordBox datos = (PasswordBox)sender;
            Colorea.BordeCorrectoError(datos, Comprueba.UsuarioPrograma(datos.Password));
        }

        /// <summary>
        /// Comprueba que el input de repetir la contraseña tenga datos correctos
        /// </summary>
        private void pwdBoxRepetirContrasenia_LostFocus(object sender, RoutedEventArgs e)
        {
            PasswordBox datos = (PasswordBox)sender;
            Colorea.BordeCorrectoError(datos, this.ComprobarContrasenias());
        }

        /// <summary>
        /// Comprueba que los inputs de usuario, contraseña y repetir contraseña sean correctos.
        /// </summary>
        /// <returns>True si los valores de los input son correctos.</returns>
        private bool ComprobarCampos()
        {
            return ((Comprueba.UsuarioPrograma(txtBoxUsuario.Text) ?? false)
                && (Comprueba.ContraseniaPrograma(pwdBoxContrasenia.Password) ?? false)
                && ComprobarContrasenias());
        }

        /// <summary>
        /// Comprueba que los inputs de contraseña y repetir contraseña sean correctos.
        /// </summary>
        /// <returns>True si los valores de los input son correctos.</returns>
        private bool ComprobarContrasenias()
        {
            return (Comprueba.ContraseniaPrograma(pwdBoxContrasenia.Password) ?? false)
                && pwdBoxRepetirContrasenia.Password.Equals(pwdBoxContrasenia.Password);
        }

        /// <summary>
        /// Comprueba si el usuario que ya se ha guardado corresponde con los valores
        /// introducidos en los inputs.
        /// </summary>
        /// <returns>True si el usuario guardado corresponde con los valores de los inputs.</returns>
        private bool UsuarioGuardadoCorrespondeConInputs()
        {
            if (this.usuarioGuardado != null)
            {
                if (this.usuarioGuardado.Nombre.Equals(txtBoxUsuario.Text) &&
                    this.usuarioGuardado.Contrasenia.Equals(pwdBoxContrasenia.Password))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
