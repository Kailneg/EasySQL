using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EasySQL.BBDD.ResultadoRegistro;

namespace EasySQL.BBDD
{
    public class BBDDProgramaImpl
    {
        private string cadenaConexion;
        private SqlConnection sqlCon;
        private string registQuery, existQuery, loginQuery;

        private BBDDProgramaImpl()
        {
            // 1. Crea la cadena de conexión
            cadenaConexion =
                "Data Source=localhost\\SQLALE;"
                + "Initial Catalog=usuarios;"
                + "Integrated Security=True";

            registQuery = "INSERT INTO Usuario (nombre, contrasenia) VALUES (@usuario,@contrasenia)";
            
        }

        private static BBDDProgramaImpl instancia;
        public static BBDDProgramaImpl ObtenerInstancia()
        {
            if (instancia == null)
            {
                instancia = new BBDDProgramaImpl();
            }
            return instancia;
        }

        public ResultadoLogin LoginUsuario(string usuario, string contrasenia)
        {
            throw new NotImplementedException();
        }

        public ResultadoRegistro RegistrarUsuario(string usuario, string contrasenia)
        {
            if (ExisteUsuario(usuario))
            {
                return new ResultadoRegistro(TipoResultado.DUPLICADO, null);
            }
            else
            {
                int resultadoFilasSQL = 0;

                using (sqlCon = new SqlConnection(cadenaConexion))
                {
                    // 1. Crea el comando
                    SqlCommand registCommand = new SqlCommand(registQuery, sqlCon);
                    registCommand.Parameters.AddWithValue("@usuario", usuario);
                    registCommand.Parameters.AddWithValue("@contrasenia", contrasenia);
                    try
                    {
                        // 2. Abre the la conexión
                        sqlCon.Open();
                        // 3. Ejecuta y devuelve el número de filas afectadas
                        resultadoFilasSQL = registCommand.ExecuteNonQuery();
                    }
                    catch (SqlException s)
                    {
                        Console.WriteLine(s);
                    }
                }
                // Si es distinto a 0, se habrá registrado el usuario
                if (resultadoFilasSQL != 0)
                {
                    return new ResultadoRegistro(TipoResultado.DUPLICADO, 
                        new Modelos.Usuario(usuario, contrasenia));
                }
                else
                {
                    return new ResultadoRegistro(TipoResultado.ERROR_CONEXION, null);
                }
             }
            
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
    }
}
