using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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

        private void BtnVista1_Click(object sender, RoutedEventArgs e)
        {
            VistaPrincipal.Content = new Vista1(); // Vista1 es un UserControl
        }

        private void BtnVista2_Click(object sender, RoutedEventArgs e)
        {
            VistaPrincipal.Content = new Vista2();
        }

        private void BtnVista3_Click(object sender, RoutedEventArgs e)
        {
            VistaPrincipal.Content = new Vista3();
        }
    }
}