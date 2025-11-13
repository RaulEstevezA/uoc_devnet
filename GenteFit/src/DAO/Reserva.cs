using GenteFit.src.model.enums;

namespace GenteFit.src.model.entity;

public class Reserva
{
    public int Id { get; set; }

    // ids para las relaciones con cliente y sesion
    public int ClienteId { get; set; }
    public int SesionId { get; set; }

    // objetos cargados opcionalmente en logica (no se usan en ado.net)
    public Cliente? Cliente { get; set; }
    public Sesion? Sesion { get; set; }

    // estado de la reserva (mapeado desde la tabla estadoreserva)
    public TipoEstado EstadoReserva { get; set; } = TipoEstado.Reservada;

    // fecha en la que se realiza la reserva
    public DateTime FechaReserva { get; set; } = DateTime.Now;

    // posicion en la lista de espera (null si no aplica)
    public int? PosicionEspera { get; set; }

    public override string ToString()
    {
        var pos = PosicionEspera.HasValue ? $"espera {PosicionEspera}" : "sin espera";
        return $"{Id}: cliente {ClienteId}, sesion {SesionId}, estado {EstadoReserva}, fecha {FechaReserva}, {pos}";
    }
}
