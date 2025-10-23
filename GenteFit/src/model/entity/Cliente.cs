namespace GenteFit.src.model.entity;

public class Cliente
{
    public int Id { get; set; }
    public string Nombre { get; set; } = "";
    public string Apellidos { get; set; } = "";
    public string? Dni { get; set; }
    public string? Email { get; set; } = null;
    public int MaxReservas { get; set; } = 3;
    public bool Activo { get; set; } = true;

    public ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
    public ICollection<ListaEspera> Esperas { get; set; } = new List<ListaEspera>();
}