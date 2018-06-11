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
    /// Interaction logic for VShowTables.xaml
    /// </summary>
    public partial class VShowTables : Window
    {
        private Conexion conexionActual;

        public VShowTables() :
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

        public VShowTables(Conexion actual)
        {
            InitializeComponent();
            this.conexionActual = actual;
            this.Title += " || Base de datos \"" + actual.BaseDatos + "\"";
            lblDescripcion.Content += " \"" + actual.BaseDatos + "\"";
            // Obtiene el comando SQL correspondiente
            Comun.GenerarShowTables(stackTablas, actual, Button_Click);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Saco la información del textbox de su izquierda
            string nombreTabla = (((sender as Button).Parent as Grid).Children[1] as TextBox).Text;
            DbCommand comando = Operacion.ComandoSelect(conexionActual);
            string txtComando = comando.CommandText;
            txtComando = txtComando.Replace(Operacion.PARAMS[0], "*");
            txtComando = txtComando.Replace(Operacion.PARAMS[1], nombreTabla);
            txtComando = txtComando.Replace(Operacion.PARAMS[2], "");
            txtComando = txtComando.Replace(Operacion.PARAMS[3], "");
            comando.CommandText = txtComando;


            object comprobarComando = Ayudante.ExecuteScalar(conexionActual, comando);
            if (comprobarComando != null)
            {
                int resultado = 0;
                Int32.TryParse(comprobarComando.ToString(), out resultado);
                if (resultado != Ayudante.ERROR)
                {
                    // Al menos hay una fila que mostrar
                    IDataReader readerSelect = Ayudante.ExecuteReader(conexionActual, comando);
                    DataTable datosMostrar = new DataTable();
                    datosMostrar.Load(readerSelect);
                    DatosConsulta paqueteDatos = new DatosConsulta(conexionActual, datosMostrar, comando.CommandText);
                    VMostrarDatos vmd = new VMostrarDatos(paqueteDatos);
                    vmd.Show();
                }
            }
            else
            {
                MessageBox.Show("Ninguna fila encontrada.");
            }
        }
    }
}
