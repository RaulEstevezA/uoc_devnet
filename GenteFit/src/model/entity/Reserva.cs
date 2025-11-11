using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GenteFit.model.enums;

namespace GenteFit.model.entity
{
    public class Reserva
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        // 🔗 Relación N–1 con Cliente
        [Required]
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }

        // 🔗 Relación N–1 con Sesion
        [Required]
        public int SesionId { get; set; }
        public Sesion Sesion { get; set; }

        // Estado de la reserva (enum equivalente a tabla EstadoReserva)
        [Required]
        public TipoEstado EstadoReserva { get; set; } = TipoEstado.Reservada;

        // Fecha en la que se realiza la reserva
        [Required]
        public DateTime FechaReserva { get; set; } = DateTime.Now;

        // Posición en lista de espera (solo si EstadoReserva == EnEspera)
        public int? PosicionEspera { get; set; }
    }
}