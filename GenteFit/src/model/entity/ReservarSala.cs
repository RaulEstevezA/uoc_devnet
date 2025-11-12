using GenteFit.src.model.enums;
namespace GenteFit.src.model.entity;

public class ReservarSala
{
    public int Id { get; set; }
    public int SalaId { get; set; }
    public int SesionId { get; set; }
    public TipoEstado Estado { get; set; }

    // relaciones
    public Sala? Sala { get; set; }
    public Sesion? Sesion { get; set; }

    public override string ToString()
    {
        return $"{Id}: sala {SalaId}, sesion {SesionId}, estado {Estado}";
    }
}
