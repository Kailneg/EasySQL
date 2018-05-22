using EasySQL.Modelos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySQL.BBDD
{
    public class BBDDPrograma
    {
        private string cadenaConexion;
        private SqlConnection sqlCon;

        public BBDDPrograma()
        {
            // 1. Crea la cadena de conexión
            cadenaConexion =
                "Data Source=localhost\\SQLALE;"
                + "Initial Catalog=usuarios;"
                + "Integrated Security=True";
        }

        public bool RegistrarUsuario(string usuario, string contrasenia)
        {
            if (!ExisteUsuario(usuario))
            {
                int resultadoFilasSQL = 0;

                using (sqlCon = new SqlConnection(cadenaConexion))
                {
                    // 1. Crea el comando
                    string query = "INSERT INTO Usuario (nombre, contrasenia) VALUES (@usuario,@contrasenia)";
                    SqlCommand command = new SqlCommand(query, sqlCon);
                    command.Parameters.AddWithValue("@usuario", usuario);
                    command.Parameters.AddWithValue("@contrasenia", contrasenia);

                    try
                    {
                        // 2. Abre the la conexión
                        sqlCon.Open();
                        // 3. Ejecuta y devuelve el número de filas afectadas
                        resultadoFilasSQL = command.ExecuteNonQuery();
                    }
                    catch (SqlException s)
                    {
                        Console.WriteLine(s);
                    }
                }
                // Si es distinto a 0, se habrá registrado el usuario
                return (resultadoFilasSQL != 0);
            }
            return false;
        }

        private bool ExisteUsuario(string usuario)
        {
            object resultado = null;
            using (sqlCon = new SqlConnection(cadenaConexion))
            {
                // 1. Crea el comando
                string query = "SELECT nombre FROM Usuario WHERE nombre =@usuario";
                SqlCommand command = new SqlCommand(query, sqlCon);
                command.Parameters.AddWithValue("@usuario", usuario);

                try
                {
                    // 2. Abre the la conexión
                    sqlCon.Open();
                    // 3. Ejecuta y devuelve un objeto resultado
                    resultado = command.ExecuteScalar();
                }
                catch (SqlException s)
                {
                    Console.WriteLine(s);
                }
            }
            // Si el resultado es nulo, no existe el usuario.
            return (resultado != null);
        }

        private static BBDDPrograma instancia;
        public static BBDDPrograma ObtenerInstancia()
        {
            if (instancia == null)
            {
                instancia = new BBDDPrograma();
            }
            return instancia;
        }
    }
}
