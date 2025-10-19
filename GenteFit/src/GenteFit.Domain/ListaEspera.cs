namespace GenteFit.Domain;

public class ListaEspera
{
    public int Id { get; set; }

    public int SesionId { get; set; }
    public Sesion Sesion { get; set; } = null!;

    public int ClienteId { get; set; }
    public Cliente Cliente { get; set; } = null!;

    public int Posicion { get; set; }
    public DateTime CreadaEn { get; set; } = DateTime.UtcNow;
}