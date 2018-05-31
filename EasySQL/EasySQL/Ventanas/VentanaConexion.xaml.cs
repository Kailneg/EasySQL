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
        public VentanaConexion(Usuario usuario) : this (usuario, null) {}

        public VentanaConexion(Usuario usuario, Conexion conexion)
        {
            InitializeComponent();
            ComprobacionInicial(usuario, conexion);
        }

        /*
         * Botones General
         */
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Cancelar();
        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarDatos();
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            GuardarConexion();
        }

        private void btnTest_Click(object sender, RoutedEventArgs e)
        {
            TestConexion();
        }

        private void btnConectar_Click(object sender, RoutedEventArgs e)
        {
            Conectar();
        }

        /*
         * Botones ListView
         */
        private void btnListaActualizar_Click(object sender, RoutedEventArgs e)
        {
            ListaActualizar();
        }

        private void btnListaBorrar_Click(object sender, RoutedEventArgs e)
        {
            ListaBorrar();
        }

        private void btnListaOrdenarID_Click(object sender, RoutedEventArgs e)
        {
            ListaOrdenarID();
        }

        private void btnListaOrdenarNombre_Click(object sender, RoutedEventArgs e)
        {
            ListaOrdenarNombre();
        }

        private void txtBoxNombre_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox datos = (TextBox)sender;
            Colorea.BordeCorrectoError(datos, Comprueba.Nombre(datos.Text));
        }

        private void txtBoxDireccion_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox datos = (TextBox)sender;
            Colorea.BordeCorrectoError(datos, Comprueba.Direccion(datos.Text));
        }

        private void txtBoxPuerto_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox datos = (TextBox)sender;
            Colorea.BordeCorrectoErrorDefecto(datos, Comprueba.Puerto(datos.Text));
        }

        private void txtBoxUsuario_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox datos = (TextBox)sender;
            Colorea.BordeCorrectoError(datos, Comprueba.UsuarioConexion(datos.Text));
        }

        private void txtBoxContrasenia_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox datos = (TextBox)sender;
            Colorea.BordeCorrectoErrorDefecto(datos, Comprueba.ContraseniaConexion(datos.Text));
        }

        private void listViewConexiones_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SeleccionCambiada(sender);
        }

        private void chkIntegratedSecurity_Click(object sender, RoutedEventArgs e)
        {
            IntegratedSecurity(sender);
        }

    }
}
