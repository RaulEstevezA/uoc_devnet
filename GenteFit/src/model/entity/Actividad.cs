using GenteFit.src.model.enums;

namespace GenteFit.src.model.entity;

public class Actividad
{
    public int Id { get; set; }
    public string Nombre { get; set; } = "";
    public string? Descripcion { get; set; }
    public int Duracion { get; set; } = 45; // en minutos
    public TipoIntensidad Intensidad { get; set; }
    public int PlazasMax { get; set; } = 16;

    public int SalaId { get; set; }
    public Sala Sala { get; set; } = null!;

    public ICollection<Sesion> Sesiones { get; set; } = new List<Sesion>();
}