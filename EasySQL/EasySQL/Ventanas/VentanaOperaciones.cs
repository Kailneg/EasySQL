using EasySQL.Modelos;
using EasySQL.Operaciones.Controlador;
using EasySQL.Utils;
using EasySQL.Ventanas.Operaciones;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace EasySQL.Ventanas
{
    public partial class VentanaOperaciones : Window
    {
        private Conexion conexionActual;
        private static readonly string CMB_BASEDATOS_DEFECTO = "Elige base de datos...";
        private static readonly string DESCRIPCION_DROPDB = "Introduce nombre de la BBDD a eliminar:";
        private static readonly string DESCRIPCION_DROPTABLE = "Introduce nombre de la tabla a eliminar:";

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
            cmbBaseDatos.Items.Add(CMB_BASEDATOS_DEFECTO);
            cmbBaseDatos.SelectedItem = CMB_BASEDATOS_DEFECTO;
            cmbBaseDatos.Items.Add("bbdd1");
        }
        
        private bool SeleccionCambiada()
        {
            // Comprobar que la base de datos no sea la por defecto
            if (cmbBaseDatos.SelectedItem != null
                && !cmbBaseDatos.SelectedItem.Equals(CMB_BASEDATOS_DEFECTO)) {
                // Actualiza label descriptivo
                lblBaseDatos.Content = "Base de datos : " + cmbBaseDatos.SelectedItem;
                // Asigna BBDD
                conexionActual.BaseDatos = cmbBaseDatos.SelectedItem.ToString();
                return true;
            } else
            {
                lblBaseDatos.Content = "Base de datos no elegida";
                return false;
            }
        }

        private void MostrarBasesDatos()
        {
            cmbBaseDatos.Items.Clear();
            List<string> nombres_bbdd = Operacion.ObtenerBasesDatos(conexionActual);
            nombres_bbdd.Insert(0, CMB_BASEDATOS_DEFECTO);
            Rellena.ComboBox(cmbBaseDatos, nombres_bbdd);
            cmbBaseDatos.SelectedIndex = 0;
        }

        /*
         * Botones DDL
         */
        private void CreateDB()
        {
            // Debo pasar: descripcion, conexión actual, comando.
            DbCommand comando = Operacion.ComandoCreateDatabase(conexionActual);
            VCreateDatabase vog = 
                new VCreateDatabase(conexionActual, comando);
            vog.ShowDialog();
        }

        private void DropDB()
        {
            DbCommand comando = Operacion.ComandoDropDatabase(conexionActual);
            //VCreateDatabase vog = 
            //    new VCreateDatabase(DESCRIPCION_DROPDB, conexionActual, comando);
            //vog.ShowDialog();
            Utils.Consola.NoImplementado();
        }

        private void ShowDBs()
        {
            Utils.Consola.NoImplementado();
        }

        private void CreateTable()
        {
            Utils.Consola.NoImplementado();
        }

        private void DropTable()
        {
            DbCommand comando = Operacion.ComandoDropDatabase(conexionActual);
            //VCreateDatabase vog = 
            //    new VCreateDatabase(DESCRIPCION_DROPTABLE, conexionActual, comando);
            //vog.ShowDialog();
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
    }
}
