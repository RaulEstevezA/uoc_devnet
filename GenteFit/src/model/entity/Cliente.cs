using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenteFit.model.entity
{
    public class Cliente
    {
        [Key, ForeignKey("Usuario")]   // El ID es también la FK a Usuario
        public int Id { get; set; }

        [Required]
        [MaxLength(9)]
        public string Dni { get; set; }

        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; }

        [Required]
        [MaxLength(50)]
        public string Apellido1 { get; set; }

        [MaxLength(50)]
        public string? Apellido2 { get; set; }

        [MaxLength(50)]
        public string? Email { get; set; } // a la espera de decision conjunta

        // 🔗 Relación 1–1 con Usuario
        public Usuario Usuario { get; set; }

        // 🔗 Relación 1–N con Reserva
        public ICollection<Reserva>? Reservas { get; set; }
    }
}