using System.Windows;
using System.Windows.Controls;
using GenteFit.src.model.entity;
using GenteFit.src.model.GestionModelo;

namespace GenteFit_WPF.Views
{
    public partial class AltaSalaView : UserControl
    {
        public AltaSalaView()
        {
            InitializeComponent();
        }

        private void GuardarSala_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NombreTextBox.Text))
            {
                MessageBox.Show("introduce un nombre válido");
                return;
            }

            if (!int.TryParse(AforoTextBox.Text, out int aforo))
            {
                MessageBox.Show("aforo no válido");
                return;
            }

            var sala = new Sala
            {
                Nombre = NombreTextBox.Text.Trim(),
                AforoMax = aforo,
                Disponible = DisponibleCheckBox.IsChecked == true
            };

            GestionSala.AgregarSala(sala);

            MessageBox.Show("sala guardada correctamente");

            NombreTextBox.Clear();
            AforoTextBox.Clear();
            DisponibleCheckBox.IsChecked = true;
        }
    }
}
