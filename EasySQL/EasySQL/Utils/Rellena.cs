using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace EasySQL.Utils
{
    public class Rellena
    {
        public static void ComboBox(ComboBox aRellenar, IList<string> lista)
        {
            if (lista != null)
            {
                aRellenar.Items.Clear();
                foreach (string n in lista)
                {
                    aRellenar.Items.Add(n);
                }
            }
        }
    }
}
