namespace GenteFit.src.model.entity;

public class Reserva
{
    public int Id { get; set; }

    public int SesionId { get; set; }
    public Sesion Sesion { get; set; } = null!;

    public int ClienteId { get; set; }
    public Cliente Cliente { get; set; } = null!;

    public string Estado { get; set; } = "CONFIRMADA"; // CONFIRMADA | CANCELADA | PROMOCIONADA
    public string Origen { get; set; } = "CLIENTE";    // CLIENTE | RECEPCION | SISTEMA
    public DateTime CreadaEn { get; set; } = DateTime.UtcNow;
}