using EasySQL.Modelos;
using EasySQL.Operaciones.Ayudante;
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
    /// Lógica de interacción para VOperacionGenerica.xaml
    /// </summary>
    public partial class VGenericaDrop : Window
    {
        private static readonly string CMB_BASEDATOS_DEFECTO = "Elige base de datos...";
        private Conexion conexionActual;
        private DbCommand comandoEnviar;
        private string textoComandoOriginal;

        public VGenericaDrop(string descripcion, Conexion actual, DbCommand comando)
        {
            InitializeComponent();
            lbl.Content = descripcion;
            lblComando.Content = comando.CommandText;
            this.conexionActual = actual;
            this.comandoEnviar = comando;
            this.textoComandoOriginal = comando.CommandText;
            this.Title = textoComandoOriginal;
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            //if (txtbox.Text.Length > 0)
            //{
            //    if (Comprueba.ContieneSeparadorSQL(txtbox.Text))
            //    {
            //        MessageBox.Show("Detectado separador SQL. Sólo se ejecutará: " + comandoEnviar.CommandText);
            //    }
            //    comandoEnviar.CommandText = textoComandoOriginal + Comprueba.EliminarResto(txtbox.Text);

            //    int resultado = Ayudante.ExecuteNonQuery(actual, comandoEnviar);
            //    if (resultado == -1)
            //    {
            //        MessageBox.Show("Base de datos " + Comprueba.EliminarResto(txtbox.Text) + " creada con éxito.");
            //    }
            //} else
            //{
            //    MessageBox.Show("Debes introducir un nombre");
            //}
        }

        private void cmbDatos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Comprobar que la base de datos no sea la por defecto
            if (cmbDatos.SelectedItem != null
                && !cmbDatos.SelectedItem.Equals(CMB_BASEDATOS_DEFECTO))
            {
                // Actualiza label descriptivo
                lblComando.Content = textoComandoOriginal + cmbDatos.SelectedItem;
                // Asigna nombre
                conexionActual.BaseDatos = cmbDatos.SelectedItem.ToString();
                //return true;
            }
            else
            {
                lblComando.Content = textoComandoOriginal;
                //return false;
            }
        }

        private void cmbDatos_DropDownOpened(object sender, EventArgs e)
        {
            // Mostrar bases de datos
            cmbDatos.Items.Clear();
            List<string> nombres_bbdd = Operacion.ObtenerBasesDatos(conexionActual);
            nombres_bbdd.Insert(0, CMB_BASEDATOS_DEFECTO);
            Rellena.ComboBox(cmbDatos, nombres_bbdd);
            cmbDatos.SelectedIndex = 0;
        }
    }
}
