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
    public DateTime FechaReserva { get; set; }
    public int? PosicionEspera { get; set; }


    public override string ToString()
    {
        return $"{Id}: cliente {ClienteId}, sesion {SesionId}, estado {EstadoReserva}, fecha {FechaReserva}, espera {PosicionEspera}";
    }
}