using System;
using System.Windows;
using System.Windows.Controls;
using GenteFit.src.model.GestionModelo;
using GenteFit.src.model.entity;

namespace GenteFit_WPF.Views
{
    public partial class MisReservasView : UserControl
    {
        public MisReservasView()
        {
            InitializeComponent();

            // 1) sacamos el usuario de SesionApp
            var usuario = SesionApp.UsuarioLogueado
                          ?? throw new Exception("No hay usuario en sesión.");

            // 2) cargamos sus reservas activas / en espera
            ListaMisReservas.ItemsSource =
                GestionMisReservas.ObtenerReservasActivasOrdenadas(usuario.Id);
        }

        private void ListaMisReservas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListaMisReservas.SelectedItem is not Reserva reservaSeleccionada)
                return;

            // 3) obtenemos la sesión de esa reserva
            var sesion = GestionSesion.ObtenerSesionPorId(reservaSeleccionada.SesionId);

            if (sesion == null)
            {
                MessageBox.Show("No se ha encontrado la sesión asociada.",
                                "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // 4) navegamos al detalle, pasándole la sesión
            var ventana = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            if (ventana != null)
            {
                ventana.VistaPrincipal.Content = new DetalleReservaSesionView(sesion);
            }


        }
    }
}


