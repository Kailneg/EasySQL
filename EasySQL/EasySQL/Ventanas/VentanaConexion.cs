using EasySQL.BBDD;
using EasySQL.Modelos;
using System;
using System.Collections.Generic;
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
        private List<Conexion> conexionesUsuario;
        private Conexion conexionActual;

        private void ComprobacionInicial(Usuario usuario)
        {
            if (ComprobarUsuario(usuario))
            {
                conexionesUsuario = ObtenerConexionesUsuario();
                if (conexionesUsuario != null)
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

        private void MostrarTituloUsuario()
        {
            this.Title += " || Conectado usuario: " + usuarioActivo.Nombre;
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
            rbtnMicrosoftSQL.IsChecked = false;
            rbtnMySQL.IsChecked = false;
        }

        private List<Conexion> ObtenerConexionesUsuario()
        {
            //datosPrograma.ObtenerConexiones(usuarioActivo);
            // debe llamar a la bbdd y traer una lista con todas las conexiones pobladas
            // que existan para ese usuario
            throw new NotImplementedException();
        }

        private void MostarConexionesUsuario()
        {
            // Listview listaConexiones
            // Debe mostrar en el ListView cada una de las conexiones existentes en conexionesUsuario
            throw new NotImplementedException();
        }

        private void GuardarConexion()
        {

            conexionesUsuario = new List<Conexion>();
            conexionesUsuario.Add(new Conexion() { Nombre = "John Doe", Puerto = 42, Direccion = "john@doe-family.com" });
            conexionesUsuario.Add(new Conexion() { Nombre = "Jane Doe", Puerto = 39, Direccion = "jane@doe-family.com" });
            conexionesUsuario.Add(new Conexion() { Nombre = "Sammy Doe", Puerto = 7, Direccion = "sammy.doe@gmail.com" });
            conexionesUsuario.Add(new Conexion() { Nombre = "Aammy Foe", Puerto = 7, Direccion = "sammy.doe@gmail.com" });
            listViewConexiones.ItemsSource = conexionesUsuario;

            //if (ComprobarDatos())
            //{
            //    string nombreConex = txtBoxNombre.Text;
            //    string nombreDirecc = txtBoxDireccion.Text;
            //    //string no
            //    //listaConexiones.Items.Add(new Conexion());
            //}
        }

        private void LimpiarDatos()
        {
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
        /// Se comprueban todos los inputs de la ventana y que estos tengan datos correctos.
        /// </summary>
        /// <returns>Devuelve true si los campos obligatorios tienen valores correctos y los demás
        /// estén vacíos o con valores correctos.</returns>
        private bool ComprobarDatos()
        {
            //Campos obligatorios: nombre conexión, dirección, usuario, tipo conexión.
            Console.Write("ComprobarDatos no implementado");
            return true;
        }

        private void TestConexion()
        {
            Console.Write("ComprobarConexion no implementado");
        }

        private void Conectar()
        {
            if (ComprobarDatos())
            {
                Conexion datosActuales = new Conexion();
                VentanaOperaciones vo = new VentanaOperaciones(datosActuales);
                this.Close();
                vo.Show();
            }
        }

        private void Cancelar()
        {
            VentanaInicio vi = new VentanaInicio(usuarioActivo);
            this.Close();
            vi.Show();
        }

        /*
         * Métodos ListView
         */
        private void ListaActualizar()
        {
            Utils.Consola.NoImplementado();
        }

        private void ListaBorrar()
        {
            // Debo mandar el comando sql para borrar y
            LimpiarDatos();
            Utils.Consola.NoImplementado();
        }

        /// <summary>
        /// Ordena alfabéticamente según "Nombre" la lista de conexiones.
        /// En caso de no existir conexiones, lanza un aviso.
        /// </summary>
        private void ListaOrdenar()
        {
            if (listViewConexiones.Items.Count > 0)
            {
                conexionesUsuario = conexionesUsuario.OrderBy(c => c.Nombre).ToList();
                // Reasigna las conexiones al listView para reflejar los cambios.
                listViewConexiones.ItemsSource = conexionesUsuario;
            } else
            {
                MessageBox.Show("No existen conexiones a ordenar");
            }
        }

        private void SeleccionCambiada(object sender)
        {
            Conexion nueva = (sender as ListView).SelectedItem as Conexion;
            ActualizarConexionActual(nueva);
        }

        /// <summary>
        /// Desactiva los campos Usuario y contraseña en caso de marcar la opción
        /// "Integrated Security" para SQL Server.
        /// </summary>
        /// <param name="sender"></param>
        private void IntegratedSecurity(object sender)
        {
            bool habilitado = !(sender as CheckBox).IsChecked.Value;
            txtBoxUsuario.IsEnabled = habilitado;
            txtBoxContrasenia.IsEnabled = habilitado;
            chkGuardarContrasenia.IsEnabled = habilitado;
            rbtnMySQL.IsEnabled = habilitado;
            rbtnMicrosoftSQL.IsChecked = true;
        }
    }
}
