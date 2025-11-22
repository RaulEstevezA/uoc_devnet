using System.Windows;
using System.Windows.Controls;
using GenteFit.src.model.GestionModelo;
using GenteFit.src.model.entity;

namespace GenteFit_WPF.Views
{
    public partial class ModificarSalasView : UserControl
    {
        private Sala? salaActual;

        public ModificarSalasView()
        {
            InitializeComponent();
        }

        private void Buscar_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(BuscarIdTextBox.Text, out int id))
                salaActual = GestionSala.BuscarPorId(id);

            if (salaActual == null)
            {
                MessageBox.Show("sala no encontrada");
                return;
            }

            IdTextBox.Text = salaActual.Id.ToString();
            NombreTextBox.Text = salaActual.Nombre;
            AforoTextBox.Text = salaActual.AforoMax.ToString();
            DisponibleCheckBox.IsChecked = salaActual.Disponible;
        }

        private void Guardar_Click(object sender, RoutedEventArgs e)
        {
            if (salaActual == null)
            {
                MessageBox.Show("no hay sala cargada");
                return;
            }

            salaActual.Nombre = NombreTextBox.Text.Trim();
            salaActual.AforoMax = int.Parse(AforoTextBox.Text);
            salaActual.Disponible = DisponibleCheckBox.IsChecked == true;

            GestionSala.ModificarSala(salaActual);

            MessageBox.Show("sala modificada");
        }
    }
}
