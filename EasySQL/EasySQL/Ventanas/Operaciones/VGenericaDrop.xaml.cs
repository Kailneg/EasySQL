using EasySQL.Modelos;
using EasySQL.Operaciones.Ayudante;
using EasySQL.Operaciones.Controlador;
using EasySQL.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
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

namespace EasySQL.Ventanas.Operaciones
{
    /// <summary>
    /// Lógica de interacción para VOperacionGenerica.xaml
    /// </summary>
    public partial class VGenericaDrop : Window
    {
        private const string DESCRIPCION_DATABASE = "Elige nombre de la BBDD a eliminar:";
        private const string DESCRIPCION_TABLE = "Elige nombre de la tabla a eliminar:";
        private const string CLICK_ERROR_DATABASE = "Debes elegir una base de datos";
        private const string CLICK_ERROR_TABLE = "Debes elegir una tabla";
        private const string CLICK_OK_DATABASE = "Base de datos ";
        private const string CLICK_OK_TABLE = "Tabla ";
        private static string DESCRIPCION;
        private static string CLICK_ERROR;
        private static string CLICK_OK;
        private Conexion conexionActual;
        private DbCommand comandoEnviar;
        private string textoComandoOriginal;
        private enum Modo { DROP_DATABASE, DROP_TABLE };
        private Modo modoActual;

        public VGenericaDrop(Conexion actual, DbCommand comando)
        {
            InitializeComponent();
            lblComando.Content = comando.CommandText;
            this.conexionActual = actual;
            this.comandoEnviar = comando;
            this.textoComandoOriginal = comando.CommandText;
            this.Title = textoComandoOriginal;
            if (textoComandoOriginal.Contains("DATABASE"))
                modoActual = Modo.DROP_DATABASE;
            else if (textoComandoOriginal.Contains("TABLE"))
                modoActual = Modo.DROP_TABLE;
            cambiarModo();
            lblDescripcion.Content = DESCRIPCION;
        }

        private void cambiarModo()
        {
            if (modoActual.Equals(Modo.DROP_DATABASE))
            {
                CLICK_ERROR = CLICK_ERROR_DATABASE;
                CLICK_OK = CLICK_OK_DATABASE;
                DESCRIPCION = DESCRIPCION_DATABASE;
                Comun.RellenarComboBasesDatos(conexionActual, cmbDatos);
            }
            else if (modoActual.Equals(Modo.DROP_TABLE))
            {
                CLICK_ERROR = CLICK_ERROR_TABLE;
                CLICK_OK = CLICK_OK_TABLE;
                DESCRIPCION = DESCRIPCION_TABLE;
                Comun.RellenarComboTablas(conexionActual, cmbDatos);
            }
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            if (!Comun.ElegidaTablaDefecto(cmbDatos) && !Comun.ElegidaBaseDatosDefecto(cmbDatos))
            {
                comandoEnviar.CommandText = textoComandoOriginal + cmbDatos.SelectedItem;
                int resultado = Ayudante.ExecuteNonQuery(conexionActual, comandoEnviar);
                if (resultado == -1)
                {
                    MessageBox.Show(CLICK_OK + "\"" + cmbDatos.SelectedItem + "\"" + " eliminada con con éxito.");
                }
            }
            else
            {
                MessageBox.Show(CLICK_ERROR);
            }
        }

        private void cmbDatos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox datos = (ComboBox)sender;

            // Comprobar que la base de datos no sea la por defecto, actualizar label
            if (!Comun.ElegidaTablaDefecto(datos) && !Comun.ElegidaBaseDatosDefecto(datos))
            {
                lblComando.Content = textoComandoOriginal + cmbDatos.SelectedItem;
            }
            else
            {
                lblComando.Content = textoComandoOriginal;
            }
        }

        private void cmbDatos_DropDownOpened(object sender, EventArgs e)
        {
            ComboBox aRellenar = (ComboBox)sender;
            // Dependiendo del modo actual, el combo se rellenará con tablas o BBDD
            if (modoActual.Equals(Modo.DROP_DATABASE))
            {
                Comun.RellenarComboBasesDatos(conexionActual, aRellenar);
            }
            else if (modoActual.Equals(Modo.DROP_TABLE))
            {
                Comun.RellenarComboTablas(conexionActual, aRellenar);
            }
        }
    }
}
