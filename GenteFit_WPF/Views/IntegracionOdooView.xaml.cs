using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using GenteFit.src.DAO;

namespace GenteFit_WPF.Views
{
    public partial class IntegracionOdooView : UserControl
    {
        private readonly string exePath;

        public IntegracionOdooView()
        {
            InitializeComponent();

            // Ruta al ejecutable Python generado con PyInstaller (junto al exe WPF)
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            exePath = Path.Combine(basePath, "conexionOdoo.exe");
        }

        // ============================================================
        // EXPORTAR CLIENTES: C# -> XML -> Odoo
        // ============================================================
        private void BtnExportar_Click(object sender, RoutedEventArgs e)
        {
            string opcion = (ComboSeleccioneEntidad.SelectedItem as ComboBoxItem)?.Content.ToString();
            if (opcion != "Clientes")
            {
                MessageBox.Show("Esta funcionalidad estará disponible próximamente.");
                return;
            }

            if (!File.Exists(exePath))
            {
                MessageBox.Show($"No se encuentra el ejecutable de integración:\n{exePath}");
                return;
            }

            try
            {
                // Usar la carpeta base del ejecutable WPF
                string basePath = AppDomain.CurrentDomain.BaseDirectory;

                // Crear la carpeta xml_data si no existe
                string xmlFolder = Path.Combine(basePath, "xml_data");
                Directory.CreateDirectory(xmlFolder);

                // Definir la ruta del archivo XML
                string xmlFile = Path.Combine(xmlFolder, "clientes.xml");

                // Obtener los clientes y exportar
                var clientes = new ClienteDAO().GetAll().ToList();
                ClienteXML.GuardarClientesEnXml(clientes, xmlFile);

                // Ejecutar: conexionOdoo.exe exportar
                var psi = new ProcessStartInfo
                {
                    FileName = exePath,
                    Arguments = "importar",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                };

                var process = Process.Start(psi);

                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();
                process.WaitForExit();

                MessageBox.Show(
                    $"Exportación completada.\n\nSalida Python:\n{output}\n\nErrores:\n{error}",
                    "Integración con Odoo"
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error durante la exportación:\n{ex.Message}");
            }
        }

        // ============================================================
        // IMPORTAR CLIENTES: Odoo -> XML -> BBDD local
        // ============================================================
        private void BtnImportar_Click(object sender, RoutedEventArgs e)
        {
            string opcion = (ComboSeleccioneEntidad.SelectedItem as ComboBoxItem)?.Content.ToString();
            if (opcion != "Clientes")
            {
                MessageBox.Show("Esta funcionalidad estará disponible próximamente.");
                return;
            }

            if (!File.Exists(exePath))
            {
                MessageBox.Show($"No se encuentra el ejecutable de integración:\n{exePath}");
                return;
            }

            try
            {
                MessageBox.Show("Descargando clientes desde Odoo...");

                // 1. Ejecutar: conexionOdoo.exe importar
                var psi = new ProcessStartInfo
                {
                    FileName = exePath,
                    Arguments = "exportar",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                };

                var process = Process.Start(psi);

                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();
                process.WaitForExit();

                // 2. Buscar XML generado por Python en la misma carpeta base
                string basePath = AppDomain.CurrentDomain.BaseDirectory;
                string xmlFile = Path.Combine(basePath, "xml_data", "clientes.xml");

                if (!File.Exists(xmlFile))
                    throw new FileNotFoundException("Python no generó el XML", xmlFile);

                // 3. Importar a SQL Server
                ClienteXML.ImportarClientesDesdeXml(xmlFile);

                MessageBox.Show(
                    $"Importación completada.\n\n" +
                    $"Se han guardado en la BBDD local.\n\n" +
                    $"Salida Python:\n{output}\n\n" +
                    $"Errores:\n{error}",
                    "Integración con Odoo"
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error durante la importación:\n{ex.Message}");
            }
        }
    }
}