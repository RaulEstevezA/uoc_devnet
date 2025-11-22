using System;
using System.Windows;
using System.Windows.Controls;
using GenteFit.src.model.GestionModelo;
using GenteFit.src.model.entity;

namespace GenteFit_WPF.Views
{
    public partial class AltaSesionView : UserControl
    {
        public AltaSesionView()
        {
            InitializeComponent();
            CargarCombos();
            CargarHoras();
        }

        private void CargarCombos()
        {
            ActividadCombo.ItemsSource = GestionActividad.GetAll();
            ActividadCombo.DisplayMemberPath = "Nombre";
            ActividadCombo.SelectedValuePath = "Id";

            SalaCombo.ItemsSource = GestionSala.ObtenerSalas();
            SalaCombo.DisplayMemberPath = "Nombre";
            SalaCombo.SelectedValuePath = "Id";

            InstructorCombo.ItemsSource = GestionInstructor.ObtenerInstructores();
            InstructorCombo.DisplayMemberPath = "Nombre";
            InstructorCombo.SelectedValuePath = "Id";
        }

        private void CargarHoras()
        {
            TimeSpan horaInicio = new TimeSpan(8, 0, 0);
            TimeSpan horaFin = new TimeSpan(19, 15, 0);

            while (horaInicio <= horaFin)
            {
                HoraCombo.Items.Add(horaInicio.ToString(@"hh\:mm"));
                horaInicio = horaInicio.Add(new TimeSpan(0, 15, 0));
            }
        }

        private void CrearSesion_Click(object sender, RoutedEventArgs e)
        {
            if (ActividadCombo.SelectedValue == null ||
                SalaCombo.SelectedValue == null ||
                InstructorCombo.SelectedValue == null ||
                FechaPicker.SelectedDate == null ||
                HoraCombo.SelectedItem == null)
            {
                MessageBox.Show("Completa todos los campos");
                return;
            }

            var sesion = new Sesion
            {
                ActividadId = (int)ActividadCombo.SelectedValue,
                SalaId = (int)SalaCombo.SelectedValue,
                InstructorId = (int)InstructorCombo.SelectedValue,
                FechaInicio = DateTime.Parse($"{FechaPicker.SelectedDate:yyyy-MM-dd} {HoraCombo.SelectedItem}")
            };

            // duracion fija 45 min
            sesion.FechaFin = sesion.FechaInicio.AddMinutes(45);

            GestionSesion.CrearSesion(sesion);

            MessageBox.Show("Sesión creada correctamente");

            ActividadCombo.SelectedIndex = -1;
            SalaCombo.SelectedIndex = -1;
            InstructorCombo.SelectedIndex = -1;
            FechaPicker.SelectedDate = null;
            HoraCombo.SelectedIndex = -1;
        }
    }
}
