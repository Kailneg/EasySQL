using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private string registrarQuery, existirQuery, loginQuery, obtenerIDQuery, obtenerConexionesQuery;

        private BBDDProgramaImpl()
        {
            // 1. Crea la cadena de conexión
            cadenaConexion =
                "Data Source=localhost\\SQLALE;"
                + "Initial Catalog=usuarios;"
                + "Integrated Security=True";

            loginQuery = "SELECT nombre FROM Usuario WHERE nombre =@usuario AND contrasenia =@contrasenia";
            registrarQuery = "INSERT INTO Usuario (nombre, contrasenia) VALUES (@usuario,@contrasenia)";
            existirQuery = "SELECT nombre FROM Usuario WHERE nombre =@usuario";
            obtenerIDQuery = "SELECT id_usuario FROM Usuario WHERE nombre=@usuario AND contrasenia=@contrasenia";
            obtenerConexionesQuery = "SELECT id_conexion, id_tipo_conexion, nombre, direccion, puerto, usuario, contrasenia " +
                                        "FROM conexion WHERE id_usuario = @id_usuario";
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
                    SqlCommand registCommand = new SqlCommand(registrarQuery, sqlCon);
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

        public int ObtenerIDUsuario(Usuario usuario)
        {
            // SELECT id_usuario FROM usuario
            // WHERE nombre = AND contrasenia =
            object resultado = null;
            using (sqlCon = new SqlConnection(cadenaConexion))
            {
                // 1. Crea el comando
                SqlCommand command = new SqlCommand(obtenerIDQuery, sqlCon);
                command.Parameters.AddWithValue("@usuario", usuario.Nombre);
                command.Parameters.AddWithValue("@contrasenia", usuario.Contrasenia);
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
                    return -1;
                }
            }
            // Si el resultado es nulo, no existe el usuario.
            if (resultado == null)
            {
                return -1;
            }
            else
            {
                return (int) resultado;
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
                SqlCommand command = new SqlCommand(existirQuery, sqlCon);
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

        public ObservableCollection<Conexion> ObtenerConexionesUsuario(Usuario usuario)
        {
            /*
             * SELECT id_conexion, id_tipo_conexion, c.nombre, direccion, puerto, usuario, c.contrasenia 
             *  FROM conexion as c INNER JOIN usuario on c.id_usuario = usuario.id_usuario
             */
            SqlDataReader lector = null;
            using (sqlCon = new SqlConnection(cadenaConexion))
            {
                // 1. Crea el comando
                SqlCommand command = new SqlCommand(obtenerConexionesQuery, sqlCon);
                command.Parameters.AddWithValue("@id_usuario", usuario.ID);
                try
                {
                    // 2. Abre the la conexión
                    sqlCon.Open();
                    // 3. Ejecuta y devuelve un objeto resultado
                    lector = command.ExecuteReader();

                    // Si el lector no es nulo, parsear las conexiones
                    if (lector != null)
                    {
                        return new ObservableCollection<Conexion>(BBDDProgramaMapeo.ExtraerConexiones(lector, usuario));
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (SqlException s)
                {
                    Console.WriteLine(s);
                    return null;
                }
            }
            
        }
    }
}
