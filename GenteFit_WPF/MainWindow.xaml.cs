using System.Windows;
using System.Windows.Controls;
using GenteFit.src.model.entity;
using GenteFit.src.model.enums;
using GenteFit_WPF.Views;

namespace GenteFit_WPF
{
    public partial class MainWindow : Window
    {
        private readonly Usuario _usuarioLogueado;
        public Usuario UsuarioLogueado => _usuarioLogueado;

        public MainWindow(Usuario usuarioLogueado)
        {
            InitializeComponent();
            _usuarioLogueado = usuarioLogueado;
            this.Title = $"GenteFit - Bienvenido, {_usuarioLogueado.Username}";
            ConfigurarVistaSegunRol();
        }

        private void ConfigurarVistaSegunRol()
        {
            OcultarTodosLosBotones();

            BtnMisReservas.Visibility = Visibility.Visible;
            BtnReservas.Visibility = Visibility.Visible;

            switch (_usuarioLogueado.TipoRolId)
            {
                case (int)TipoRol.Administrador:
                    MostrarBotonesAdministrador();
                    break;
                case (int)TipoRol.Encargado:
                    MostrarBotonesEncargado();
                    break;
                case (int)TipoRol.Recepcionista:
                    MostrarBotonesRecepcionista();
                    break;
                case (int)TipoRol.Cliente:
                    // Solo verá los botones básicos
                    break;
                case (int)TipoRol.Monitor:
                    // Configurar permisos para monitores
                    break;
            }
        }

        private void OcultarTodosLosBotones()
        {
            BtnGestionClientes.Visibility = Visibility.Collapsed;
            BtnAltaInstructor.Visibility = Visibility.Collapsed;
            BtnGestionUsuarios.Visibility = Visibility.Collapsed;
            BtnGestionActividades.Visibility = Visibility.Collapsed;
            BtnGestionSalas.Visibility = Visibility.Collapsed;
            BtnGestionSesiones.Visibility = Visibility.Collapsed;
        }

        private void MostrarBotonesAdministrador()
        {
            BtnGestionUsuarios.Visibility = Visibility.Visible;
            BtnGestionActividades.Visibility = Visibility.Visible;
            BtnGestionSalas.Visibility = Visibility.Visible;
            BtnGestionSesiones.Visibility = Visibility.Visible;
            BtnGestionClientes.Visibility = Visibility.Visible;
            BtnAltaInstructor.Visibility = Visibility.Visible;
        }

        private void MostrarBotonesEncargado()
        {
            BtnGestionActividades.Visibility = Visibility.Visible;
            BtnGestionSalas.Visibility = Visibility.Visible;
            BtnGestionSesiones.Visibility = Visibility.Visible;
            BtnGestionClientes.Visibility = Visibility.Visible;
        }

        private void MostrarBotonesRecepcionista()
        {
            BtnGestionClientes.Visibility = Visibility.Visible;
        }

        private void BtnMisReservas_Click(object sender, RoutedEventArgs e)
        {
            if (TienePermisoParaAcceder("MisReservas"))
            {
                MessageBox.Show("Funcionalidad de Mis Reservas");
            }
            else
            {
                MostrarMensajeSinPermisos();
            }
        }

        private void BtnReservas_Click(object sender, RoutedEventArgs e)
        {
            if (TienePermisoParaAcceder("ReservarActividades"))
            {
                MessageBox.Show("Funcionalidad de Reservar Actividades");
            }
            else
            {
                MostrarMensajeSinPermisos();
            }
        }

        private void BtnGestionClientes_Click(object sender, RoutedEventArgs e)
        {
            if (TienePermisoParaAcceder("GestionClientes"))
            {
                MessageBox.Show("Funcionalidad de Gestión de Clientes");
            }
            else
            {
                MostrarMensajeSinPermisos();
            }
        }

        private void BtnAltaInstructor_Click(object sender, RoutedEventArgs e)
        {
            if (TienePermisoParaAcceder("AltaInstructor"))
            {
                MessageBox.Show("Funcionalidad de Alta de Instructor");
            }
            else
            {
                MostrarMensajeSinPermisos();
            }
        }

        private void BtnGestionUsuarios_Click(object sender, RoutedEventArgs e)
        {
            if (TienePermisoParaAcceder("GestionUsuarios"))
            {
                MessageBox.Show("Funcionalidad de Gestión de Usuarios");
            }
            else
            {
                MostrarMensajeSinPermisos();
            }
        }

        private void BtnGestionActividades_Click(object sender, RoutedEventArgs e)
        {
            if (TienePermisoParaAcceder("GestionActividades"))
            {
                MessageBox.Show("Funcionalidad de Gestión de Actividades");
            }
            else
            {
                MostrarMensajeSinPermisos();
            }
        }

        private void BtnGestionSalas_Click(object sender, RoutedEventArgs e)
        {
            if (TienePermisoParaAcceder("GestionSalas"))
            {
                MessageBox.Show("Funcionalidad de Gestión de Salas");
            }
            else
            {
                MostrarMensajeSinPermisos();
            }
        }

        private void BtnGestionSesiones_Click(object sender, RoutedEventArgs e)
        {
            if (TienePermisoParaAcceder("GestionSesiones"))
            {
                MessageBox.Show("Funcionalidad de Gestión de Sesiones");
            }
            else
            {
                MostrarMensajeSinPermisos();
            }
        }

        private bool TienePermisoParaAcceder(string funcionalidad)
        {
            switch (funcionalidad)
            {
                case "MisReservas":
                case "ReservarActividades":
                    return true;

                case "GestionClientes":
                    return _usuarioLogueado.TipoRolId == (int)TipoRol.Administrador ||
                           _usuarioLogueado.TipoRolId == (int)TipoRol.Encargado ||
                           _usuarioLogueado.TipoRolId == (int)TipoRol.Recepcionista;

                case "GestionUsuarios":
                case "AltaInstructor":
                case "GestionActividades":
                case "GestionSalas":
                case "GestionSesiones":
                    return _usuarioLogueado.TipoRolId == (int)TipoRol.Administrador ||
                           _usuarioLogueado.TipoRolId == (int)TipoRol.Encargado;

                default:
                    return false;
            }
        }

        private void MostrarMensajeSinPermisos()
        {
            MessageBox.Show(
                "No tiene permisos para acceder a esta funcionalidad.",
                "Acceso denegado",
                MessageBoxButton.OK,
                MessageBoxImage.Warning
            );
        }
    }
}