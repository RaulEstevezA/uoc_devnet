using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GenteFit.model.enums;

namespace GenteFit.model.entity
{
    public class ReservarSala
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        // 🔗 Relación N–1 con Sala
        [Required]
        public int SalaId { get; set; }
        public Sala Sala { get; set; }

        // 🔗 Relación N–1 con Sesion
        [Required]
        public int SesionId { get; set; }
        public Sesion Sesion { get; set; }

        // Estado de la reserva de sala (puede compartir el enum de Reserva)
        [Required]
        public TipoEstado Estado { get; set; } = TipoEstado.Reservada;
    }
}