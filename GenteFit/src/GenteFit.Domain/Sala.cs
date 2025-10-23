namespace GenteFit.Domain;

public class Sala
{
    public int Id { get; set; }
    public string Nombre { get; set; } = "";
    public int AforoMax { get; set; }
    public bool Disponible { get; set; } = true;

    // public ICollection<Actividad> Actividades { get; set; } = new List<Actividad>(); No es necesario tenemos ReservaSala
}