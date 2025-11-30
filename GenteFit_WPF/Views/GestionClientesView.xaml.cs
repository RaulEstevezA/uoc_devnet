using System.Windows;
using System.Windows.Controls;

namespace GenteFit_WPF.Views
{
    public partial class GestionClientesView : UserControl
    {
        public GestionClientesView()
        {
            InitializeComponent();

            // Al abrir, mostramos Alta Cliente
            ClientesContentPresenter.Content = new AltaClienteView();
        }

        private void BtnAltaCliente_Click(object sender, RoutedEventArgs e)
        {
            ClientesContentPresenter.Content = new AltaClienteView();
        }

        private void BtnModificarCliente_Click(object sender, RoutedEventArgs e)
        {
            ClientesContentPresenter.Content = new ModificarClienteView();
        }

        private void BtnBajaCliente_Click(object sender, RoutedEventArgs e)
        {
            ClientesContentPresenter.Content = new BajaClienteView();
        }
    }
}

