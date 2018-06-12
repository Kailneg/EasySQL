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
    /// Lógica de interacción para VentanaConexion.xaml
    /// </summary>
    public partial class VentanaConexion : Window
    {
        /// <summary>
        /// Crea una nueva ventana de conexión e inicia sus componentes.
        /// Carga un usuario. Constructor llamado desde ventana Registro.
        /// </summary>
        /// <param name="usuario">El usuario actualmente activo, o null
        /// si se está en modo invitado.</param>
        public VentanaConexion(Usuario usuario) : this (usuario, null) {}

        /// <summary>
        /// Crea una nueva ventana de conexión e inicia sus componentes.
        /// Carga un usuario y una conexión. 
        /// Constructor llamado desde ventana Operaciones.
        /// </summary>
        /// <param name="usuario">El usuario actualmente activo, o null
        /// si se está en modo invitado.</param>
        /// <param name="conexion">La conexión activa del momento.</param>
        public VentanaConexion(Usuario usuario, Conexion conexion)
        {
            InitializeComponent();
            ComprobacionInicial(usuario, conexion);
        }

        /*
         * Botones General
         */
        /// <summary>
        /// Captura el evento click del botón Cancelar y ejecuta la lógica.
        /// </summary>
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Cancelar();
        }

        /// <summary>
        /// Captura el evento click del botón Limpiar y ejecuta la lógica.
        /// </summary>
        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarDatos();
        }

        /// <summary>
        /// Captura el evento click del botón Guardar y ejecuta la lógica.
        /// </summary>
        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            Guardar();
        }

        /// <summary>
        /// Captura el evento click del botón Test y ejecuta la lógica.
        /// </summary>
        private void btnTest_Click(object sender, RoutedEventArgs e)
        {
            Test();
        }

        /// <summary>
        /// Captura el evento click del botón Conectar y ejecuta la lógica.
        /// </summary>
        private void btnConectar_Click(object sender, RoutedEventArgs e)
        {
            Conectar();
        }

        /*
         * Botones ListView
         */
        /// <summary>
        /// Captura el evento click del botón Actualizar y ejecuta la lógica.
        /// </summary>
        private void btnListaActualizar_Click(object sender, RoutedEventArgs e)
        {
            ListaActualizar();
        }

        /// <summary>
        /// Captura el evento click del botón Borrar y ejecuta la lógica.
        /// </summary>
        private void btnListaBorrar_Click(object sender, RoutedEventArgs e)
        {
            ListaBorrar();
        }

        /// <summary>
        /// Captura el evento click del botón OrdenarID y ejecuta la lógica.
        /// </summary>
        private void btnListaOrdenarID_Click(object sender, RoutedEventArgs e)
        {
            ListaOrdenarID();
        }

        /// <summary>
        /// Captura el evento click del botón OrdenarNombre y ejecuta la lógica.
        /// </summary>
        private void btnListaOrdenarNombre_Click(object sender, RoutedEventArgs e)
        {
            ListaOrdenarNombre();
        }

        /// <summary>
        /// Comprueba que el input Nombre tenga los datos correctos.
        /// </summary>
        private void txtBoxNombre_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox datos = (TextBox)sender;
            Colorea.BordeCorrectoError(datos, Comprueba.Nombre(datos.Text));
        }

        /// <summary>
        /// Comprueba que el input Direccion tenga los datos correctos.
        /// </summary>
        private void txtBoxDireccion_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox datos = (TextBox)sender;
            Colorea.BordeCorrectoError(datos, Comprueba.Direccion(datos.Text));
        }

        /// <summary>
        /// Comprueba que el input Puerto tenga los datos correctos.
        /// </summary>
        private void txtBoxPuerto_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox datos = (TextBox)sender;
            Colorea.BordeCorrectoErrorDefecto(datos, Comprueba.Puerto(datos.Text));
        }

        /// <summary>
        /// Comprueba que el input Usuario tenga los datos correctos.
        /// </summary>
        private void txtBoxUsuario_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox datos = (TextBox)sender;
            Colorea.BordeCorrectoError(datos, Comprueba.UsuarioConexion(datos.Text));
        }

        /// <summary>
        /// Comprueba que el input Contrasenia tenga los datos correctos.
        /// </summary>
        private void pwdBoxContrasenia_LostFocus(object sender, RoutedEventArgs e)
        {
            PasswordBox datos = (PasswordBox)sender;
            Colorea.BordeCorrectoErrorDefecto(datos, Comprueba.ContraseniaConexion(datos.Password));
        }

        /// <summary>
        /// Ejecuta la lógica al cambiar de selección en el list view de conexiones.
        /// </summary>
        private void listViewConexiones_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SeleccionCambiada(sender);
        }

        /// <summary>
        /// Ejecuta la lógica al pulsar el checkbox Integrated Security.
        /// </summary>
        private void chkIntegratedSecurity_Click(object sender, RoutedEventArgs e)
        {
            IntegratedSecurity(sender);
        }
    }
}
