using System.Windows;
using GenteFit.src.model.entity;
using GenteFit_WPF.Views;

namespace GenteFit_WPF
{
    public partial class MainWindow : Window
    {
        private readonly Usuario _usuarioLogueado;

        // Propiedad pública para que otras vistas accedan al usuario
        public Usuario UsuarioLogueado => _usuarioLogueado;

        public MainWindow(Usuario usuarioLogueado)
        {
            InitializeComponent();
            _usuarioLogueado = usuarioLogueado;

            this.Title = $"GenteFit - Bienvenido, {_usuarioLogueado.Username}";
        }

        private void BtnMisReservas_Click(object sender, RoutedEventArgs e)
        {
            VistaPrincipal.Content = new MisReservasView();
        }

        private void BtnGestionClientes_Click(object sender, RoutedEventArgs e)
        {
            VistaPrincipal.Content = new GestionClientesView();
        }

        private void BtnAltaInstructor_Click(object sender, RoutedEventArgs e)
        {
            VistaPrincipal.Content = new AltaInstructorView();
        }

        private void BtnAltaUsuario_Click(object sender, RoutedEventArgs e)
        {
            VistaPrincipal.Content = new AltaUsuarioView();
        }

        private void BtnGestionUsuarios_Click(object sender, RoutedEventArgs e)
        {
            VistaPrincipal.Content = new GestionUsuariosView();
        }

        private void BtnGestionActividades_Click(object sender, RoutedEventArgs e)
        {
            VistaPrincipal.Content = new GestionActividadesView();
        }

        private void BtnGestionSalas_Click(object sender, RoutedEventArgs e)
        {
            VistaPrincipal.Content = new GestionSalasView();
        }

        private void BtnGestionSesiones_Click(object sender, RoutedEventArgs e)
        {
            VistaPrincipal.Content = new GestionSesionesView();
        }

        private void BtnReservas_Click(object sender, RoutedEventArgs e)
        {
            VistaPrincipal.Content = new ReservasView();
        }
    }
}
