using System.Windows;
using System.Windows.Controls;
using GenteFit.src.model.entity;
using GenteFit.src.model.GestionModelo;

namespace GenteFit_WPF.Views
{
    public partial class GestionUsuariosView : UserControl
    {
        public GestionUsuariosView()
        {
            InitializeComponent();
        }

        private void BtnAltaUsuario_Click(object sender, RoutedEventArgs e)
        {
            UsuariosContentPresenter.Content = new AltaUsuarioView();
        }

        private void BtnModificarUsuario_Click(object sender, RoutedEventArgs e)
        {
            UsuariosContentPresenter.Content = new ModificarUsuarioView();
        }

        private void BtnBajaUsuario_Click(object sender, RoutedEventArgs e)
        {
            UsuariosContentPresenter.Content = new BajaUsuarioView();
        }
    }
}


