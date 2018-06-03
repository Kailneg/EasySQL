using EasySQL.Modelos;
using EasySQL.Operaciones.Controlador;
using EasySQL.Utils;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for VCreateTable.xaml
    /// </summary>
    public partial class VCreateTable : Window
    {
        private Conexion conexionActual;
        private DbCommand comandoEnviar;
        private string textoComandoOriginal;

        public VCreateTable() :
            this(
                    new Conexion()
                    {
                        Direccion = "localhost\\SQLALE",
                        TipoActual = Conexion.TipoConexion.MicrosoftSQL,
                        UsuarioConexion = Usuario.NombreIntegratedSecurity,
                        BaseDatos = "Pruebas"
                    }
                )
        { }

        public VCreateTable(Conexion actual)
        {
            InitializeComponent();
            this.conexionActual = actual;
            this.Title += " || Base de datos \"" + actual.BaseDatos + "\"";
            // Obtiene el comando SQL correspondiente
            this.comandoEnviar = Operacion.ComandoCreateTable(actual);
            lblComando.Content = comandoEnviar.CommandText;
            this.textoComandoOriginal = comandoEnviar.CommandText;
        }

        private void txtbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            lblComando.Content = textoComandoOriginal + Comprueba.EliminarResto(txtbox.Text);
        }

        private void btnEjecutar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
