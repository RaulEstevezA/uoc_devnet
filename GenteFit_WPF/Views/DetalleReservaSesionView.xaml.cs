using GenteFit.src.model.entity;
using GenteFit.src.model.enums;
using GenteFit.src.model.GestionModelo;
using System.Windows;
using System.Windows.Controls;

namespace GenteFit_WPF.Views
{
    public partial class DetalleReservaSesionView : UserControl
    {
        private readonly Sesion _sesion;
        private readonly Usuario _usuario;

        public DetalleReservaSesionView(Sesion sesion)
        {
            InitializeComponent();
            _sesion = sesion;
            _usuario = SesionApp.UsuarioLogueado
                       ?? throw new Exception("Error: No hay usuario en sesión");
            CargarDatos();
        }

        private void CargarDatos()
        {
            var detalle = GestionDetalleReserva.ConstruirDetalle(_sesion);

            TxtTitulo.Text = $"Detalles de la sesión #{_sesion.Id}";
            TxtActividad.Text = $"Actividad: {detalle.Actividad}";
            TxtInstructor.Text = $"Instructor: {detalle.Instructor}";
            TxtSala.Text = $"Sala: {detalle.Sala}";
            TxtFecha.Text = $"Horario: {detalle.Horario}";
            TxtCapacidad.Text = $"Ocupación: {detalle.ReservasConfirmadas}/{detalle.Plazas}";
            TxtEspera.Text = $"Lista de espera: {detalle.EnEspera} personas";

            ConfigurarBoton();
        }

        private void ConfigurarBoton()
        {
            var reserva = GestionMisReservas.ObtenerReservaPorUsuarioYSesion(_usuario.Id, _sesion.Id);

            if (reserva == null)
            {
                BtnAccion.Content = "Reservar";
                return;
            }

            if (reserva.EstadoReserva == TipoEstado.Reservada)
                BtnAccion.Content = "Cancelar reserva";
            else if (reserva.EstadoReserva == TipoEstado.EnEspera)
                BtnAccion.Content = "Cancelar (Lista de Espera)";
            else
                BtnAccion.Content = "Reservar";
        }

        private void BtnAccion_Click(object sender, RoutedEventArgs e)
        {
            var reservaExistente = GestionMisReservas.ObtenerReservaPorUsuarioYSesion(_usuario.Id, _sesion.Id);

            string resultado;

            if (reservaExistente == null)
                resultado = GestionLogicaReserva.Reservar(_usuario.Id, _sesion.Id);
            else
                resultado = GestionLogicaReserva.Cancelar(reservaExistente.Id);

            MessageBox.Show(resultado, "Información", MessageBoxButton.OK, MessageBoxImage.Information);

            // Navegación usando el botón real
            var mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();

            if (mainWindow?.BtnMisReservas != null)
            {
                mainWindow.BtnMisReservas.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
        }
    }
}





