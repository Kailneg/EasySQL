﻿using EasySQL.BBDD;
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
            pwdBoxContrasenia.Password = usuario.Contrasenia;
        }

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
                MessageBox.Show("Uno o más campos contienen errores");
            }
        }
        

        private void btnInvitado_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Aviso: funcionalidades de guardado de conexiones no están disponibles en modo invitado.");
            VentanaConexion vc = new VentanaConexion(null);
            Manejador.CambiarVentana(this, vc);
        }

        private void btnRegistro_Click(object sender, RoutedEventArgs e)
        {
            VentanaRegistro vr = new VentanaRegistro(this);
            Manejador.CambiarVentana(this, vr);
        }

        private void txtBoxUsuario_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox datos = (TextBox)sender;
            Colorea.BordeCorrectoErrorDefecto(datos, Comprueba.UsuarioPrograma(datos.Text));
        }

        private void pwdBoxContrasenia_LostFocus(object sender, RoutedEventArgs e)
        {
            PasswordBox datos = (PasswordBox)sender;
            Colorea.BordeCorrectoErrorDefecto(datos, Comprueba.ContraseniaPrograma(datos.Password));
        }

        private bool ComprobarCampos()
        {
            return ((Comprueba.UsuarioPrograma(txtBoxUsuario.Text) ?? false)
                && (Comprueba.ContraseniaPrograma(pwdBoxContrasenia.Password) ?? false));
        }
    }
}
