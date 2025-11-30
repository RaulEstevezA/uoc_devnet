using System.Windows;
using System.Windows.Controls;
using GenteFit.src.model.entity;
using GenteFit.src.model.GestionModelo;

namespace GenteFit_WPF.Views
{
    public partial class BajaUsuarioView : UserControl
    {
        private Usuario? usuarioActual;

        public BajaUsuarioView()
        {
            InitializeComponent();
        }

        // buscar por id
        private void BuscarPorId_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(BuscarIdTextBox.Text, out int id))
                usuarioActual = GestionUsuario.BuscarPorId(id);

            CargarUsuario();
        }

        // buscar por email
        private void BuscarPorEmail_Click(object sender, RoutedEventArgs e)
        {
            usuarioActual = GestionUsuario.BuscarPorEmail(BuscarEmailTextBox.Text.Trim());
            CargarUsuario();
        }

        // buscar por username
        private void BuscarPorUsername_Click(object sender, RoutedEventArgs e)
        {
            usuarioActual = GestionUsuario.BuscarPorUsername(BuscarUsernameTextBox.Text.Trim());
            CargarUsuario();
        }

        private void CargarUsuario()
        {
            // no encontrado
            if (usuarioActual == null)
            {
                MessageBox.Show("usuario no encontrado");
                return;
            }

            // mostrar datos
            IdTextBox.Text = usuarioActual.Id.ToString();
            UsernameTextBox.Text = usuarioActual.Username;

            // si es administrador, bloquear baja
            if (usuarioActual.TipoRolId == 1)
            {
                BtnBajaUsuario.IsEnabled = false;
                MessageBox.Show("no se puede dar de baja un administrador");
            }
            else
            {
                BtnBajaUsuario.IsEnabled = true;
            }
        }

        private void BtnBajaUsuario_Click(object sender, RoutedEventArgs e)
        {
            if (usuarioActual == null)
            {
                MessageBox.Show("no hay usuario cargado");
                return;
            }

            // proteger administrador
            if (usuarioActual.TipoRolId == 1)
            {
                MessageBox.Show("no se puede dar de baja un administrador");
                return;
            }

            GestionUsuario.DarDeBajaUsuario(usuarioActual);

            MessageBox.Show("usuario dado de baja");

            // limpiar
            usuarioActual = null;
            IdTextBox.Clear();
            UsernameTextBox.Clear();
        }
    }
}

