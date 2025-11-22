namespace GenteFit_WPF.ViewModels
{
    public class SesionListadoVM
    {
        public int SesionId { get; set; }
        public string Hora { get; set; } = "";
        public string Duracion { get; set; } = "";
        public string Actividad { get; set; } = "";
        public string Sala { get; set; } = "";
        public string Instructor { get; set; } = "";

        public string Capacidad { get; set; } = "";  // ejemplo: 12/16
        public string Espera { get; set; } = "";     // ejemplo: 3

        public string EstadoIcono { get; set; } = "⚪"; // ⚪ 🟡 🟢
    }
}

