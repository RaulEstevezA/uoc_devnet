using System;
using System.Collections.Generic;

namespace GenteFit.src.model.entity;

public class Sesion
{
    public int Id { get; set; }
    public int ActividadId { get; set; }
    public int MonitorId { get; set; }
    public DateTime FechaInicio { get; set; }
    public DateTime FechaFin { get; set; }

    // relaciones
    public Actividad? Actividad { get; set; }
    public Monitor? Monitor { get; set; }
    public List<Reserva>? Reservas { get; set; }
    public List<ReservarSala>? ReservasSala { get; set; }

    public override string ToString()
    {
        return $"{Id}: actividad {ActividadId}, monitor {MonitorId}, inicio {FechaInicio}, fin {FechaFin}";
    }
}