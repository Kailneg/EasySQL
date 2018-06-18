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

namespace EasySQL.Ventanas
{
    /// <summary>
    /// Interaction logic for VentanaInicio.xaml
    /// </summary>
    public partial class VentanaInicio : Window
    {
        /// <summary>
        /// Crea una nueva ventana de inicio e inicia sus componentes.
        /// </summary>
        public VentanaInicio()
        {
            InitializeComponent();  
        }

        /// <summary>
        /// Crea una nueva ventana de inicio e inicia sus componentes.
        /// Usado para su llamada desde ventana registro.
        /// </summary>
        /// <param name="usuario">El usuario del que extraer los datos para rellenar los inputs.</param>
        public VentanaInicio(Usuario usuario) : this()
        {
            if (usuario != null)
            {
                RefrescarTexboxes(usuario);
            }
        }

        /// <summary>
        /// Asigna nuevos valores a los inputs.
        /// </summary>
        /// <param name="usuario">El usuario del que extraer los datos para rellenar los inputs.</param>
        private void RefrescarTexboxes(Usuario usuario)
        {
            txtBoxUsuario.Text = usuario.Nombre;
            pwdBoxContrasenia.Password = usuario.Contrasenia;
        }

        /// <summary>
        /// Comprueba los inputs y realiza una comprobación de los datos contra la base de datos.
        /// Si son correctos, se cambia ventana a ventana Conexión.
        /// </summary>
        private void btnAcceder_Click(object sender, RoutedEventArgs e)
        {
            if (ComprobarCampos())
            {                
                ResultadoLogin resultado =
                    BBDDPrograma.LoginUsuario(txtBoxUsuario.Text, pwdBoxContrasenia.Password);
                resultado.MostrarMensaje();

                // Si el login ha sido correcto, abrimos la ventana de conexión pasando el usuario logeado.
                if (resultado.ResultadoActual == ResultadoLogin.TipoResultado.ACEPTADO)
                {
                    VentanaConexion vc = new VentanaConexion(resultado.UsuarioActual);
                    Manejador.CambiarVentana(this, vc);
                }
            }
            else
            {
                Msj.Error("Uno o más campos contienen errores");
            }
        }
        
        /// <summary>
        /// Cambia a la ventana Conexion en modo Invitado.
        /// </summary>
        private void btnInvitado_Click(object sender, RoutedEventArgs e)
        {
            Msj.Aviso("Aviso: funcionalidades de guardado de conexiones no están disponibles en modo invitado.");
            VentanaConexion vc = new VentanaConexion(null);
            Manejador.CambiarVentana(this, vc);
        }

        /// <summary>
        /// Cambia a la ventana Registro
        /// </summary>
        private void btnRegistro_Click(object sender, RoutedEventArgs e)
        {
            VentanaRegistro vr = new VentanaRegistro();
            Manejador.CambiarVentana(this, vr);
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
        /// Comprueba que el input del usuario tenga datos correctos
        /// </summary>
        private void pwdBoxContrasenia_LostFocus(object sender, RoutedEventArgs e)
        {
            PasswordBox datos = (PasswordBox)sender;
            Colorea.BordeCorrectoError(datos, Comprueba.ContraseniaPrograma(datos.Password));
        }

        /// <summary>
        /// Comprueba que los inputs de usuario y contraseña sean correctos.
        /// </summary>
        /// <returns>True si los valores de los input son correctos.</returns>
        private bool ComprobarCampos()
        {
            return ((Comprueba.UsuarioPrograma(txtBoxUsuario.Text) ?? false)
                && (Comprueba.ContraseniaPrograma(pwdBoxContrasenia.Password) ?? false));
        }
    }
}
