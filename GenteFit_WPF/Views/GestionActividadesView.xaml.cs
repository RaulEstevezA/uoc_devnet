using System.Windows;
using System.Windows.Controls;

namespace GenteFit_WPF.Views
{
    public partial class GestionActividadesView : UserControl
    {
        public GestionActividadesView()
        {
            InitializeComponent();
        }

        private void BtnAlta_Click(object sender, RoutedEventArgs e)
        {
            ActividadesContentPresenter.Content = new AltaActividadView();
        }

        private void BtnMostrar_Click(object sender, RoutedEventArgs e)
        {
            ActividadesContentPresenter.Content = new MostrarActividadesView();
        }

        private void BtnModificar_Click(object sender, RoutedEventArgs e)
        {
            ActividadesContentPresenter.Content = new ModificarActividadView();
        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            ActividadesContentPresenter.Content = new EliminarActividadView();
        }
    }
}



