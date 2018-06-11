using EasySQL.Modelos;
using EasySQL.Operaciones;
using EasySQL.Operaciones.Ayudante;
using EasySQL.Operaciones.Controlador;
using EasySQL.Utils;
using EasySQL.Ventanas.Operaciones;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace EasySQL.Ventanas
{
    public partial class VentanaOperaciones : Window
    {
        /// <summary>
        /// Conexión activa del momento.
        /// </summary>
        private Conexion conexionActual;

        /// <summary>
        /// Mensaje de aviso al usuario cuando se intenta ejecutar una operación
        /// que requiera el uso de una base de datos elegida y no se encuentre.
        /// </summary>
        private const string MSJ_ELEGIR_BBDD = "Debes elegir antes una base de datos.";

        /// <summary>
        /// Opción por defecto en el ComboBox de elección de base de datos.
        /// </summary>
        private const string CMB_BASEDATOS_DEFECTO = "Elige base de datos...";

        /// <summary>
        /// Actualiza el UI con los datos de la conexión.
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
        }
        
        /// <summary>
        /// Comprueba si hay una base de datos seleccionada, la asigna a
        /// la conexión actual y actualiza el label relacionado.
        /// </summary>
        /// <returns>True si se ha asignado a una base de datos existente.
        /// False si estaba elegida la opción por defecto.</returns>
        private bool SeleccionBBDDCambiada()
        {
            // Comprobar que la base de datos no sea la por defecto
            if (HayBBDDSeleccionada()) {
                conexionActual.BaseDatos = cmbBaseDatos.SelectedItem.ToString();
                lblBaseDatos.Content = "Base de datos : " + cmbBaseDatos.SelectedItem;
                return true;
            } else
            {
                // libera BBDD
                conexionActual.BaseDatos = null;
                lblBaseDatos.Content = "Base de datos no elegida";
                return false;
            }
        }

        /// <summary>
        /// Rellena el ComboBox de base de datos con los nombres de las 
        /// bases de datos de la conexión actual.
        /// </summary>
        private void MostrarBasesDatos()
        {
            List<string> nombres_bbdd = 
                Ayudante.MapearReaderALista(Ayudante.ObtenerReaderBasesDatos(conexionActual));  
            nombres_bbdd.Insert(0, CMB_BASEDATOS_DEFECTO);
            cmbBaseDatos.Items.Clear();
            Rellena.ComboBox(cmbBaseDatos, nombres_bbdd);
            cmbBaseDatos.SelectedIndex = 0;
        }

        /*
         * /// Botones DDL
         */

        /// <summary>
        /// Abre una ventana CREATE DATABASE de manera modal,
        /// pasando la conexión actual como parámetro.
        /// </summary>
        private void CreateDB()
        {
            VCreateDatabase vog = new VCreateDatabase(conexionActual);
            vog.ShowDialog();
        }

        /// <summary>
        /// Comprueba si hay una BBDD seleccionada. Si la hay, la deselecciona.
        /// Abre una ventana DROP DATABASE de manera modal,
        /// pasando la conexión actual como parámetro.
        /// </summary>
        private void DropDB()
        {
            if (HayBBDDSeleccionada())
            {
                // Quita la selección de la BBDD actual
                cmbBaseDatos.SelectedIndex = 0;
                SeleccionBBDDCambiada();
                MessageBox.Show("Deseleccionada BBDD elegida");
            }
            DbCommand comando = Operacion.ComandoDropDatabase(conexionActual, false);
            VGenericaDrop vod =
                new VGenericaDrop(conexionActual, VGenericaDrop.Modo.DATABASE);
            vod.ShowDialog();
        }

        /// <summary>
        /// Comprueba si hay una BBDD seleccionada. 
        /// Si no la hay, pide al usuario seleccionar una.
        /// Luego abre una ventana CREATE TABLE de manera modal,
        /// pasando la conexión actual como parámetro.
        /// </summary>
        private void CreateTable()
        {
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

        /// <summary>
        /// Comprueba si hay una BBDD seleccionada. 
        /// Si no la hay, pide al usuario seleccionar una.
        /// Luego abre una ventana DROP TABLE de manera modal,
        /// pasando la conexión actual como parámetro.
        /// </summary>
        private void DropTable()
        {
            if (HayBBDDSeleccionada())
            {
                DbCommand comando = Operacion.ComandoDropTable(conexionActual);
                VGenericaDrop vod =
                    new VGenericaDrop(conexionActual, VGenericaDrop.Modo.TABLE);
                vod.ShowDialog();
            }
            else
            {
                MessageBox.Show(MSJ_ELEGIR_BBDD);
            }
        }

        /// <summary>
        /// Comprueba si hay una BBDD seleccionada. 
        /// Si no la hay, pide al usuario seleccionar una.
        /// Luego abre una ventana ALTER TABLE de manera modal,
        /// pasando la conexión actual como parámetro.
        /// </summary>
        private void AlterTable()
        {
            if (HayBBDDSeleccionada())
            {
                VAlterTable vat = new VAlterTable(conexionActual);
                vat.ShowDialog();
            }
            else
            {
                MessageBox.Show(MSJ_ELEGIR_BBDD);
            }
        }

        private void ShowTables()
        {
            // Antes comprobar si existe una BBDD seleccionada
            if (HayBBDDSeleccionada())
            {
                VShowTables vst = new VShowTables(conexionActual);
                vst.ShowDialog();
            }
            else
            {
                MessageBox.Show(MSJ_ELEGIR_BBDD);
            }
        }

        /*
         * /// Botones DML
         */

        /// <summary>
        /// Comprueba si hay una BBDD seleccionada. 
        /// Si no la hay, pide al usuario seleccionar una.
        /// Luego abre una ventana SELECT de manera modal,
        /// pasando la conexión actual como parámetro.
        /// </summary>
        private void Select()
        {
            // Antes comprobar si existe una BBDD seleccionada
            if (HayBBDDSeleccionada())
            {
                VSelect vs = new VSelect(conexionActual);
                vs.ShowDialog();
            }
            else
            {
                MessageBox.Show(MSJ_ELEGIR_BBDD);
            }
        }

        /// <summary>
        /// Comprueba si hay una BBDD seleccionada. 
        /// Si no la hay, pide al usuario seleccionar una.
        /// Luego abre una ventana INSERT de manera modal,
        /// pasando la conexión actual como parámetro.
        /// </summary>
        private void Insert()
        {
            // Antes comprobar si existe una BBDD seleccionada
            if (HayBBDDSeleccionada())
            {
                VInsert vi = new VInsert(conexionActual);
                vi.ShowDialog();
            }
            else
            {
                MessageBox.Show(MSJ_ELEGIR_BBDD);
            }
        }

        /// <summary>
        /// Comprueba si hay una BBDD seleccionada. 
        /// Si no la hay, pide al usuario seleccionar una.
        /// Luego abre una ventana UPDATE de manera modal,
        /// pasando la conexión actual como parámetro.
        /// </summary>
        private void Update()
        {
            // Antes comprobar si existe una BBDD seleccionada
            if (HayBBDDSeleccionada())
            {
                VUpdate vu = new VUpdate(conexionActual);
                vu.ShowDialog();
            }
            else
            {
                MessageBox.Show(MSJ_ELEGIR_BBDD);
            }
        }

        /// <summary>
        /// Comprueba si hay una BBDD seleccionada. 
        /// Si no la hay, pide al usuario seleccionar una.
        /// Luego abre una ventana DELETE de manera modal,
        /// pasando la conexión actual como parámetro.
        /// </summary>
        private void Delete()
        {
            // Antes comprobar si existe una BBDD seleccionada
            if (HayBBDDSeleccionada())
            {
                VDeleteFrom vdf = new VDeleteFrom(conexionActual);
                vdf.ShowDialog();
            }
            else
            {
                MessageBox.Show(MSJ_ELEGIR_BBDD);
            }
        }
        

        /*
         * Botones General
         */
        /// <summary>
        /// Cambia la ventana actual por ventana Conexion, 
        /// pasando la conexión actual por parámetros.
        /// </summary>
        private void Atras()
        {
            VentanaConexion vc = new VentanaConexion(conexionActual.Propietario, conexionActual);
            Manejador.CambiarVentana(this, vc);
        }

        /// <summary>
        /// Intenta cargar datos de un fichero de una consulta SELECT previamente creado.
        /// Si lo consigue, muestra metadatos del archivo por medio de una ventana de información,
        /// luego muestra los datos del fichero en una ventana Mostrar Datos no modal.
        /// En caso contrario, muestra un mensaje de aviso al usuario.
        /// </summary>
        private void Cargar()
        {
            DatosConsulta datosCargados = Serializador.Cargar();
            if (datosCargados != null)
            {
                var c = datosCargados.Conexion;
                // Mostrar información del archivo
                string informacion =
                    "Base de datos \"" + c.BaseDatos + "\" tipo \"" + c.TipoActual +
                    "\"\r\nFecha consulta: " + datosCargados.FechaCreacion;
                MessageBox.Show(informacion, "Recuperando consulta",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                VMostrarDatos vmd = new VMostrarDatos(datosCargados);
                vmd.Show();
            }
            else
            {
                MessageBox.Show("No se ha podido cargar el fichero seleccionado", "Aviso",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        /// <summary>
        /// Abre una conexión Web a una reconocida página de información sobre comandos SQL,
        /// previa petición de permiso al usuario.
        /// </summary>
        private void Ayuda()
        {
            MessageBoxResult opcionElegir =
                MessageBox.Show("Se abrirá una conexión a la web W3SCHOOLS\r\n"
                + "¿Desea permitirlo?", "Información", MessageBoxButton.YesNo,
                MessageBoxImage.Information);

            if (opcionElegir.Equals(MessageBoxResult.Yes))
            {
                try
                {
                    Process.Start("https://www.w3schools.com/sql/default.asp");
                }
                catch (Exception)
                {
                    MessageBox.Show("Navegador por defecto no encontrado", "Aviso",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        /// <summary>
        /// Comprueba si la BBDD elegida en el ComboBox de selección de base de datos
        /// no es la opción por defecto.
        /// </summary>
        /// <returns>True si la BBDD no es la opción por defecto.</returns>
        private bool HayBBDDSeleccionada()
        {
            return cmbBaseDatos.SelectedItem != null
                && !cmbBaseDatos.SelectedItem.Equals(CMB_BASEDATOS_DEFECTO);
        }
    }
}
