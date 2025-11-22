using System.Windows;
using System.Windows.Controls;
using GenteFit.src.model.entity;
using GenteFit.src.model.GestionModelo;

namespace GenteFit_WPF.Views
{
    public partial class BajaSalaView : UserControl
    {
        private Sala? salaActual;

        public BajaSalaView()
        {
            InitializeComponent();
        }

        private void Buscar_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(BuscarIdTextBox.Text, out int id))
                salaActual = GestionSala.ObtenerSalaPorId(id);

            if (salaActual == null)
            {
                MessageBox.Show("sala no encontrada");
                return;
            }

            NombreTextBox.Text = salaActual.Nombre;
        }

        private void Baja_Click(object sender, RoutedEventArgs e)
        {
            if (salaActual == null)
            {
                MessageBox.Show("no hay sala cargada");
                return;
            }

            // aqui usamos dar de baja (no borrar)
            GestionSala.DarDeBaja(salaActual);

            MessageBox.Show("sala dada de baja");

            BuscarIdTextBox.Clear();
            NombreTextBox.Clear();
            salaActual = null;
        }
    }
}
