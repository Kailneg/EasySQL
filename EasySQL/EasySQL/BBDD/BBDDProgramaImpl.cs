using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasySQL.Modelos;
using EasySQL.Operaciones.Ayudante;

namespace EasySQL.BBDD
{
    public class BBDDProgramaImpl
    {
        private string cadenaConexion;
        
        private string registrarUsuarioQuery, registrarConexionQuery, existirQuery, loginQuery, saltQuery, obtenerIDUsuarioQuery,
            obtenerIDConexionQuery, obtenerConexionesQuery, eliminarConexionQuery, obtenerPuertoQuery;

        private BBDDProgramaImpl()
        {
            // 1. Crea la cadena de conexión
            cadenaConexion =
                "Data Source=localhost\\SQLALE;"
                + "Initial Catalog=usuarios;"
                + "Integrated Security=True";

            loginQuery = "SELECT nombre FROM Usuario WHERE nombre =@usuario " +
                "AND contrasenia =@contrasenia";
            saltQuery = "SELECT contrasenia_salt FROM Usuario WHERE nombre =@usuario ";
            registrarUsuarioQuery = "INSERT INTO Usuario (nombre, contrasenia, contrasenia_salt) VALUES " +
                "(@usuario,@contrasenia,@contrasenia_salt)";
            registrarConexionQuery = "INSERT INTO Conexion (id_tipo_conexion, id_usuario, nombre, direccion, puerto, usuario, contrasenia)" +
                " VALUES (@tipo_conexion, @id_usuario, @nombre, @direccion, @puerto, @usuario, @contrasenia)";
            existirQuery = "SELECT nombre FROM Usuario WHERE nombre =@usuario";
            obtenerIDUsuarioQuery = "SELECT id_usuario FROM Usuario WHERE nombre=@usuario AND contrasenia=@contrasenia";
            obtenerIDConexionQuery = "SELECT id_conexion FROM Conexion WHERE nombre=@nombredir " + 
                                        "AND direccion=@direccion AND usuario=@usuario";
            obtenerConexionesQuery = "SELECT id_conexion, id_tipo_conexion, nombre, direccion, puerto, usuario, contrasenia " + 
                "FROM conexion WHERE id_usuario = @id_usuario";
            eliminarConexionQuery = "DELETE FROM Conexion WHERE id_conexion = @id_conexion";
            obtenerPuertoQuery = "SELECT puerto_defecto FROM tipo_conexion WHERE id_tipo = @id_tipo";
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
            string contraseniaSalt = ObtenerSal(usuario);
            string contraseniaBcrypt = "";
            if (!String.IsNullOrWhiteSpace(contraseniaSalt))
                contraseniaBcrypt = BCrypt.HashPassword(contrasenia, contraseniaSalt);
            SqlCommand loginCmd = new SqlCommand(loginQuery);
            loginCmd.Parameters.AddWithValue("@usuario", usuario);
            loginCmd.Parameters.AddWithValue("@contrasenia", contraseniaBcrypt);

            // Obtiene el resultado
            object resultado = AyudanteSQL.ExecuteScalar(cadenaConexion, loginCmd);
            
            // Si el resultado es nulo, no existe el usuario.
            if (resultado == null)
            {
                return new ResultadoLogin(ResultadoLogin.TipoResultado.DENEGADO, null);
            }
            else if (resultado.Equals(Ayudante.ERROR))
            {
                return new ResultadoLogin(ResultadoLogin.TipoResultado.ERROR, null);
            }
            {
                return new ResultadoLogin(ResultadoLogin.TipoResultado.ACEPTADO,
                    new Usuario(usuario, contrasenia));
            }
        }

        public string ObtenerSal(string usuario)
        {
            SqlCommand saltCmd = new SqlCommand(saltQuery);
            saltCmd.Parameters.AddWithValue("@usuario", usuario);
            // Obtiene el resultado
            object resultado = AyudanteSQL.ExecuteScalar(cadenaConexion, saltCmd);
            if (resultado != null)
                return resultado.ToString();
            else
                return "";
        }

        public ResultadoRegistro RegistrarUsuario(string usuario, string contrasenia)
        {
            if (ExisteUsuario(usuario))
            {
                return new ResultadoRegistro(ResultadoRegistro.TipoResultado.DUPLICADO, null);
            }
            else
            {
                // Generar sal
                string contraseniaSalt = BCrypt.GenerateSalt();
                //mySalt == "$2a$10$rBV2JDeWW3.vKyeQcM8fFO"
                string contraseniaBcrypt = BCrypt.HashPassword(contrasenia, contraseniaSalt);
                //myHash == "$2a$10$rBV2JDeWW3.vKyeQcM8fFO4777l4bVeQgDL6VIkxqlzQ7TCalQvla"
                bool doesPasswordMatch = BCrypt.CheckPassword(contrasenia, contraseniaBcrypt);
                Console.WriteLine("Bcript: " + doesPasswordMatch);

                SqlCommand registrarCmd = new SqlCommand(registrarUsuarioQuery);
                registrarCmd.Parameters.AddWithValue("@usuario", usuario);
                registrarCmd.Parameters.AddWithValue("@contrasenia", contraseniaBcrypt);
                registrarCmd.Parameters.AddWithValue("@contrasenia_salt", contraseniaSalt);

                // Obtiene el resultado
                int resultadoFilasSQL = AyudanteSQL.ExecuteNonQuery(cadenaConexion, registrarCmd);
                
                // Si es distinto mayor a 0, se habrá registrado el usuario
                if (resultadoFilasSQL > 0)
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
            // Crea el comando
            SqlCommand obtenerCmd = new SqlCommand(obtenerIDUsuarioQuery);
            obtenerCmd.Parameters.AddWithValue("@usuario", usuario.Nombre);
            obtenerCmd.Parameters.AddWithValue("@contrasenia", usuario.Contrasenia);
            // Obtiene el resultado
            object resultado = AyudanteSQL.ExecuteScalar(cadenaConexion, obtenerCmd);
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
        
        public int ObtenerIDConexion(Conexion conexion)
        {
            // Crea el comando
            SqlCommand obtenerIDCmd = new SqlCommand(obtenerIDConexionQuery);
            obtenerIDCmd.Parameters.AddWithValue("@nombredir", conexion.Nombre);
            obtenerIDCmd.Parameters.AddWithValue("@direccion", conexion.Direccion);
            obtenerIDCmd.Parameters.AddWithValue("@usuario", conexion.UsuarioConexion);
            // Obtiene el resultado
            object resultado = AyudanteSQL.ExecuteScalar(cadenaConexion, obtenerIDCmd);
            // Si el resultado es nulo, no existe la conexion.
            if (resultado == null)
            {
                return -1;
            }
            else
            {
                return (int)resultado;
            }
        }

        public ResultadoConexion RegistrarConexion(Conexion guardar)
        {
            // Si el puerto no está relleno, usar el por defecto almacenado en la bbdd
            // INSERT INTO conexion VALUES 
            // tipo_conexion, id_usuario, nombre, direccion, puerto, usuario, contrasenia
            int tipo_conexion = (int)guardar.TipoActual;
            int id_usuario = guardar.Propietario.ID;
            string nombre = guardar.Nombre;
            string direccion = guardar.Direccion;
            string usuario = guardar.UsuarioConexion;
            string contrasenia = guardar.ContraseniaConexion;
            if (guardar.Puerto == 0)
                guardar.Puerto = ObtenerPuertoDefecto(guardar.TipoActual);
            int puerto = guardar.Puerto;
            // La conexion se registra y luego se obtiene el ID de la BBDD en el constructor de Conexion
            if (ExisteConexion(guardar))
            {
                return new ResultadoConexion(ResultadoConexion.TipoResultado.DUPLICADO, null);
            }
            else
            {
                SqlCommand registrarCmd = new SqlCommand(registrarConexionQuery);
                // @tipo_conexion, @id_usuario, @nombre, @direccion, @puerto, @usuario, @contrasenia
                registrarCmd.Parameters.AddWithValue("@tipo_conexion", tipo_conexion);
                registrarCmd.Parameters.AddWithValue("@id_usuario", id_usuario);
                registrarCmd.Parameters.AddWithValue("@nombre", nombre);
                registrarCmd.Parameters.AddWithValue("@direccion", direccion);
                registrarCmd.Parameters.AddWithValue("@puerto", puerto);
                registrarCmd.Parameters.AddWithValue("@usuario", usuario);
                registrarCmd.Parameters.AddWithValue("@contrasenia", contrasenia);

                // Obtiene el resultado
                int resultadoFilasSQL = AyudanteSQL.ExecuteNonQuery(cadenaConexion, registrarCmd);

                // Si es distinto mayor a 0, se habrá registrado la conexion
                if (resultadoFilasSQL > 0)
                {
                    // Devuelve la conexión guardada con su ID asignado
                    guardar.ID = ObtenerIDConexion(guardar);
                    return new ResultadoConexion(ResultadoConexion.TipoResultado.ACEPTADO, guardar);
                }
                else
                {
                    return new ResultadoConexion(ResultadoConexion.TipoResultado.ERROR, null);
                }
            }
        }

        public bool EliminarConexion(Conexion eliminar)
        {
            SqlCommand eliminarCmd = new SqlCommand(eliminarConexionQuery);
            eliminarCmd.Parameters.AddWithValue("@id_conexion", eliminar.ID);
            // Obtiene resultado
            int resultadoFilasSQL = AyudanteSQL.ExecuteNonQuery(cadenaConexion, eliminarCmd); 

            // Si es distinto a 0, se eliminado la conexión
            return (resultadoFilasSQL != 0);
        }

        public ObservableCollection<Conexion> ObtenerConexionesUsuario(Usuario usuario)
        {
            // Crea el comando
            SqlCommand obtenerConexCmd = new SqlCommand(obtenerConexionesQuery);
            obtenerConexCmd.Parameters.AddWithValue("@id_usuario", usuario.ID);
            using (SqlDataReader lector = AyudanteSQL.ExecuteReader(cadenaConexion, obtenerConexCmd))
            {
                // Si el lector no es nulo, parsear las conexiones
                if (lector != null)
                {
                    ObservableCollection<Conexion> retorno = new ObservableCollection<Conexion>(BBDDProgramaMapeo.ExtraerConexiones(lector, usuario));
                    return retorno;
                }
                else
                {
                    return null;
                }
            }
        }

        //// Métodos privados ////

        private bool ExisteUsuario(string usuario)
        {
            // Crea el comando
            SqlCommand existirCmd = new SqlCommand(existirQuery);
            existirCmd.Parameters.AddWithValue("@usuario", usuario);
            // Obtiene resultado
            object resultado = AyudanteSQL.ExecuteScalar(cadenaConexion, existirCmd);

            // Si el resultado es nulo, no existe el usuario.
            return (resultado != null);
        }

        private bool ExisteConexion(Conexion guardar)
        {
            int resultado = ObtenerIDConexion(guardar);

            // Si el resultado es distinto a -1, existe la conexion
            return (resultado != -1);
        }

        public int ObtenerPuertoDefecto(Conexion.TipoConexion tipo)
        {
            // Crea el comando
            SqlCommand obtenerPuerto = new SqlCommand(obtenerPuertoQuery);
            obtenerPuerto.Parameters.AddWithValue("@id_tipo", (int)tipo);
            // Obtiene y devuelve el resultado
            return (int) AyudanteSQL.ExecuteScalar(cadenaConexion, obtenerPuerto);
        }

    }
}
