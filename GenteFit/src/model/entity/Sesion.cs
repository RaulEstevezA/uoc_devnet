using System;
using System.Collections.Generic;
namespace GenteFit.src.model.entity;

public class Sesion
{
    public int Id { get; set; }
    public int ActividadId { get; set; }
    public int InstructorId { get; set; }
    public int SalaId { get; set; }
    public DateTime FechaInicio { get; set; }
    public DateTime FechaFin { get; set; }

    // relaciones
    public Actividad? Actividad { get; set; }
    public Instructor? Instructor { get; set; }
    public List<Reserva>? Reservas { get; set; }
    public Sala? Sala { get; set; }

    public override string ToString()
    {
        return $"{Id}: actividad {ActividadId}, instructor {InstructorId}, sala {SalaId}, " +
               $"{FechaInicio:dd/MM HH:mm} - {FechaFin:HH:mm}";
    }
}