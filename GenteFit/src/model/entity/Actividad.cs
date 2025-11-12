using System.Collections.Generic;
namespace GenteFit.src.model.entity;

public class Actividad
{
    public int Id { get; set; }
    public string? Nombre { get; set; }
    public int DuracionMin { get; set; }
    public int PlazasMax { get; set; }
    // public string TipoIntensidad { get; set; } = ""; // podriamos quitarlo


    public override string ToString()
    {
        return $"{Id}: {Nombre} ({DuracionMin} min, {PlazasMax} plazas)";
    }
}
