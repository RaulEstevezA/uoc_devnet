using System.Windows;
using System.Windows.Controls;
using GenteFit.src.model.GestionModelo;
using GenteFit.src.model.entity;

namespace GenteFit_WPF.Views
{
    public partial class ModificarActividadView : UserControl
    {
        private Actividad? actividadActual;

        public ModificarActividadView()
        {
            InitializeComponent();
        }

        private void BuscarActividad_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(BuscarIdTextBox.Text, out int id))
                actividadActual = GestionActividad.BuscarPorId(id);

            if (actividadActual == null)
            {
                MessageBox.Show("actividad no encontrada");
                return;
            }

            IdTextBox.Text = actividadActual.Id.ToString();
            NombreTextBox.Text = actividadActual.Nombre;
            DuracionTextBox.Text = actividadActual.DuracionMin.ToString();
            PlazasTextBox.Text = actividadActual.PlazasMax.ToString();
        }

        private void GuardarCambios_Click(object sender, RoutedEventArgs e)
        {
            if (actividadActual == null)
            {
                MessageBox.Show("no hay actividad cargada");
                return;
            }

            actividadActual.Nombre = NombreTextBox.Text.Trim();
            actividadActual.DuracionMin = int.Parse(DuracionTextBox.Text);
            actividadActual.PlazasMax = int.Parse(PlazasTextBox.Text);

            GestionActividad.ModificarActividad(actividadActual);

            MessageBox.Show("actividad modificada");
        }
    }
}
