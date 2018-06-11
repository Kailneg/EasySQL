using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace EasySQL.Utils
{
    public class Colorea
    {
        /// <summary>
        /// Color que se utilizará para valores correctos.
        /// </summary>
        private static readonly SolidColorBrush correcto = Brushes.LimeGreen;

        /// <summary>
        /// Color que se utilizará para valores incorrectos.
        /// </summary>
        private static readonly SolidColorBrush incorrecto = Brushes.Tomato;

        /// <summary>
        /// Colorea el contorno de un control dependiendo de un valor booleano.
        /// </summary>
        /// <param name="objeto">Control que se desea colorear.</param>
        /// <param name="valor">Valor booleano (correcto/incorrecto) que
        /// definirá el color que se desea asignar</param>
        public static void BordeCorrectoError(Control objeto, bool? valor)
        {
            if (valor.HasValue && valor.Value)
            {
                objeto.BorderBrush = correcto;
            } else
            {
                objeto.BorderBrush = incorrecto;
            }
        }

        /// <summary>
        /// Colorea o deja por defecto el contorno de un control 
        /// dependiendo de un valor booleano.
        /// </summary>
        /// <param name="objeto">Control que se desea colorear.</param>
        /// <param name="valor">Valor booleano (correcto/incorrecto/null) que
        /// definirá el color que se desea asignar</param>
        public static void BordeCorrectoErrorDefecto(Control objeto, bool? valor)
        {
            if (!valor.HasValue)
            {
                objeto.ClearValue(Control.BorderBrushProperty);
            }
            else if (valor.Value)
            {
                objeto.BorderBrush = correcto;
            }
            else
            {
                objeto.BorderBrush = incorrecto;
            }
        }
    }
}
