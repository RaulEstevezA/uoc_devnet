using GenteFit.src.model.GestionModelo;
using System;
using System.Windows;
using System.Windows.Controls;

namespace GenteFit_WPF.Views
{
    public partial class AltaInstructorView : UserControl
    {
        public AltaInstructorView()
        {
            InitializeComponent();
        }

        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GestionAltaInstructor.CrearInstructor(
                    UsernameTextBox.Text,
                    EmailUsuarioTextBox.Text,
                    PasswordTextBox.Password,
                    NombreTextBox.Text,
                    Apellido1TextBox.Text,
                    Apellido2TextBox.Text
                );

                MessageBox.Show("Instructor creado correctamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}

