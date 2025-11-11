using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenteFit.model.entity
{
    public class Actividad
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "La duración debe ser mayor que 0")]
        public int DuracionMin { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "El número de plazas debe ser mayor que 0")]
        public int PlazasMax { get; set; } = 16; // valor por defecto en SQL

        // 🔗 Relación 1–N con Sesion
        public ICollection<Sesion>? Sesiones { get; set; }
    }
}