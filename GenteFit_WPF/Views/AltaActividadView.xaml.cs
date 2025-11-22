using System.Windows;
using System.Windows.Controls;
using GenteFit.src.model.GestionModelo;

namespace GenteFit_WPF.Views
{
    public partial class AltaActividadView : UserControl
    {
        public AltaActividadView()
        {
            InitializeComponent();
        }

        private void CrearActividad_Click(object sender, RoutedEventArgs e)
        {
            string nombre = NombreTextBox.Text.Trim();

            if (!int.TryParse(DuracionTextBox.Text, out int duracion))
            {
                MessageBox.Show("duracion invalida");
                return;
            }

            if (!int.TryParse(PlazasTextBox.Text, out int plazas))
            {
                MessageBox.Show("plazas invalidas");
                return;
            }

            GestionActividad.CrearActividad(nombre, duracion, plazas);

            MessageBox.Show("actividad creada correctamente");

            NombreTextBox.Clear();
            DuracionTextBox.Clear();
            PlazasTextBox.Clear();
        }
    }
}
