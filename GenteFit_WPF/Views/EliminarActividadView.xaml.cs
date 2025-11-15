using System.Windows;
using System.Windows.Controls;
using GenteFit.src.model.GestionModelo;
using GenteFit.src.model.entity;

namespace GenteFit_WPF.Views
{
    public partial class EliminarActividadView : UserControl
    {
        private Actividad? actividadActual;

        public EliminarActividadView()
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

            NombreTextBox.Text = actividadActual.Nombre;
        }

        private void EliminarActividad_Click(object sender, RoutedEventArgs e)
        {
            if (actividadActual == null)
            {
                MessageBox.Show("no hay actividad cargada");
                return;
            }

            GestionActividad.EliminarActividad(actividadActual);

            MessageBox.Show("actividad eliminada");

            BuscarIdTextBox.Clear();
            NombreTextBox.Clear();
            actividadActual = null;
        }
    }
}
