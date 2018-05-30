﻿using EasySQL.BBDD;
using EasySQL.Modelos;
using EasySQL.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace EasySQL.Ventanas
{
    public partial class VentanaConexion : Window
    {
        private Usuario usuarioActivo;
        private ObservableCollection<Conexion> listaConexiones;
        private Conexion conexionActual;

        private void ComprobacionInicial(Usuario usuario)
        {
            if (ComprobarUsuario(usuario))
            {
                listaConexiones = ObtenerConexionesUsuario();
                if (listaConexiones != null)
                {
                    MostarConexionesUsuario();
                }
            }
            
        }

        private bool ComprobarUsuario(Usuario usuario)
        {
            if (usuario != null)
            {
                usuarioActivo = usuario;
                MostrarTituloUsuario();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Actualiza el título de la pantalla con el nombre e ID del usuario.
        /// </summary>
        private void MostrarTituloUsuario()
        {
            this.Title += " || Conectado usuario: " + usuarioActivo.Nombre
                + " con ID: " + usuarioActivo.ID;
        }

        /// <summary>
        /// Actualiza la conexion seleccionada actual, reflejando los nuevos datos por pantalla.
        /// </summary>
        /// <param name="actual">La nueva conexión a mostrar</param>
        private void ActualizarConexionActual(Conexion actual)
        {
            conexionActual = actual;
            txtBoxNombre.Text = actual.Nombre;
            txtBoxDireccion.Text = actual.Direccion;
            txtBoxPuerto.Text = actual.Puerto.ToString();
            txtBoxUsuario.Text = actual.UsuarioConexion;
            txtBoxContrasenia.Text = actual.ContraseniaConexion;
            chkGuardarContrasenia.IsChecked = false;
            rbtnMicrosoftSQL.IsChecked = actual.TipoActual.Equals(Conexion.TipoConexion.MicrosoftSQL);
            rbtnMySQL.IsChecked = actual.TipoActual.Equals(Conexion.TipoConexion.MySQL);
        }

        /// <summary>
        /// Hace una llamada a la BBDD para obtener las conexiones del usuario
        /// </summary>
        /// <returns>Una lista observable con las conexiones del usuario</returns>
        private ObservableCollection<Conexion> ObtenerConexionesUsuario()
        {
            return BBDDPrograma.ObtenerConexionesUsuario(usuarioActivo);
        }

        private void MostarConexionesUsuario()
        {
            // Listview listaConexiones
            // Debe mostrar en el ListView cada una de las conexiones existentes en conexionesUsuario
            listViewConexiones.ItemsSource = listaConexiones;
        }

        private bool GuardarConexion()
        {
            Conexion guardar = ComprobarCampos();
            if (guardar != null)
            {
                ResultadoConexion resultado =
                    BBDDPrograma.RegistrarConexion(guardar);
                resultado.MostrarMensaje();

                // Si se guarda, lo almacenamos temporalmente por si se desea acceder directamente
                if (resultado.ResultadoActual == ResultadoConexion.TipoResultado.ACEPTADO)
                {
                    conexionActual = resultado.ConexionGuardar;
                    listaConexiones.Add(conexionActual);
                }
                return (resultado.ResultadoActual == ResultadoConexion.TipoResultado.ACEPTADO);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Se comprueban todos los inputs de la ventana y que estos tengan datos correctos.
        /// </summary>
        /// <returns>Devuelve una Conexion si los campos obligatorios tienen valores correctos y los demás
        /// estén vacíos o con valores correctos.</returns>
        private Conexion ComprobarCampos()
        {
            // Campos obligatorios: nombre conexión, dirección, usuario, tipo conexión.

            // El operador ?? operador devuelve el operando izquierdo si no es NULL; 
            // de lo contrario, devuelve el operando derecho.
            if ((Comprueba.Nombre(txtBoxNombre.Text) ?? false)
                && (Comprueba.Direccion(txtBoxDireccion.Text) ?? false)
                && (Comprueba.Usuario(txtBoxUsuario.Text) ?? false)
                && (Comprueba.Puerto(txtBoxPuerto.Text)))
            {
                string nombre = txtBoxNombre.Text;
                string direccion = txtBoxDireccion.Text;
                string usuario = txtBoxUsuario.Text;
                int puerto = 0;
                if (!String.IsNullOrWhiteSpace(txtBoxPuerto.Text))
                    puerto = Int32.Parse(txtBoxPuerto.Text);
                Conexion.TipoConexion tipo;

                // Comprobando si el tipo de conexión está marcado
                if (rbtnMicrosoftSQL.IsChecked.Value)
                {
                    tipo = Conexion.TipoConexion.MicrosoftSQL;
                    if (chkIntegratedSecurity.IsChecked.Value)
                        usuario = Usuario.NombreIntegratedSecurity;
                }
                else if (rbtnMySQL.IsChecked.Value) {
                    tipo = Conexion.TipoConexion.MySQL;
                }
                else
                {
                    MessageBox.Show("No se ha marcado el tipo de conexión.");
                    return null;
                }

                // Si los campos están correctos, mirar si se quiere guardar la contraseña.
                if (chkGuardarContrasenia.IsChecked.Value)
                {
                    if (Comprueba.Contrasenia(txtBoxContrasenia.Text) ?? false)
                    {
                        // Todo correcto, se devuelve una conexion guardando contraseña
                        string contrasenia = txtBoxContrasenia.Text;
                        Conexion guardar = new Conexion(nombre, direccion, puerto, usuario,
                            contrasenia, tipo, usuarioActivo);
                        return guardar;
                    }
                    else
                    {
                        MessageBox.Show("Se ha marcado 'guardar contraseña' pero está vacía o con valores nulos.");
                        return null;
                    }
                }
                else
                {
                    // Todo correcto, se devuelve una conexion SIN guardar contraseña
                    return new Conexion(nombre, direccion, puerto, usuario, tipo, usuarioActivo);
                }
            }
            else
            {
                MessageBox.Show("Uno o más campos obligatorios están vacíos o con valores nulos.");
                return null;
            }
        }

        private void TestConexion()
        {
            Console.Write("ComprobarConexion no implementado");
        }

        private void Conectar()
        {
            if (conexionActual != null)
            {
                VentanaOperaciones vo = new VentanaOperaciones(conexionActual);
                Manejador.CambiarVentana(this, vo);
            } else
            {
                MessageBox.Show("No existe una conexión válida guardada");
            }
        }

        private void Cancelar()
        {
            VentanaInicio vi = new VentanaInicio(usuarioActivo);
            Manejador.CambiarVentana(this, vi);
        }

        /*
         * Métodos ListView
         */
        private void ListaActualizar()
        {
            // Debe traer de nuevo las conexiones del usuario de la bbdd
            // Reasigna las conexiones al listView para reflejar los cambios.
            MostarConexionesUsuario();
        }

        /// <summary>
        /// Elimina de la lista de conexiones la conexión seleccionada, ejecuta el comando 
        /// que borra la conexión de la BBDD, y limpia los campos de la aplicación
        /// </summary>
        private void ListaBorrar()
        {
            listaConexiones.Remove(conexionActual);
            BBDDPrograma.EliminarConexion(conexionActual);
            LimpiarDatos();
            Utils.Consola.NoImplementado();
        }
        
        private void LimpiarDatos()
        {
            conexionActual = null;
            txtBoxNombre.Text = "";
            txtBoxDireccion.Text = "";
            txtBoxPuerto.Text = "";
            txtBoxUsuario.Text = "";
            txtBoxContrasenia.Text = "";
            chkGuardarContrasenia.IsChecked = false;
            rbtnMicrosoftSQL.IsChecked = false;
            rbtnMySQL.IsChecked = false;
        }

        /// <summary>
        /// Ordena alfabéticamente según "Nombre" la lista de conexiones.
        /// En caso de no existir conexiones, lanza un aviso.
        /// </summary>
        private void ListaOrdenar()
        {
            if (listViewConexiones.Items.Count > 0)
            {

                listaConexiones = new ObservableCollection<Conexion>(listaConexiones.OrderBy(c => c.Nombre));
                ListaActualizar();
            } else
            {
                MessageBox.Show("No existen conexiones a ordenar");
            }
        }

        private void SeleccionCambiada(object sender)
        {
            Conexion nueva = (sender as ListView).SelectedItem as Conexion;
            if (nueva != null)
            {
                ActualizarConexionActual(nueva);
            }
        }

        /// <summary>
        /// Desactiva los campos Usuario y contraseña en caso de marcar la opción
        /// "Integrated Security" para SQL Server.
        /// </summary>
        /// <param name="sender"></param>
        private void IntegratedSecurity(object sender)
        {
            bool pulsado = (sender as CheckBox).IsChecked.Value;
            txtBoxUsuario.IsEnabled = !pulsado;
            txtBoxContrasenia.IsEnabled = !pulsado;
            chkGuardarContrasenia.IsEnabled = !pulsado;
            chkGuardarContrasenia.IsChecked = false;
            rbtnMySQL.IsEnabled = !pulsado;
            rbtnMicrosoftSQL.IsChecked = true;
            if (pulsado)
                txtBoxUsuario.Text = Usuario.NombreIntegratedSecurity;
            else
                txtBoxUsuario.Text = "";
        }
    }
}
