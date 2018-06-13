using EasySQL.BBDD;
using EasySQL.Modelos;
using EasySQL.Operaciones.Operacion;
using EasySQL.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace EasySQL.Ventanas
{
    public partial class VentanaConexion : Window
    {
        /// <summary>
        /// El usuario activo del momento.
        /// </summary>
        private Usuario usuarioActivo;

        /// <summary>
        /// Una lista con las conexiones que tiene el usuario.
        /// </summary>
        private ObservableCollection<Conexion> listaConexiones;

        /// <summary>
        /// La conexión asignada actual
        /// </summary>
        private Conexion conexionActual;

        /// <summary>
        /// Indica si la interfaz de usuario se encuentre en modo invitado.
        /// </summary>
        private bool modoInvitado;

        /// <summary>
        /// Realiza la primera comprobación de datos y asigna los parámetros necesarios.
        /// </summary>
        /// <param name="usuario">El usuario activo actual, o null si se accede en modo invitado.</param>
        /// <param name="conexion">La conexión actual en caso de invocar la ventana desde ventana operaciones.</param>
        private void ComprobacionInicial(Usuario usuario, Conexion conexion)
        {
            if (usuario != null)
            {
                usuarioActivo = usuario;
                MostrarTitulo(" || Conectado usuario: " + usuarioActivo.Nombre
                    + " con ID: " + usuarioActivo.ID);
                listaConexiones = ObtenerConexionesUsuario();
                if (listaConexiones != null)
                {
                    RefrescarConexionesListview();
                }
            } else
            {
                ModoInvitado();
            }
            if (conexion != null)
            {
                conexionActual = conexion;
                ActualizarConexionActual(conexionActual);
            }
        }

        /// <summary>
        /// Realiza los cambios pertinentes en la interfaz de usuario para ponerla en modo invitado.
        /// </summary>
        private void ModoInvitado()
        {
            modoInvitado = true;
            MostrarTitulo(" || Conectado como invitado");
            btnListaActualizar.IsEnabled = false;
            btnListaOrdenarID.IsEnabled = false;
            btnListaOrdenarNombre.IsEnabled = false;
            btnListaBorrar.IsEnabled = false;
            btnGuardar.IsEnabled = false;
            listViewConexiones.IsEnabled = false;
            lblListaConexiones.IsEnabled = false;
            txtBoxNombre.IsEnabled = false;
        }

        /// <summary>
        /// Actualiza el título de la pantalla con el nombre e ID del usuario.
        /// </summary>
        private void MostrarTitulo(string titulo)
        {
            this.Title += titulo;
        }

        /// <summary>
        /// Actualiza la conexion seleccionada actual, reflejando los nuevos datos por pantalla.
        /// </summary>
        /// <param name="actual">La nueva conexión a mostrar</param>
        private void ActualizarConexionActual(Conexion actual)
        {
            conexionActual = actual;
            lblConexionActual.Content = "Conexión actual: " + actual.Nombre;
            txtBoxNombre.Text = actual.Nombre;
            txtBoxDireccion.Text = actual.Direccion;
            txtBoxPuerto.Text = actual.Puerto.ToString();
            txtBoxUsuario.Text = actual.UsuarioConexion;
            pwdBoxContrasenia.Password = actual.ContraseniaConexion;
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

        /// <summary>
        /// Muestra en el ListView cada una de las conexiones existentes en conexionesUsuario
        /// </summary>
        private void RefrescarConexionesListview()
        {
            listViewConexiones.ItemsSource = listaConexiones;
        }

        /// <summary>
        /// Guarda la conexión resultante de los datos actualmente introducidos en base de datos,
        /// o en modo local si se está en modo invitado.
        /// </summary>
        /// <returns>True si la conexión se ha podido guardar.</returns>
        private bool Guardar()
        {
            Conexion guardar = ComprobarCampos();
            if (guardar != null)
            {
                if (!modoInvitado)
                {
                    // Si no está marcado el check de contraseñas, se borran los datos
                    if (chkGuardarContrasenia.IsChecked.Value)
                    {
                        if (Comprueba.ContraseniaConexion(pwdBoxContrasenia.Password) ?? false)
                        {
                            // Todo correcto, se devuelve una conexion guardando contraseña
                            guardar.ContraseniaConexion = pwdBoxContrasenia.Password;
                        }
                        else
                        {
                            MessageBox.Show("Se ha marcado 'guardar contraseña' pero está vacía o con valores nulos.");
                            return false;
                        }
                    }
                    else
                    {
                        guardar.ContraseniaConexion = "";
                    }
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
                    conexionActual = guardar;
                    return true;
                }
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

            // Comprueba si el nombre tiene datos correctos o está deshabilitado (modoInvitado)
            if (((Comprueba.Nombre(txtBoxNombre.Text)) || !txtBoxNombre.IsEnabled)
                && (Comprueba.Direccion(txtBoxDireccion.Text)) 
                && (Comprueba.UsuarioConexion(txtBoxUsuario.Text))
                && (Comprueba.Puerto(txtBoxPuerto.Text) ?? true))
            {
                string nombre = txtBoxNombre.Text;
                string direccion = txtBoxDireccion.Text;
                string usuario = txtBoxUsuario.Text;
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
                int puerto = 0;
                if (!String.IsNullOrWhiteSpace(txtBoxPuerto.Text))
                    puerto = Int32.Parse(txtBoxPuerto.Text);
                if (puerto == 0)
                    puerto = BBDDPrograma.ObtenerPuertoDefecto(tipo);

                string contrasenia = pwdBoxContrasenia.Password;
                Conexion guardar = new Conexion(nombre, direccion, puerto, usuario,
                    contrasenia, tipo, usuarioActivo);
                return guardar;

            }
            else
            {
                MessageBox.Show("Uno o más campos obligatorios están vacíos o con valores nulos.");
                return null;
            }
        }

        /// <summary>
        /// Realiza un test de conexión que informa al usuario de manera visual
        /// si los campos introducidos tienen una conexión válida.
        /// </summary>
        /// <returns>True si la conexión es válida.</returns>
        private bool Test()
        {
            conexionActual = ComprobarCampos();
            if (conexionActual != null)
            {
                // Muestra la conexión actual
                ActualizarConexionActual(conexionActual);
                // Obtiene el resultado
                bool retorno = Operacion.ExecuteTest(conexionActual);

                if (retorno)
                    MessageBox.Show("Conexión correcta.");
                else
                    MessageBox.Show("Conexión fallida");
                return retorno;
            }
            else
            {
                MessageBox.Show("No existe conexión actual guardada correcta.");
                return false;
            }
        }

        /// <summary>
        /// Comprueba si existe una conexión elegida correcta y cambia a ventana Operaciones
        /// </summary>
        private void Conectar()
        {
            // Si existe una conexión elegida correcta, o se está en modo invitado
            // y los inputs tienen los datos de una conexion correcta, cambiar ventana
            if (!modoInvitado)
            {
                if (conexionActual != null)
                {
                    if (Test())
                    {
                        VentanaOperaciones vo = new VentanaOperaciones(conexionActual);
                        Manejador.CambiarVentana(this, vo);
                    }
                }
                else
                {
                    MessageBox.Show("No existe una conexión válida guardada");
                }
            }
            else
            {
                if (modoInvitado && (conexionActual = ComprobarCampos()) != null)
                {
                    if (Test())
                    {
                        VentanaOperaciones vo = new VentanaOperaciones(conexionActual);
                        Manejador.CambiarVentana(this, vo);
                    }
                }
            }
        }

        /// <summary>
        /// Cierra la ventana actual y vuelve a la ventana de Inicio.
        /// </summary>
        private void Cancelar()
        {
            VentanaInicio vi = new VentanaInicio(usuarioActivo);
            Manejador.CambiarVentana(this, vi);
        }

        /*
         * Métodos ListView
         */
        /// <summary>
        /// Trae la lista de conexiones de la base de datos y refresca los datos
        /// </summary>
        private void ListaActualizar()
        {
            listaConexiones = ObtenerConexionesUsuario();
            RefrescarConexionesListview();
        }

        /// <summary>
        /// Elimina de la lista de conexiones la conexión seleccionada, ejecuta el comando 
        /// que borra la conexión de la BBDD, y limpia los campos de la aplicación
        /// </summary>
        private void ListaBorrar()
        {
            if (conexionActual != null)
            {
                listaConexiones.Remove(conexionActual);
                BBDDPrograma.EliminarConexion(conexionActual);
                LimpiarDatos();
            }
        }
        
        /// <summary>
        /// Vacía y restablece los campos de información.
        /// </summary>
        private void LimpiarDatos()
        {
            conexionActual = null;
            txtBoxNombre.Text = "";
            txtBoxDireccion.Text = "";
            txtBoxPuerto.Text = "";
            txtBoxUsuario.Text = "";
            pwdBoxContrasenia.Password = "";
            chkGuardarContrasenia.IsChecked = false;
            rbtnMicrosoftSQL.IsChecked = false;
            rbtnMySQL.IsChecked = false;
            Colorea.BordeCorrectoErrorDefecto(txtBoxNombre, null);
            Colorea.BordeCorrectoErrorDefecto(txtBoxDireccion, null);
            Colorea.BordeCorrectoErrorDefecto(txtBoxPuerto, null);
            Colorea.BordeCorrectoErrorDefecto(txtBoxUsuario, null);
            Colorea.BordeCorrectoErrorDefecto(pwdBoxContrasenia, null);
        }

        /// <summary>
        /// Ordena alfabéticamente según "Nombre" la lista de conexiones.
        /// En caso de no existir conexiones, lanza un aviso.
        /// </summary>
        private void ListaOrdenarNombre()
        {
            if (listViewConexiones.Items.Count > 0)
            {
                listaConexiones = new ObservableCollection<Conexion>(listaConexiones.OrderBy(c => c.Nombre));
                RefrescarConexionesListview();
            } else
            {
                MessageBox.Show("No existen conexiones a ordenar");
            }
        }

        /// <summary>
        /// Ordena numéricamente según "ID" la lista de conexiones.
        /// En caso de no existir conexiones, lanza un aviso.
        /// </summary>
        private void ListaOrdenarID()
        {
            if (listViewConexiones.Items.Count > 0)
            {
                listaConexiones = new ObservableCollection<Conexion>(listaConexiones.OrderBy(c => c.ID));
                RefrescarConexionesListview();
            }
            else
            {
                MessageBox.Show("No existen conexiones a ordenar");
            }
        }

        /// <summary>
        /// Actualiza los campos que reflejan la información de una conexión al
        /// cambiar de selección en el ListView de conexiones.
        /// </summary>
        /// <param name="sender">ListView conexiones del que se reciben los nuevos datos</param>
        private void SeleccionCambiada(object sender)
        {
            Conexion nueva = (sender as ListView).SelectedItem as Conexion;
            if (nueva != null)
            {
                LimpiarDatos();
                ActualizarConexionActual(nueva);
            }
        }

        /// <summary>
        /// Realiza cambios en la UI como desactivar los campos 
        /// Usuario y contraseña en caso de marcar la opción
        /// "Integrated Security" para SQL Server.
        /// </summary>
        private void IntegratedSecurity(object sender)
        {
            bool pulsado = (sender as CheckBox).IsChecked.Value;
            txtBoxUsuario.IsEnabled = !pulsado;
            pwdBoxContrasenia.IsEnabled = !pulsado;
            chkGuardarContrasenia.IsEnabled = !pulsado;
            chkGuardarContrasenia.IsChecked = false;
            rbtnMySQL.IsEnabled = !pulsado;
            rbtnMicrosoftSQL.IsChecked = true;
            if (pulsado)
            {
                txtBoxUsuario.Text = Usuario.NombreIntegratedSecurity;
                pwdBoxContrasenia.Password = "";
                Colorea.BordeCorrectoError(txtBoxUsuario, true);
                Colorea.BordeCorrectoError(pwdBoxContrasenia, true);
            }
            else
            {
                txtBoxUsuario.Text = "";
                pwdBoxContrasenia.Password = "";
                Colorea.BordeCorrectoError(txtBoxUsuario, false);
                Colorea.BordeCorrectoErrorDefecto(pwdBoxContrasenia, null);
            }
        }
    }
}
