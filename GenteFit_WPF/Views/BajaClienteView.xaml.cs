using System.Linq;
using System.Windows;
using System.Windows.Controls;
using GenteFit.src.model.entity;
using GenteFit.src.model.GestionModelo;

namespace GenteFit_WPF.Views
{
    public partial class BajaClienteView : UserControl
    {
        private Cliente? clienteActual;

        public BajaClienteView()
        {
            InitializeComponent();
        }

        // ======================
        // BUSCAR POR ID
        // ======================
        private void BuscarPorIdButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(BuscarIdTextBox.Text, out int id))
                clienteActual = GestionCliente.BuscarPorId(id);

            CargarClienteSiExiste();
        }

        // ======================
        // BUSCAR POR EMAIL
        // ======================
        private void BuscarPorEmailButton_Click(object sender, RoutedEventArgs e)
        {
            clienteActual = GestionCliente.BuscarPorEmailUsuario(BuscarEmailTextBox.Text.Trim());
            CargarClienteSiExiste();
        }

        // ======================
        // BUSCAR POR USERNAME
        // ======================
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
            UsernameTextBox.Text = clienteActual.Usuario?.Username;
            NombreTextBox.Text = clienteActual.Nombre;
            ApellidosTextBox.Text = $"{clienteActual.Apellido1} {clienteActual.Apellido2}";
        }

        // ======================
        // DAR DE BAJA
        // ======================
        private void EliminarClienteButton_Click(object sender, RoutedEventArgs e)
        {
            if (clienteActual == null)
            {
                MessageBox.Show("No hay cliente cargado.");
                return;
            }

            var confirm = MessageBox.Show(
                "¿Seguro que quieres dar de baja este cliente?",
                "Confirmar",
                MessageBoxButton.YesNo);

            if (confirm == MessageBoxResult.Yes)
            {
                GestionBajaUsuario.BajaUsuario(clienteActual.Usuario!);
                MessageBox.Show("Cliente dado de baja correctamente.");
                LimpiarCampos();
            }
        }

        private void LimpiarCampos()
        {
            clienteActual = null;
            IdTextBox.Clear();
            UsernameTextBox.Clear();
            NombreTextBox.Clear();
            ApellidosTextBox.Clear();
        }
    }
}

