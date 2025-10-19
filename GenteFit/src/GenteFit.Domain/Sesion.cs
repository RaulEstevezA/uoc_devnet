namespace GenteFit.Domain;

public class Sesion
{
    public int Id { get; set; }

    public int ActividadId { get; set; }
    public Actividad Actividad { get; set; } = null!;

    public int MonitorId { get; set; }
    public Monitor Monitor { get; set; } = null!;

    public DateTime Inicio { get; set; }
    public DateTime Fin { get; set; }
    public string Estado { get; set; } = "PROGRAMADA";

    public ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
    public ICollection<ListaEspera> Espera { get; set; } = new List<ListaEspera>();
}