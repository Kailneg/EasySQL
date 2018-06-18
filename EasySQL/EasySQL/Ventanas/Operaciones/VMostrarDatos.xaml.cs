using EasySQL.Modelos;
using EasySQL.Operaciones;
using EasySQL.Operaciones.Operacion;
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
    /// Interaction logic for VMostrarDatos.xaml
    /// </summary>
    public partial class VMostrarDatos : Window
    {
        private DatosConsulta paqueteDatos;

        public VMostrarDatos(DatosConsulta paqueteDatos)
        {
            InitializeComponent();
            this.paqueteDatos = paqueteDatos;
            lblComando.Content = paqueteDatos.ComandoSQL;
            dataGrid.ItemsSource = paqueteDatos.Datos.DefaultView;
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            // Crear datos a guardar
            DatosConsulta datosGuardar = new DatosConsulta(
                paqueteDatos.Conexion, paqueteDatos.Datos, paqueteDatos.ComandoSQL);
            if (Serializador.Guardar(datosGuardar))
            {
                Msj.Info("Datos almacenados correctamente.");
            } else
            {
                Msj.Aviso("Error al guardar. Datos no almacenados.");
            }
        }
    }
}
