using EasySQL.Modelos;
using EasySQL.Operaciones;
using EasySQL.Operaciones.Ayudante;
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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EasySQL.Ventanas.Operaciones
{
    /// <summary>
    /// Interaction logic for VMostrarDatos.xaml
    /// </summary>
    public partial class VMostrarDatos : Window
    {
        private Conexion conexionActual;
        private DbCommand comandoEnviar;
        private DataTable datosMostrar;

        public VMostrarDatos(Conexion actual, DbCommand comandoEnviar)
        {
            InitializeComponent();
            this.conexionActual = actual;

            // Obtiene el comando SQL correspondiente
            this.comandoEnviar = comandoEnviar;
            lblComando.Content = comandoEnviar.CommandText;

            IDataReader datosSelect = Ayudante.ExecuteReader(conexionActual, comandoEnviar);

            datosMostrar = new DataTable();
            datosMostrar.Load(datosSelect);
            dataGrid.ItemsSource = datosMostrar.DefaultView;
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            // Crear datos a guardar
            DatosConsulta datosGuardar = new DatosConsulta(conexionActual, datosMostrar);
            Serializador.Guardar(datosGuardar);
        }
    }
}
