using System.Windows;
using System.Windows.Controls;
using GenteFit.src.model.entity;
using GenteFit.src.model.GestionModelo;

namespace GenteFit_WPF.Views
{
    public partial class ModificarUsuarioView : UserControl
    {
        private Usuario? usuarioActual;

        public ModificarUsuarioView()
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

        // cargar datos
        private void CargarUsuario()
        {
            if (usuarioActual == null)
            {
                MessageBox.Show("usuario no encontrado");
                return;
            }

            IdTextBox.Text = usuarioActual.Id.ToString();
            UsernameTextBox.Text = usuarioActual.Username;
            EmailTextBox.Text = usuarioActual.Email;
            TipoRolTextBox.Text = usuarioActual.TipoRolId.ToString();
            ActivoCheckBox.IsChecked = usuarioActual.Activo;
        }

        // guardar cambios
        private void GuardarCambios_Click(object sender, RoutedEventArgs e)
        {
            if (usuarioActual == null)
            {
                MessageBox.Show("no hay usuario cargado");
                return;
            }

            usuarioActual.Username = UsernameTextBox.Text.Trim();
            usuarioActual.Email = EmailTextBox.Text.Trim();
            usuarioActual.TipoRolId = int.Parse(TipoRolTextBox.Text.Trim());
            usuarioActual.Activo = ActivoCheckBox.IsChecked == true;

            GestionUsuario.ModificarUsuario(usuarioActual);

            MessageBox.Show("usuario modificado correctamente");
        }
    }
}

