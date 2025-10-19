namespace GenteFit.Domain;

public class Actividad
{
    public int Id { get; set; }
    public string Nombre { get; set; } = "";
    public string? Descripcion { get; set; }
    public string Intensidad { get; set; } = "MEDIA";   // o enum si queréis
    public int PlazasMax { get; set; } = 16;

    public int SalaId { get; set; }
    public Sala Sala { get; set; } = null!;

    public ICollection<Sesion> Sesiones { get; set; } = new List<Sesion>();
}