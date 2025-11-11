using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenteFit.model.entity
{
    public class Monitor
    {
        [Key, ForeignKey("Usuario")]   // Mismo ID que Usuario
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; }

        [Required]
        [MaxLength(50)]
        public string Apellido1 { get; set; }

        [MaxLength(50)]
        public string? Apellido2 { get; set; }

        [MaxLength(50)]
        public string? Email { get; set; }  // a la espera de decision conjunta

        // 🔗 Relación 1–1 con Usuario
        public Usuario Usuario { get; set; }

        // 🔗 Relación 1–N con Sesion
        public ICollection<Sesion>? Sesiones { get; set; }
    }
}