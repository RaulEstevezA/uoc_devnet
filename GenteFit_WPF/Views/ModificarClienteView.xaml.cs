using System.Linq;
using System.Windows;
using System.Windows.Controls;
using GenteFit.src.model.entity;
using GenteFit.src.model.GestionModelo;

namespace GenteFit_WPF.Views
{
    public partial class ModificarClienteView : UserControl
    {
        private Cliente? clienteActual;

        public ModificarClienteView()
        {
            InitializeComponent();
        }

        // BUSCAR POR ID
        private void BuscarPorIdButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(BuscarIdTextBox.Text, out int id))
                clienteActual = GestionCliente.BuscarPorId(id);

            CargarClienteSiExiste();
        }

        // BUSCAR POR EMAIL
        private void BuscarPorEmailButton_Click(object sender, RoutedEventArgs e)
        {
            clienteActual = GestionCliente.BuscarPorEmailUsuario(BuscarEmailTextBox.Text.Trim());
            CargarClienteSiExiste();
        }

        // BUSCAR POR USERNAME
        private void BuscarPorUsuarioButton_Click(object sender, RoutedEventArgs e)
        {
            clienteActual = GestionCliente.BuscarPorUsername(BuscarUsuarioTextBox.Text.Trim());
            CargarClienteSiExiste();
        }

        private void CargarClienteSiExiste()
        {
            if (clienteActual == null)
            {
                MessageBox.Show("Cliente no encontrado.");
                return;
            }

            IdTextBox.Text = clienteActual.Id.ToString();
            UsernameTextBox.Text = clienteActual.Usuario!.Username;
            DniTextBox.Text = clienteActual.Dni;
            NombreTextBox.Text = clienteActual.Nombre;
            Apellido1TextBox.Text = clienteActual.Apellido1;
            Apellido2TextBox.Text = clienteActual.Apellido2;
        }

        // GUARDAR CAMBIOS
        private void GuardarCambiosButton_Click(object sender, RoutedEventArgs e)
        {
            if (clienteActual == null)
            {
                MessageBox.Show("No hay cliente cargado.");
                return;
            }

            clienteActual.Usuario!.Username = UsernameTextBox.Text.Trim();
            clienteActual.Dni = DniTextBox.Text.Trim();
            clienteActual.Nombre = NombreTextBox.Text.Trim();
            clienteActual.Apellido1 = Apellido1TextBox.Text.Trim();
            clienteActual.Apellido2 = Apellido2TextBox.Text.Trim();

            GestionCliente.ModificarCliente(clienteActual);
            GestionCliente.ModificarUsuario(clienteActual.Usuario);

            MessageBox.Show("Cliente modificado correctamente.");
        }
    }
}

