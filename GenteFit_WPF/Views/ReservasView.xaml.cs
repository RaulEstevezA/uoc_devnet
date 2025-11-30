using GenteFit.src.model.entity;
using GenteFit.src.model.GestionModelo;
using System;
using System.Windows;
using System.Windows.Controls;

namespace GenteFit_WPF.Views
{
    public partial class ReservasView : UserControl
    {
        public ReservasView()
        {
            InitializeComponent();
        }

        // Cuando el usuario elige día en el calendario
        private void CalendarioSesiones_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CalendarioSesiones.SelectedDate == null)
                return;

            DateTime fecha = CalendarioSesiones.SelectedDate.Value;
            ListaSesionesDia.ItemsSource = GestionSesion.ObtenerSesionesPorFecha(fecha);
        }

        // Cuando selecciona una sesión del listado
        private void ListaSesionesDia_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListaSesionesDia.SelectedItem == null)
                return;

            var sesionSeleccionada = ListaSesionesDia.SelectedItem as Sesion;

            // Buscar la ventana correcta
            var ventana = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            if (ventana == null)
            {
                MessageBox.Show("No se encontró la ventana principal.");
                return;
            }

            // Cambiar vista
            ventana.VistaPrincipal.Content = new DetalleReservaSesionView(sesionSeleccionada!);
        }


    }
}
