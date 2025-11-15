using GenteFit.src.model.GestionModelo;
using System.Windows;
using System.Windows.Controls;

namespace GenteFit_WPF.Views
{
    public partial class AltaUsuarioView : UserControl
    {
        public AltaUsuarioView()
        {
            InitializeComponent();
        }

        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (RolComboBox.SelectedItem is not ComboBoxItem item)
                {
                    MessageBox.Show("Selecciona un rol.");
                    return;
                }

                int tipoRolId = int.TryParse(item?.Tag?.ToString(), out int result)
                    ? result
                    : 0;


                GestionAltaUsuario.CrearUsuario(
                    UsernameTextBox.Text,
                    EmailUsuarioTextBox.Text,
                    PasswordTextBox.Password,
                    tipoRolId
                );

                MessageBox.Show("Usuario creado correctamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
