using System.Windows;
using System.Windows.Controls;

namespace GenteFit_WPF.Views
{
    public partial class GestionSesionesView : UserControl
    {
        public GestionSesionesView()
        {
            InitializeComponent();
        }

        private void BtnAltaSesion_Click(object sender, RoutedEventArgs e)
        {
            SesionesContentPresenter.Content = new AltaSesionView();
        }

        private void BtnMostrarSesiones_Click(object sender, RoutedEventArgs e)
        {
            SesionesContentPresenter.Content = new MostrarSesionesView();
        }


        private void BtnModificarSesion_Click(object sender, RoutedEventArgs e)
        {
            SesionesContentPresenter.Content = new ModificarSesionView();
        }

        /* Pending decidir si se implementa o no la funcionalidad de eliminar sesiones
        private void BtnEliminarSesion_Click(object sender, RoutedEventArgs e)
        {
            SesionesContentPresenter.Content = new EliminarSesionView();
        }
        */
    }
}

