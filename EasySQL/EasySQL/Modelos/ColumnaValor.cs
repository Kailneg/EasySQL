using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySQL.Modelos
{
    /// <summary>
    /// Modelo para utilizar los pares de datos Columna/Valor
    /// existentes en una consulta SELECT.
    /// </summary>
    public class ColumnaValor
    {
        /// <summary>
        /// Nombre de la columna de la BBDD.
        /// </summary>
        public string Columna { get; set; }

        /// <summary>
        /// Datos de la columna de la BBDD.
        /// </summary>
        public string Valor { get; set; }

        /// <summary>
        /// Construye un objeto ColumnaValor y asigna sus campos.
        /// </summary>
        /// <param name="columna">La columna de la BBDD.</param>
        /// <param name="valor">El valor de la columna.</param>
        public ColumnaValor(string columna, string valor)
        {
            Columna = columna;
            Valor = valor;
        }
    }
}
