using System;
using System.Diagnostics;
using System.IO;

namespace GenteFit.src.service
{
    public static class PythonRunner
    {
        public static void Ejecutar(string scriptName)
        {
            // 1. Obtener path base del exe de WPF
            string basePath = AppDomain.CurrentDomain.BaseDirectory;

            // 2. Subir a raíz de solución
            string solutionRoot = Directory.GetParent(basePath).Parent.Parent.Parent.FullName;

            // 3. Construir ruta del script
            string pythonScript = Path.Combine(solutionRoot, "ConexionOdoo", scriptName);

            if (!File.Exists(pythonScript))
                throw new FileNotFoundException(
                    $"No se encuentra el script Python '{scriptName}' en la ruta:",
                    pythonScript
                );

            // 4. Preparar proceso
            var psi = new ProcessStartInfo
            {
                FileName = "python",
                Arguments = $"\"{pythonScript}\"",
                WorkingDirectory = Path.GetDirectoryName(pythonScript),
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };

            var process = Process.Start(psi);

            // 5. Capturar salida
            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();

            process.WaitForExit();

            // 6. Log opcional
            Console.WriteLine(output);
            Console.WriteLine(error);

            // 7. Comprobar exit code
            if (process.ExitCode != 0)
                throw new Exception($"Python devolvió error:\n{error}");
        }
    }
}