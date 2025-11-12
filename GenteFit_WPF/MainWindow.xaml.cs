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

        private void BtnSala_Click(object sender, RoutedEventArgs e)
        {
            VistaPrincipal.Content = new SalaView(); // Vista1 es un UserControl
        }
        
        private void BtnSala_Click(object sender, RoutedEventArgs e)
        {
            VistaPrincipal.Content = new SalaView();
        }
        /*
        private void BtnVista3_Click(object sender, RoutedEventArgs e)
        {
            VistaPrincipal.Content = new Vista3();
        }
        */
    }
}