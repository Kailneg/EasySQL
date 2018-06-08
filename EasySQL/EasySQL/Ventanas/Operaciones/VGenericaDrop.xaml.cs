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
        private enum Modo { DATABASE, TABLE };
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
                modoActual = Modo.DATABASE;
            else if (textoComandoOriginal.Contains("TABLE"))
                modoActual = Modo.TABLE;
            cambiarModo();
            lblDescripcion.Content = DESCRIPCION;
        }

        private void cambiarModo()
        {
            if (modoActual.Equals(Modo.DATABASE))
            {
                CLICK_ERROR = CLICK_ERROR_DATABASE;
                CLICK_OK = CLICK_OK_DATABASE;
                DESCRIPCION = DESCRIPCION_DATABASE;
                Comun.RellenarComboBasesDatos(conexionActual, cmbDatos);
            }
            else if (modoActual.Equals(Modo.TABLE))
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
                comandoEnviar.CommandText = GenerarComandoEnvio(false);

                int resultado = Ayudante.ExecuteNonQuery(conexionActual, comandoEnviar);
                // Bien
                if (resultado == -1)
                {
                    MessageBox.Show(CLICK_OK + "\"" + cmbDatos.SelectedItem + "\"" + " eliminada con con éxito.");
                }

                // Si la base de datos no se ha podido borrar por conexión abierta, preguntar forzado
                if (modoActual.Equals(Modo.DATABASE) && resultado == Ayudante.ERROR)
                {
                    MessageBoxResult opcionElegir = 
                        MessageBox.Show("Error al eliminar la base de datos.\r\n" +
                        "¿Desea forzar el borrado?", "Alerta", MessageBoxButton.YesNo,
                        MessageBoxImage.Warning);

                    if (opcionElegir.Equals(MessageBoxResult.Yes))
                    {
                        comandoEnviar.CommandText = GenerarComandoEnvio(true);
                        resultado = Ayudante.ExecuteNonQuery(conexionActual, comandoEnviar);
                        // Bien
                        if (resultado == -1)
                        {
                            MessageBox.Show(CLICK_OK + "\"" + cmbDatos.SelectedItem + "\"" + " eliminada con con éxito.");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show(CLICK_ERROR);
            }
        }

        private string GenerarComandoEnvio(bool forzar)
        {
            string comando = "";
            if (modoActual.Equals(Modo.DATABASE))
            {
                string nombreBBDD = cmbDatos.SelectedItem?.ToString();
                if (forzar)
                {
                    comando = Operacion.ComandoDropDatabase(conexionActual, forzar).CommandText;
                    comando = comando.Replace(Operacion.PARAMS[0], nombreBBDD);
                } else
                {
                    comando = textoComandoOriginal + nombreBBDD;
                }
            }
            else if (modoActual.Equals(Modo.TABLE))
            {
                string nombreTabla = cmbDatos.SelectedItem?.ToString();
                comando = Operacion.ComandoDropTable(conexionActual).CommandText;
                comando += nombreTabla;
            }
            return comando;
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
            if (modoActual.Equals(Modo.DATABASE))
            {
                Comun.RellenarComboBasesDatos(conexionActual, aRellenar);
            }
            else if (modoActual.Equals(Modo.TABLE))
            {
                Comun.RellenarComboTablas(conexionActual, aRellenar);
            }
        }
    }
}
