using System;
using System.Diagnostics;

namespace SQLLauncher
{
    internal class Program
    {
        static int Main(string[] args)
        {
            string scriptPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "GenteFit_Init.sql");

            var psi = new ProcessStartInfo
            {
                FileName = "sqlcmd",
                Arguments = $"-S .\\SQLEXPRESS -E -i \"{scriptPath}\" -C -b",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };

            try
            {
                using var process = Process.Start(psi);
                if (process == null)
                {
                    Console.Error.WriteLine("No se pudo iniciar el proceso 'sqlcmd'. ¿Está instalado y en el PATH?");
                    return 2;
                }

                process.WaitForExit();

                Console.WriteLine(process.StandardOutput.ReadToEnd());
                Console.Error.WriteLine(process.StandardError.ReadToEnd());

                return process.ExitCode;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Error ejecutando sqlcmd:");
                Console.Error.WriteLine(ex.Message);
                Console.WriteLine("Presione una tecla para continuar...");
                Console.ReadKey();
                return 1;
            }
        }
    }
}
