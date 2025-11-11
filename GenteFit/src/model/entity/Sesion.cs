using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenteFit.model.entity
{
    public class Sesion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        // 🔗 Relación N–1 con Actividad
        [Required]
        public int ActividadId { get; set; }
        public required Actividad Actividad { get; set; }

        // 🔗 Relación N–1 con Monitor
        [Required]
        public int MonitorId { get; set; }
        public required Monitor Monitor { get; set; }

        [Required]
        public DateTime FechaInicio { get; set; }

        [Required]
        public DateTime FechaFin { get; set; }

        // 🔗 Relación 1–N con Reserva
        public ICollection<Reserva>? Reservas { get; set; }

        // 🔗 Relación 1–1 o 1–N con ReservarSala
        public ICollection<ReservarSala>? ReservasSala { get; set; }

        // Validación simple para asegurar coherencia horaria
        [NotMapped]
        public bool RangoValido => FechaFin > FechaInicio;
    }
}