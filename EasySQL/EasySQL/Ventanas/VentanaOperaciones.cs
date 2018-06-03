using EasySQL.Modelos;
using EasySQL.Operaciones.Ayudante;
using EasySQL.Operaciones.Controlador;
using EasySQL.Utils;
using EasySQL.Ventanas.Operaciones;
using System;
using System.Collections.Generic;
using System.Data;
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
        private const string MSJ_ELEGIR_BBDD = "Debes elegir antes una base de datos.";
        private const string CMB_BASEDATOS_DEFECTO = "Elige base de datos...";

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
            if (HayBBDDSeleccionada()) {
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
            List<string> nombres_bbdd = new List<string>();
            // Obtener comando databases
            DbCommand comando = Operacion.ComandoShowDatabases(conexionActual);

            using (IDataReader lector = Ayudante.ExecuteReader(conexionActual, comando))
            {
                // Si el resultado es nulo, no existen bases de datos.
                if (lector != null)
                {
                    nombres_bbdd = Ayudante.MapearReaderALista(lector);
                }
            }
            nombres_bbdd.Insert(0, CMB_BASEDATOS_DEFECTO);
            cmbBaseDatos.Items.Clear();
            Rellena.ComboBox(cmbBaseDatos, nombres_bbdd);
            cmbBaseDatos.SelectedIndex = 0;
        }

        /*
         * Botones DDL
         */
        private void CreateDB()
        {
            VCreateDatabase vog = new VCreateDatabase(conexionActual);
            vog.ShowDialog();
        }

        private void DropDB()
        {
            // Debo pasar: conexión actual, comando.
            DbCommand comando = Operacion.ComandoDropDatabase(conexionActual);
            VGenericaDrop vod =
                new VGenericaDrop(conexionActual, comando);
            vod.ShowDialog();
        }

        private void ShowDBs()
        {
            Utils.Consola.NoImplementado();
        }

        private void CreateTable()
        {
            // Antes comprobar si existe una BBDD seleccionada
            if (HayBBDDSeleccionada())
            {
                VCreateTable vct = new VCreateTable(conexionActual);
                vct.ShowDialog();
            }
            else
            {
                MessageBox.Show(MSJ_ELEGIR_BBDD);
            }
        }

        private void DropTable()
        {
            // Antes comprobar si existe una BBDD seleccionada
            if (HayBBDDSeleccionada())
            {
                DbCommand comando = Operacion.ComandoDropTable(conexionActual);
                VGenericaDrop vod =
                    new VGenericaDrop(conexionActual, comando);
                vod.ShowDialog();
            }
            else
            {
                MessageBox.Show(MSJ_ELEGIR_BBDD);
            }
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

        private bool HayBBDDSeleccionada()
        {
            return cmbBaseDatos.SelectedItem != null
                && !cmbBaseDatos.SelectedItem.Equals(CMB_BASEDATOS_DEFECTO);
        }
    }
}
