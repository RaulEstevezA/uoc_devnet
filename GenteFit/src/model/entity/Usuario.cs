using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GenteFit.model.enums;

namespace GenteFit.model.entity
{
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        [MaxLength(255)]
        public string PasswordHash { get; set; }

        [Required]
        public TipoRol TipoRol { get; set; } = TipoRol.Cliente;  // Valor por defecto

        [Required]
        public bool Activo { get; set; } = true;

        [Required]
        public DateTime CreadoEn { get; set; } = DateTime.Now;

        // Relación 1–1 opcional con Cliente
        public Cliente? Cliente { get; set; }

        // Relación 1–1 opcional con Monitor
        public Monitor? Monitor { get; set; }
    }
}