using System.Windows;
using GenteFit_WPF.Views;

namespace GenteFit_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnReservas_Click(object sender, RoutedEventArgs e)
        {
            VistaPrincipal.Content = new ReservaActView(); // Vista1 es un UserControl
        }
        
        private void BtnSala_Click(object sender, RoutedEventArgs e)
        {
            VistaPrincipal.Content = new SalaView();
        }

        // boton alta cliente
        private void BtnAltaCliente_Click(object sender, RoutedEventArgs e)
        {
            VistaPrincipal.Content = new AltaClienteView();
        }

        // boton alta instructor
        private void BtnAltaInstructor_Click(object sender, RoutedEventArgs e)
        {
            VistaPrincipal.Content = new AltaInstructorView();
        }

        // boton alta usuario
        private void BtnAltaUsuario_Click(object sender, RoutedEventArgs e)
        {
            VistaPrincipal.Content = new AltaUsuarioView();
        }



        /*
        private void BtnVista3_Click(object sender, RoutedEventArgs e)
        {
            VistaPrincipal.Content = new Vista3();
        }
        */
    }
}