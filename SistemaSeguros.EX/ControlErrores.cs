using System;
using System.Configuration;
using System.IO;

namespace SistemaSeguros.EX
{
    public class ControlErrores : ApplicationException
    {
        public ControlErrores() { }
        public ControlErrores(string pvcMetodo, string pvcClase, string pvcError)
        {
            string vloRuta = ConfigurationManager.AppSettings["RutaBitacora"];

            string vloNomArchivo = ConfigurationManager.AppSettings["NomBitacora"];

            if (!Directory.Exists(vloRuta))
            {
                Directory.CreateDirectory(vloRuta);
            }

            StreamWriter sw = new StreamWriter(vloRuta + vloNomArchivo, true);

            sw.WriteLine(" ");
            sw.WriteLine("---------------------REGISTRO DE ERROR---------------------");
            sw.WriteLine("FECHA.........:" + DateTime.Now);
            sw.WriteLine("METODO........:" + pvcMetodo);
            sw.WriteLine("CLASE.........:" + pvcClase);
            sw.WriteLine("ERROR.........:" + pvcError);
            sw.WriteLine("---------------------FIN DE REGISTRO-----------------------");
            sw.WriteLine(" ");
            sw.Flush();
            sw.Close();
        }
    }
}
