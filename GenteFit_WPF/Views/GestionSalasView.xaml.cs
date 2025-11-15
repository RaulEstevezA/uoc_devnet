using System.Windows;
using System.Windows.Controls;

namespace GenteFit_WPF.Views
{
    public partial class GestionSalasView : UserControl
    {
        public GestionSalasView()
        {
            InitializeComponent();
        }

        private void BtnAltaSala_Click(object sender, RoutedEventArgs e)
        {
            SalasContentPresenter.Content = new AltaSalaView();
        }

        private void BtnMostrarSalas_Click(object sender, RoutedEventArgs e)
        {
            SalasContentPresenter.Content = new MostrarSalasView();
        }

        private void BtnModificarSala_Click(object sender, RoutedEventArgs e)
        {
            SalasContentPresenter.Content = new ModificarSalasView();
        }

        private void BtnBajaSala_Click(object sender, RoutedEventArgs e)
        {
            SalasContentPresenter.Content = new BajaSalaView();
        }
    }
}
