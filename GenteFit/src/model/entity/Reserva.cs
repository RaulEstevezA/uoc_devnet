using System;
using GenteFit.src.model.enums;
namespace GenteFit.src.model.entity;

public class Reserva {
    public int Id { get; set; }
    public int ClienteId { get; set; }
    public Cliente? Cliente { get; set; }
    public int SesionId { get; set; }
    public Sesion? Sesion { get; set; }
    public TipoEstado EstadoReserva { get; set; }
    public DateTime FechaReserva { get; set; } = DateTime.Now;
    public int? PosicionEspera { get; set; }


    public override string ToString()
    {
        var pos = PosicionEspera.HasValue ? $"espera {PosicionEspera}" : "sin espera";
        return $"{Id}: cliente {ClienteId}, sesion {SesionId}, estado {EstadoReserva}, fecha {FechaReserva}, {pos}";
    }
}