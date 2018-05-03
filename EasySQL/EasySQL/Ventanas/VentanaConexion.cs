using EasySQL.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EasySQL.Ventanas
{
    public partial class VentanaConexion : Window
    {
        private void GuardarConexion()
        {
            if (ComprobarDatos())
            {
                string nombreConex = txtBoxNombre.Text;
                string nombreDirecc = txtBoxDireccion.Text;
                //string no
                //listaConexiones.Items.Add(new Conexion());
            }
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
            rbtnOracleDB.IsChecked = false;
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
            Utils.Consola.NoImplementado();
        }

        private void ListaOrdenar()
        {
            Utils.Consola.NoImplementado();
        }
    }
}
