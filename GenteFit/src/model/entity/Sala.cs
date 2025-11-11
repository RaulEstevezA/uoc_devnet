using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenteFit.model.entity
{
    public class Sala
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }

        [Required]
        public int AforoMax { get; set; }

        [Required]
        public bool Disponible { get; set; }

        // Relación 1–N con ReservarSala
        public ICollection<ReservarSala>? ReservasSala { get; set; }
    }
}