using System;
using System.Windows;
using System.Windows.Controls;
using GenteFit.src.model.entity;
using GenteFit.src.model.GestionModelo;

namespace GenteFit_WPF.Views
{
    public partial class ModificarSesionView : UserControl
    {
        private Sesion? sesionActual;

        public ModificarSesionView()
        {
            InitializeComponent();

            // cargamos listas
            ActividadCombo.ItemsSource = GestionActividad.GetAll();
            ActividadCombo.DisplayMemberPath = "Nombre";
            ActividadCombo.SelectedValuePath = "Id";

            InstructorCombo.ItemsSource = GestionInstructor.ObtenerInstructores();
            InstructorCombo.DisplayMemberPath = "NombreCompleto";
            InstructorCombo.SelectedValuePath = "Id";

            SalaCombo.ItemsSource = GestionSala.ObtenerSalas();
            SalaCombo.DisplayMemberPath = "Nombre";
            SalaCombo.SelectedValuePath = "Id";
        }

        private void Buscar_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(IdSesionTextBox.Text, out int id))
            {
                MessageBox.Show("ID no válido");
                return;
            }

            sesionActual = GestionSesion.ObtenerSesionPorId(id);

            if (sesionActual == null)
            {
                MessageBox.Show("No existe una sesión con ese ID");
                return;
            }

            // rellenamos campos
            ActividadCombo.SelectedValue = sesionActual.ActividadId;
            InstructorCombo.SelectedValue = sesionActual.InstructorId;
            SalaCombo.SelectedValue = sesionActual.SalaId;

            FechaPicker.SelectedDate = sesionActual.FechaInicio.Date;
            HoraInicioTextBox.Text = sesionActual.FechaInicio.ToString("HH:mm");
        }

        private void GuardarCambios_Click(object sender, RoutedEventArgs e)
        {
            if (sesionActual == null)
            {
                MessageBox.Show("Primero debes buscar una sesión.");
                return;
            }

            // reconstruimos fecha y hora
            if (!FechaPicker.SelectedDate.HasValue ||
                !TimeSpan.TryParse(HoraInicioTextBox.Text, out TimeSpan hora))
            {
                MessageBox.Show("Fecha u hora no válidas.");
                return;
            }

            DateTime nuevaFechaInicio = FechaPicker.SelectedDate.Value.Date + hora;

            // obtenemos duración desde la actividad
            var actividad = GestionActividad.BuscarPorId((int)ActividadCombo.SelectedValue);
            if (actividad == null)
            {
                MessageBox.Show("Error: actividad no encontrada.");
                return;
            }

            DateTime fechaFin = nuevaFechaInicio.AddMinutes(actividad.DuracionMin);

            // actualizamos objeto
            sesionActual.ActividadId = (int)ActividadCombo.SelectedValue;
            sesionActual.InstructorId = (int)InstructorCombo.SelectedValue;
            sesionActual.SalaId = (int)SalaCombo.SelectedValue;
            sesionActual.FechaInicio = nuevaFechaInicio;
            sesionActual.FechaFin = fechaFin;

            GestionSesion.ModificarSesion(sesionActual);

            MessageBox.Show("Sesión actualizada correctamente.");
        }
    }
}
