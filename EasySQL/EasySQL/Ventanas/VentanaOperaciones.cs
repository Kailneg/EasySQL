using EasySQL.Modelos;
using EasySQL.Ventanas.Operaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EasySQL.Ventanas
{
    public partial class VentanaOperaciones : Window
    {
        private Conexion conexionActual;

        /// <summary>
        /// Actualiza la pantalla con los datos de la conexión.
        /// </summary>
        private void MostrarDatosConexion()
        {
            this.Title += " || Conectado usuario: " + conexionActual.UsuarioConexion +
                " - " + conexionActual.Direccion + " (" + conexionActual.Nombre + ")";
            lblDireccion.Content += " " + conexionActual.Direccion;
            lblBaseDatos.Content += " " + "No elegida";
            lblTipoConexion.Content += " " + conexionActual.TipoActual.ToString();
            lblUsuario.Content += " " + conexionActual.UsuarioConexion;
        }

        /*
         * Botones General
         */
        private void Atras()
        {
            VentanaConexion vc = new VentanaConexion(conexionActual.Propietario, conexionActual);
            Manejador.CambiarVentana(this, vc);
        }

        private void Cargar()
        {
            Utils.Consola.NoImplementado();
        }
        

        /*
         * Botones DDL
         */
        private void CreateDB()
        {
            VOperacionGenerica vog = new VOperacionGenerica("Introduce nombre de la BBDD a crear:");
            vog.ShowDialog();
        }

        private void DropDB()
        {
            VOperacionGenerica vog = new VOperacionGenerica("Introduce nombre de la BBDD a eliminar:");
            vog.ShowDialog();
            Utils.Consola.NoImplementado();
        }

        private void ShowDBs()
        {
            Utils.Consola.NoImplementado();
        }

        private void CreateTable()
        {
            VOperacionGenerica vog = new VOperacionGenerica("Introduce nombre de la tabla a crear:");
            vog.ShowDialog();
            Utils.Consola.NoImplementado();
        }

        private void DropTable()
        {
            VOperacionGenerica vog = new VOperacionGenerica("Introduce nombre de la tabla a crear:");
            vog.ShowDialog();
            Utils.Consola.NoImplementado();
        }

        private void ShowTables()
        {
            Utils.Consola.NoImplementado();
        }

        /*
         * Botones DML
         */
        private void Select()
        {
            Utils.Consola.NoImplementado();
        }

        private void Insert()
        {
            Utils.Consola.NoImplementado();
        }

        private void Update()
        {
            Utils.Consola.NoImplementado();
        }

        private void Delete()
        {
            Utils.Consola.NoImplementado();
        }
    }
}
