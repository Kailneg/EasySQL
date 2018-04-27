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
        private static readonly SolidColorBrush correcto = Brushes.LimeGreen;
        private static readonly SolidColorBrush incorrecto = Brushes.Tomato;

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
