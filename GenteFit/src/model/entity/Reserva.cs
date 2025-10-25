using GenteFit.src.model.enums;

namespace GenteFit.src.model.entity;

public class Reserva
{
    public int Id { get; set; }

    public int SesionId { get; set; }
    public Sesion Sesion { get; set; } = null!;

    public int ClienteId { get; set; }
    public Cliente Cliente { get; set; } = null!;

    public TipoEstado Estado { get; set; }
    public DateTime CreadaEn { get; set; } = DateTime.UtcNow;  // TODO revisar si es correcto
}