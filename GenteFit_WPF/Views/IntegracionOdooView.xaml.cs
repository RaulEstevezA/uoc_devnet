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
        public IntegracionOdooView()
        {
            InitializeComponent();
        }

        private void BtnExportar_Click(object sender, RoutedEventArgs e)
        {
            string opcion = (ComboSeleccioneEntidad.SelectedItem as ComboBoxItem)?.Content.ToString();

            if (opcion != "Clientes")
            {
                MessageBox.Show("Esta funcionalidad estará disponible próximamente.");
                return;
            }

            try
            {
                // ---------------------------------------------
                // 1. GENERAR XML
                // ---------------------------------------------
                string basePath = AppDomain.CurrentDomain.BaseDirectory;

                string xmlFolder = Path.Combine(basePath, "xml_data");
                Directory.CreateDirectory(xmlFolder);

                string xmlFile = Path.Combine(xmlFolder, "clientes.xml");

                var clientes = new ClienteDAO().GetAll().ToList();
                ClienteXML.GuardarClientesEnXml(clientes, xmlFile);

                // ---------------------------------------------
                // 2. LOCALIZAR SCRIPT PYTHON
                // ---------------------------------------------
                // basePath: /bin/Debug/net8.0-windows/

                string proyectoRoot = Directory.GetParent(basePath).Parent.Parent.Parent.FullName;
                string pythonScript = Path.Combine(proyectoRoot, "ConexionOdoo", "importar_clientes.py");

                if (!File.Exists(pythonScript))
                {
                    MessageBox.Show($"No se encuentra el script Python en:\n{pythonScript}");
                    return;
                }

                // ---------------------------------------------
                // 3. EJECUTAR SCRIPT PYTHON
                // ---------------------------------------------
                var psi = new ProcessStartInfo
                {
                    FileName = exePath,
                    WorkingDirectory = Path.GetDirectoryName(exePath),
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                };

                var process = Process.Start(psi);

                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();

                process.WaitForExit();

                // ---------------------------------------------
                // 4. MOSTRAR RESULTADOS
                // ---------------------------------------------
                string msg = $"Exportación completada.\nXML generado en:\n{xmlFile}\n\n";

                if (!string.IsNullOrWhiteSpace(output))
                    msg += $"Resultado Python:\n{output}\n";

                if (!string.IsNullOrWhiteSpace(error))
                    msg += $"Errores Python:\n{error}\n";

                MessageBox.Show(msg, "Integración con Odoo");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error durante la exportación:\n{ex.Message}");
            }
        }

        private void BtnImportar_Click(object sender, RoutedEventArgs e)
        {
            string opcion = (ComboSeleccioneEntidad.SelectedItem as ComboBoxItem)?.Content.ToString();

            if (opcion != "Clientes")
            {
                MessageBox.Show("Esta funcionalidad estará disponible próximamente.");
                return;
            }

            try
            {
                MessageBox.Show("Importación iniciada.\nObteniendo clientes desde Odoo...");

                // Ejecutar script Python que trae los datos de Odoo y genera clientes.xml
                // (exportar_clientes.py: Odoo -> XML)
                PythonRunner.Ejecutar("exportar_clientes.py");

                // Localizar el XML generado por el script de Python
                string basePath = AppDomain.CurrentDomain.BaseDirectory;
                string solutionRoot = Directory.GetParent(basePath).Parent.Parent.Parent.FullName;
                string xmlFile = Path.Combine(solutionRoot, "xml_data", "clientes.xml");

                if (!File.Exists(xmlFile))
                {
                    throw new FileNotFoundException(
                        "No se ha encontrado el archivo XML generado por la integración con Odoo.",
                        xmlFile
                    );
                }

                // Importar los clientes del XML a la base de datos local (SQL Server)
                ClienteXML.ImportarClientesDesdeXml(xmlFile);

                MessageBox.Show("Importación completada.\nLos clientes de Odoo se han guardado en la base de datos.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al importar clientes desde Odoo:\n{ex.Message}");
            }
        }
    }
}

