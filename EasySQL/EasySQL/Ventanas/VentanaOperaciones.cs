using EasySQL.Modelos;
using EasySQL.Ventanas.Operaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EasySQL.Ventanas
{
    public partial class VentanaOperaciones : Window
    {
        private Conexion datosConexion;
        /*
         * Botones General
         */
        private void Atras()
        {
            this.Close();
            VentanaConexion vc = new VentanaConexion(null);
        }

        private void Cargar()
        {
            Utils.Consola.NoImplementado();
        }

        private void Guardar()
        {
            Utils.Consola.NoImplementado();
        }

        /*
         * Botones DDL
         */
        private void CreateDB()
        {
            VOperacionGenerica vog = new VOperacionGenerica("Introduce nombre:");
            vog.ShowDialog();
        }

        private void DropDB()
        {
            Utils.Consola.NoImplementado();
        }

        private void ShowDBs()
        {
            Utils.Consola.NoImplementado();
        }

        private void CreateTable()
        {
            Utils.Consola.NoImplementado();
        }

        private void DropTable()
        {

            Utils.Consola.NoImplementado();
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
    }
}
