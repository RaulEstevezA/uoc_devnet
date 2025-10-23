namespace GenteFit.src.model.entity;

public class Monitor
{
    public int Id { get; set; }
    public string Nombre { get; set; } = "";
    public string Apellidos { get; set; } = "";
    public string? Email { get; set; }
    public ICollection<Actividad> Actividades { get; set; } = new List<Actividad>();
    public ICollection<TurnoMonitor> Turnos { get; set; } = new List<TurnoMonitor>();
}