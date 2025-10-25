using System;
using System.IO;

namespace GenteFit.src.utils
{
    public static class BorradorBBDD
    {
        public static void BorrarBaseDeDatos(string rutaDb)
        {
            if (File.Exists(rutaDb))
            {
                File.Delete(rutaDb);
                Console.WriteLine($"Base de datos borrada: {rutaDb}");
            }
            else
            {
                Console.WriteLine($"No se encontró la base de datos en: {rutaDb}");
            }
        }
    }
}
