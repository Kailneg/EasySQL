using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasySQL.Modelos;
using EasySQL.Utils;

namespace EasySQL.BBDD
{
    public class BBDDProgramaImpl
    {
        private string cadenaConexion;
        
        private string registrarQuery, existirQuery, loginQuery, obtenerIDQuery, obtenerConexionesQuery,
            eliminarConexionQuery;

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
            eliminarConexionQuery = "DELETE FROM Conexion WHERE id_conexion = @id_conexion";
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
            // Crea el comando
            SqlCommand command = new SqlCommand(loginQuery);
            command.Parameters.AddWithValue("@usuario", usuario);
            command.Parameters.AddWithValue("@contrasenia", contrasenia);

            // Obtiene el resultado
            object resultado = AyudanteSQL.ExecuteScalar(cadenaConexion, command);
            
            // Si el resultado es nulo, no existe el usuario.
            if (resultado == null)
            {
                return new ResultadoLogin(ResultadoLogin.TipoResultado.DENEGADO, null);
            }
            else if (resultado.Equals(AyudanteSQL.ERROR))
            {
                return new ResultadoLogin(ResultadoLogin.TipoResultado.ERROR, null);
            }
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
                // Crea el comando
                SqlCommand registrarCmd = new SqlCommand(registrarQuery);
                registrarCmd.Parameters.AddWithValue("@usuario", usuario);
                registrarCmd.Parameters.AddWithValue("@contrasenia", contrasenia);

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
            SqlCommand obtenerCmd = new SqlCommand(obtenerIDQuery);
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

        public void RegistrarConexion(Conexion guardar)
        {
            // Mirar cada uno de los campos
            // Teniendo en consiederaración el campo Puerto
            // Si el puerto no está relleno, usar el por defecto almacenado en la bbdd

        }

        public bool EliminarConexion(Conexion eliminar)
        {
            // Crea el comando
            SqlCommand registCommand = new SqlCommand(eliminarConexionQuery);
            registCommand.Parameters.AddWithValue("@id_conexion", eliminar.ID);
            // Obtiene resultados
            int resultadoFilasSQL = AyudanteSQL.ExecuteNonQuery(cadenaConexion, registCommand); 

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

        private int obtenerPuertoDefecto(Conexion.TipoConexion tipo)
        {
            return 0;
        }

    }
}
