using System.Windows;
using System.Windows.Controls;
using System.IO;
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
                MessageBox.Show("Esta funcionalidad estará disponible proximamente.");
                return;
            }

            try
            {
                //Obtener la ruta raíz del ejecutable
                string basePath = AppDomain.CurrentDomain.BaseDirectory;

                //Crear la carpeta xml_data si no existe
                string xmlFolder = Path.Combine(basePath, "xml_data");
                Directory.CreateDirectory(xmlFolder);

                //Definir la ruta del archivo XML
                string xmlFile = Path.Combine(xmlFolder, "clientes.xml");

                //Obtener los clientes y exportar
                var clientes = new ClienteDAO().GetAll().ToList();
                ClienteXML.GuardarClientesEnXml(clientes, xmlFile);

                // TODO Pendiente: Subir el archivo a Odoo mediante su API

                MessageBox.Show($"Exportación completada.\nArchivo generado en:\n{xmlFile}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al exportar clientes:\n{ex.Message}");
            }
        }

        private void BtnImportar_Click(object sender, RoutedEventArgs e)
        {
            string opcion = (ComboSeleccioneEntidad.SelectedItem as ComboBoxItem)?.Content.ToString();

            if (opcion != "Clientes")
            {
                MessageBox.Show("Esta funcionalidad estará disponible proximamente.");
                return;
            }

            MessageBox.Show("Importación iniciada.");


            // TODO Pendiente: Descargar el archivo desde Odoo mediante su API


            try
            {
                //Obtener la ruta raíz del ejecutable
                string basePath = AppDomain.CurrentDomain.BaseDirectory;

                //Definir la ruta del archivo XML
                string xmlFile = Path.Combine(basePath, "xml_data", "clientes.xml");

                //Importar los clientes desde el XML
                ClienteXML.ImportarClientesDesdeXml(xmlFile);
                MessageBox.Show("Importación completada.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al importar clientes:\n{ex.Message}");
            }
        }
    }
}
