using System.Windows;
using System.Windows.Controls;

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

            MessageBox.Show("Exportación iniciada.");
            // Aquí llamaremos al ExportadorXML en el siguiente paso
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
            // Aquí llamaremos al ImportadorXML en el siguiente paso
        }
    }
}
