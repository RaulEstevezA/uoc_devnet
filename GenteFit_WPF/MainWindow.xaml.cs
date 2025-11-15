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
        

        // boton gestion cliente
        private void BtnGestionClientes_Click(object sender, RoutedEventArgs e)
        {
            VistaPrincipal.Content = new GestionClientesView();
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

        // gestion usuarios
        private void BtnGestionUsuarios_Click(object sender, RoutedEventArgs e)
        {
            VistaPrincipal.Content = new GestionUsuariosView();
        }

        // gestion actividades
        private void BtnGestionActividades_Click(object sender, RoutedEventArgs e)
        {
            VistaPrincipal.Content = new GestionActividadesView();
        }

        // gestion salas
        private void BtnGestionSalas_Click(object sender, RoutedEventArgs e)
        {
            VistaPrincipal.Content = new GestionSalasView();
        }




        /*
        private void BtnVista3_Click(object sender, RoutedEventArgs e)
        {
            VistaPrincipal.Content = new Vista3();
        }
        */
    }
}