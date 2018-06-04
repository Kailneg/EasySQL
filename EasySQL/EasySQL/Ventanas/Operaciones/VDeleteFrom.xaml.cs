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
    /// Interaction logic for VDeleteFrom.xaml
    /// </summary>
    public partial class VDeleteFrom : Window
    {
        private const string CMB_OPCION_DEFECTO = "Elige tabla...";
        private Conexion conexionActual;
        private DbCommand comandoEnviar;
        private TextBox txtBoxGenerado;
        private ComboBox cmbGenerado;
        private string textoComandoOriginal;

        /// <summary>
        /// Constructor para pruebas
        /// </summary>
        public VDeleteFrom() :
            this(
                    new Conexion()
                    {
                        Direccion = "localhost\\SQLALE",
                        TipoActual = Conexion.TipoConexion.MicrosoftSQL,
                        UsuarioConexion = Usuario.NombreIntegratedSecurity,
                        BaseDatos = "pruebas"
                    }
                )
        { }

        public VDeleteFrom(Conexion actual)
        {
            InitializeComponent();
            this.conexionActual = actual;
            this.Title += " || Base de datos \"" + actual.BaseDatos + "\"";
            // Obtiene el comando SQL correspondiente
            this.comandoEnviar = Operacion.ComandoDeleteFrom(actual);
            lblComando.Content = comandoEnviar.CommandText;
            this.textoComandoOriginal = comandoEnviar.CommandText;

            // Agrega y muestra la opción por defecto en el combobox.
            cmbTablas.Items.Add(CMB_OPCION_DEFECTO);
            cmbTablas.SelectedIndex = 0;
        }

        private void DatosCambiados()
        {
            // Si no, resetea el label
            comandoEnviar.CommandText = textoComandoOriginal + cmbTablas.SelectedItem.ToString();
            lblComando.Content = comandoEnviar.CommandText;
        }

        private void cmbTabla_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Comprobar que la tabla no sea la por defecto
            if (!Comprueba.ElegidaOpcionDefecto(cmbTablas, CMB_OPCION_DEFECTO))
            {
                DatosCambiados();
            }
            else
            {
                // Si no, resetea el label
                lblComando.Content = textoComandoOriginal;
            }
        }

        private void cmbTabla_DropDownOpened(object sender, EventArgs e)
        {
            List<string> nombresTablas = new List<string>();

            // Ejecuta un reader para obtener las o tablas pertinentes

            // Obtener comando tables, reemplazar el parametro con el nombre de la bbdd actual
            DbCommand comando = Operacion.ComandoShowTables(conexionActual);
            comando.CommandText = comando.CommandText.Replace(Operacion.PARAM, conexionActual.BaseDatos);

            using (IDataReader lector = Ayudante.ExecuteReader(conexionActual, comando))
            {
                // Si el resultado es nulo, no existen bases de datos.
                if (lector != null)
                {
                    nombresTablas = Ayudante.MapearReaderALista(lector);
                }
            }
            // Rellena el combobox con las bases de datos o tablas pertinentes
            nombresTablas.Insert(0, CMB_OPCION_DEFECTO);
            cmbTablas.Items.Clear();
            Rellena.ComboBox(cmbTablas, nombresTablas);
            cmbTablas.SelectedIndex = 0;
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            int resultado = Ayudante.ExecuteNonQuery(conexionActual, comandoEnviar);
            if (resultado == -1)
            {
                MessageBox.Show(
                    "Datos de la tabla \"" + 
                    Comprueba.EliminarResto(cmbTablas.SelectedItem.ToString()) +
                    "\" en base de datos " + "\"" + conexionActual.BaseDatos + 
                    "\" modificada con éxito.");
            }
        }
    }
}
