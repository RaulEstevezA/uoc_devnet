using System.Windows;
using GenteFit.src.model.GestionModelo;
using GenteFit.src.model.entity;

namespace GenteFit_WPF.Views
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void BtnEntrar_Click(object sender, RoutedEventArgs e)
        {
            string username = TxtUsername.Text.Trim();
            string pass = TxtPass.Password;

            var usuarioValido = GestionLogin.ValidarLogin(username, pass);

            if (usuarioValido == null)
            {
                MessageBox.Show("Usuario o contraseña incorrectos",
                                "Error",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
                return;
            }

            // Guardamos el usuario en la sesión global
            SesionApp.UsuarioLogueado = usuarioValido;

            var main = new MainWindow(usuarioValido);
            main.Show();
            this.Close();
        }

    }
}


