using System;
using GenteFit.src.model.GestionModelo;
using System.Windows;
using System.Windows.Controls;

namespace GenteFit_WPF.Views
{
    public partial class AltaClienteView : UserControl
    {
        public AltaClienteView()
        {
            InitializeComponent();
        }

        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GestionAltaCliente.CrearCliente(
                    UsernameTextBox.Text,
                    EmailUsuarioTextBox.Text,
                    PasswordTextBox.Password,
                    NombreTextBox.Text,
                    Apellido1TextBox.Text,
                    Apellido2TextBox.Text,
                    DniTextBox.Text,
                    EmailClienteTextBox.Text
                );

                MessageBox.Show("Cliente creado correctamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}



