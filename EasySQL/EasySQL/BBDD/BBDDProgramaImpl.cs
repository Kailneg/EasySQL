using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasySQL.Modelos;

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

            loginQuery = "SELECT nombre FROM Usuario WHERE nombre =@usuario AND contrasenia =@contrasenia";
            registQuery = "INSERT INTO Usuario (nombre, contrasenia) VALUES (@usuario,@contrasenia)";
            existQuery = "SELECT nombre FROM Usuario WHERE nombre =@usuario";
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
            object resultado = null;
            using (sqlCon = new SqlConnection(cadenaConexion))
            {
                // 1. Crea el comando
                SqlCommand command = new SqlCommand(loginQuery, sqlCon);
                command.Parameters.AddWithValue("@usuario", usuario);
                command.Parameters.AddWithValue("@contrasenia", contrasenia);
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
                    return new ResultadoLogin(ResultadoLogin.TipoResultado.ERROR, null);
                }
            }
            // Si el resultado es nulo, no existe el usuario.
            if (resultado == null)
            {
                return new ResultadoLogin(ResultadoLogin.TipoResultado.DENEGADO, null);
            }
            else
            {
                return new ResultadoLogin(ResultadoLogin.TipoResultado.ACEPTADO,
                    new Usuario(usuario, contrasenia));
            }
        }

        public ResultadoRegistro RegistrarUsuario(string usuario, string contrasenia)
        {
            if (ExisteUsuario(usuario))
            {
                return new ResultadoRegistro(ResultadoRegistro.TipoResultado.DUPLICADO, null);
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
                    return new ResultadoRegistro(ResultadoRegistro.TipoResultado.ACEPTADO, 
                        new Usuario(usuario, contrasenia));
                }
                else
                {
                    return new ResultadoRegistro(ResultadoRegistro.TipoResultado.ERROR_CONEXION, null);
                }
             }
            
        }

        public void RegistrarConexion(Conexion guardar)
        {
            // Mirar cada uno de los campos
            // Teniendo en consiederaración el campo Puerto
            // Si el puerto no está relleno, usar el por defecto almacenado en la bbdd

        }

        private int obtenerPuertoDefecto(Conexion.TipoConexion tipo)
        {
            return 0;
        }

        private bool ExisteUsuario(string usuario)
        {
            object resultado = null;
            using (sqlCon = new SqlConnection(cadenaConexion))
            {
                // 1. Crea el comando
                SqlCommand command = new SqlCommand(existQuery, sqlCon);
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

        public List<Conexion> ObtenerConexionesUsuario(Usuario usuario)
        {
            return null;
        }
    }
}
