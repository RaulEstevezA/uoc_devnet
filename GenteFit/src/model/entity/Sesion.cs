using GenteFit.src.model.enums;

namespace GenteFit.src.model.entity;

public class Sesion
{
    public int Id { get; set; }

    public int ActividadId { get; set; }
    public Actividad Actividad { get; set; } = null!;

    public int MonitorId { get; set; }
    public Monitor Monitor { get; set; } = null!;

    public DateTime Inicio { get; set; }
    public DateTime Fin { get; set; }
    public TipoEstado Estado { get; set; }

    public ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
    public ICollection<ListaEspera> Espera { get; set; } = new List<ListaEspera>();
}