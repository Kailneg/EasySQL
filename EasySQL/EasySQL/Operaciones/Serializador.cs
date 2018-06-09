using EasySQL.Modelos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasySQL.Operaciones
{
    public class Serializador
    {
        public static bool Guardar(DatosConsulta datos)
        {
            // Almacena los datos en un fichero
            SaveFileDialog savefile = new SaveFileDialog();
            // set a default file name
            savefile.FileName = "datos.easy";
            // set filters - this can be done in properties as well
            savefile.Filter = "Archivos EasySQL (*.easy)|*.easy|Todos los archivos (*.*)|*.*";

            if (savefile.ShowDialog() == DialogResult.OK)
            {
                IFormatter formatter = new BinaryFormatter();
                try
                {
                    using (Stream stream = new FileStream(savefile.FileName, FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        formatter.Serialize(stream, datos);
                    }
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return false;
                }
            }
            return false;
        }

        public static DatosConsulta Cargar()
        {
            // Almacena los datos en un fichero
            OpenFileDialog savefile = new OpenFileDialog();
            // set filters - this can be done in properties as well
            savefile.Filter = "Archivos EasySQL (*.easy)|*.easy|Todos los archivos (*.*)|*.*";

            DatosConsulta retorno = null;
            if (savefile.ShowDialog() == DialogResult.OK)
            {
                IFormatter formatter = new BinaryFormatter();
                try
                {
                    using (Stream stream = new FileStream(savefile.FileName, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        object archivo = formatter.Deserialize(stream);
                        if (archivo is DatosConsulta)
                            retorno = archivo as DatosConsulta;
                    }
                    return retorno;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                }
            }
            return null;
        }
    }
}
