using EasySQL.Modelos;
using EasySQL.Operaciones.Comandos.MicrosoftSQL;
using EasySQL.Operaciones.Comandos.MySQL;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySQL.Operaciones.Comandos
{
    public class Comando
    {
        /// <summary>
        /// Define los parámetros a usar para reemplazar en sentencias SQL.
        /// </summary>
        public static readonly string[] PARAMS 
            = { "@1param", "@2param", "@3param", "@4param", "@5param" };

        /// <summary>
        /// Define las condiciones WHERE que existen
        /// </summary>
        public static readonly string[] TIPOS_CONDICIONES 
            = { "", "=", "<>", ">", "<", ">=", "<=",
            "LIKE", "NOT LIKE", "IN", "NOT IN", "IS NULL", "IS NOT NULL" };

        /// <summary>
        /// Define los operadores de unión para unir condiciones WHERE
        /// </summary>
        public static readonly string[] TIPOS_CONDICIONES_UNION = { "", "AND", "OR" };

        /// <summary>
        /// Define el orden que pueden tener las columnas ORDER BY
        /// </summary>
        public static readonly string[] TIPOS_ORDEN = { "", "ASC", "DESC" };

        /// <summary>
        /// Devuelve los tipos de datos disponibles para una determinado tipo de base de datos.
        /// </summary>
        /// <param name="conexionActual">La conexión que se está usando.</param>
        /// <returns>Un array con los tipos de datos que existen para una BBDD.</returns>
        public static string[] TiposDatos(Conexion conexionActual)
        {
            if (conexionActual.TipoActual == Conexion.TipoConexion.MicrosoftSQL)
            {
                return ComandoMicrosoftSQL.TIPOS_DATOS;
            }
            else if (conexionActual.TipoActual == Conexion.TipoConexion.MySQL)
            {
                return ComandoMySQL.TIPOS_DATOS;
            }
            else return null;
        }

        /// <summary>
        /// Devuelve el comando utilizado para mostrar las Bases de Datos
        /// </summary>
        /// <param name="conexionActual">La conexión actual que se está usando.</param>
        /// <returns>Comando para mostrar las bases de datos según tipo de BBDD.</returns>
        public static DbCommand ShowDatabases(Conexion conexionActual)
        {
            if (conexionActual.TipoActual == Conexion.TipoConexion.MicrosoftSQL)
            {
                return ComandoMicrosoftSQL.ShowDatabases();
            }
            else if (conexionActual.TipoActual == Conexion.TipoConexion.MySQL)
            {
                return ComandoMySQL.ShowDatabases();
            }
            else return null;
        }

        /// <summary>
        /// Devuelve el comando utilizado para crear las Bases de Datos
        /// </summary>
        /// <param name="conexionActual">La conexión actual que se está usando.</param>
        /// <returns>Comando para crear las bases de datos según tipo de BBDD.</returns>
        public static DbCommand CreateDatabase(Conexion conexionActual)
        {
            if (conexionActual.TipoActual == Conexion.TipoConexion.MicrosoftSQL)
            {
                return ComandoMicrosoftSQL.CreateDatabase();
            }
            else if (conexionActual.TipoActual == Conexion.TipoConexion.MySQL)
            {
                return ComandoMySQL.CreateDatabase();
            }
            else return null;
        }

        /// <summary>
        /// Devuelve el comando utilizado para borrar las Bases de Datos
        /// </summary>
        /// <param name="conexionActual">La conexión actual que se está usando.</param>
        /// <param name="forzar">Si se desea forzar el borrado.</param>
        /// <returns>Comando para borrar las bases de datos según tipo de BBDD.</returns>
        public static DbCommand DropDatabase(Conexion conexionActual, bool forzar)
        {
            if (conexionActual.TipoActual == Conexion.TipoConexion.MicrosoftSQL)
            {
                return ComandoMicrosoftSQL.DropDatabase(forzar);
            }
            else if (conexionActual.TipoActual == Conexion.TipoConexion.MySQL)
            {
                return ComandoMySQL.ComandoDropDatabase(forzar);
            }
            else return null;
        }

        /// <summary>
        /// Devuelve el comando utilizado para crear las tablas de Bases de Datos
        /// </summary>
        /// <param name="conexionActual">La conexión actual que se está usando.</param>
        /// <returns>Comando para crear las tablas de las bases de datos según tipo de BBDD.</returns>
        public static DbCommand CreateTable(Conexion conexionActual)
        {
            if (conexionActual.TipoActual == Conexion.TipoConexion.MicrosoftSQL)
            {
                return ComandoMicrosoftSQL.CreateTable();
            }
            else if (conexionActual.TipoActual == Conexion.TipoConexion.MySQL)
            {
                return ComandoMySQL.CreateTable();
            }
            else return null;
        }

        /// <summary>
        /// Devuelve el comando utilizado para borrar las tablas de las Bases de Datos
        /// </summary>
        /// <param name="conexionActual">La conexión actual que se está usando.</param>
        /// <returns>Comando para borrar las tablas de las bases de datos según tipo de BBDD.</returns>
        public static DbCommand DropTable(Conexion conexionActual)
        {
            if (conexionActual.TipoActual == Conexion.TipoConexion.MicrosoftSQL)
            {
                return ComandoMicrosoftSQL.DropTable();
            }
            else if (conexionActual.TipoActual == Conexion.TipoConexion.MySQL)
            {
                return ComandoMySQL.DropTable();
            }
            else return null;
        }

        /// <summary>
        /// Devuelve el comando utilizado para modificar las tablas de las Bases de Datos
        /// </summary>
        /// <param name="conexionActual">La conexión actual que se está usando.</param>
        /// <returns>Comando para modificar las tablas de las bases de datos según tipo de BBDD.</returns>
        public static DbCommand AlterTable(Conexion conexionActual)
        {
            if (conexionActual.TipoActual == Conexion.TipoConexion.MicrosoftSQL)
            {
                return ComandoMicrosoftSQL.AlterTable();
            }
            else if (conexionActual.TipoActual == Conexion.TipoConexion.MySQL)
            {
                return ComandoMySQL.AlterTable();
            }
            else return null;
        }

        /// <summary>
        /// Devuelve el comando utilizado para mostrar las tablas de las Bases de Datos
        /// </summary>
        /// <param name="conexionActual">La conexión actual que se está usando.</param>
        /// <returns>Comando para mostrar las tablas de las bases de datos según tipo de BBDD.</returns>
        public static DbCommand ShowTables(Conexion conexionActual)
        {
            if (conexionActual.TipoActual == Conexion.TipoConexion.MicrosoftSQL)
            {
                return ComandoMicrosoftSQL.ShowTables();
            }
            else if (conexionActual.TipoActual == Conexion.TipoConexion.MySQL)
            {
                return ComandoMySQL.ShowTables();
            }
            else return null;
        }

        /// <summary>
        /// Devuelve el comando utilizado para mostrar las columnas de las tablas.
        /// </summary>
        /// <param name="conexionActual">La conexión actual que se está usando.</param>
        /// <returns>Comando para mostrar las columnas de las tablas según tipo de BBDD.</returns>
        public static DbCommand ShowColumnas(Conexion conexionActual)
        {
            if (conexionActual.TipoActual == Conexion.TipoConexion.MicrosoftSQL)
            {
                return ComandoMicrosoftSQL.ShowColumnas();
            }
            else if (conexionActual.TipoActual == Conexion.TipoConexion.MySQL)
            {
                return ComandoMySQL.ShowColumnas();
            }
            else return null;
        }

        /// <summary>
        /// Devuelve el comando utilizado para mostrar tipos de datos de columnas de las tablas.
        /// </summary>
        /// <param name="conexionActual">La conexión actual que se está usando.</param>
        /// <returns>Comando para mostrar los tipos de las columnas de las tablas según tipo de BBDD.</returns>
        public static DbCommand ShowTiposDatosColumnas(Conexion conexionActual)
        {
            if (conexionActual.TipoActual == Conexion.TipoConexion.MicrosoftSQL)
            {
                return ComandoMicrosoftSQL.ShowTiposDatosColumnas();
            }
            else if (conexionActual.TipoActual == Conexion.TipoConexion.MySQL)
            {
                return ComandoMySQL.ShowTiposDatosColumnas();
            }
            else return null;
        }

        /// <summary>
        /// Devuelve el comando utilizado para eliminar datos.
        /// </summary>
        /// <param name="conexionActual">La conexión actual que se está usando.</param>
        /// <returns>Comando para eliminar datos según tipo de BBDD.</returns>
        public static DbCommand DeleteFrom(Conexion conexionActual)
        {
            if (conexionActual.TipoActual == Conexion.TipoConexion.MicrosoftSQL)
            {
                return ComandoMicrosoftSQL.DeleteFrom();
            }
            else if (conexionActual.TipoActual == Conexion.TipoConexion.MySQL)
            {
                return ComandoMySQL.DeleteFrom();
            }
            else return null;
        }

        /// <summary>
        /// Devuelve el comando utilizado para actualizar datos.
        /// </summary>
        /// <param name="conexionActual">La conexión actual que se está usando.</param>
        /// <returns>Comando para actualizar datos según tipo de BBDD.</returns>
        public static DbCommand Update(Conexion conexionActual)
        {
            if (conexionActual.TipoActual == Conexion.TipoConexion.MicrosoftSQL)
            {
                return ComandoMicrosoftSQL.Update();
            }
            else if (conexionActual.TipoActual == Conexion.TipoConexion.MySQL)
            {
                return ComandoMySQL.Update();
            }
            else return null;
        }

        /// <summary>
        /// Devuelve el comando utilizado para añadir datos.
        /// </summary>
        /// <param name="conexionActual">La conexión actual que se está usando.</param>
        /// <returns>Comando para añadir datos según tipo de BBDD.</returns>
        public static DbCommand Insert(Conexion conexionActual)
        {
            if (conexionActual.TipoActual == Conexion.TipoConexion.MicrosoftSQL)
            {
                return ComandoMicrosoftSQL.Insert();
            }
            else if (conexionActual.TipoActual == Conexion.TipoConexion.MySQL)
            {
                return ComandoMySQL.Insert();
            }
            else return null;
        }

        /// <summary>
        /// Devuelve el comando utilizado para mostrar datos.
        /// </summary>
        /// <param name="conexionActual">La conexión actual que se está usando.</param>
        /// <returns>Comando para mostrar datos según tipo de BBDD.</returns>
        public static DbCommand Select(Conexion conexionActual)
        {
            if (conexionActual.TipoActual == Conexion.TipoConexion.MicrosoftSQL)
            {
                return ComandoMicrosoftSQL.Select();
            }
            else if (conexionActual.TipoActual == Conexion.TipoConexion.MySQL)
            {
                return ComandoMySQL.Select();
            }
            else return null;
        }
    }
}
