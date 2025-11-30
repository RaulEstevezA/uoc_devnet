using System;
using GenteFit.src.model.enums;
namespace GenteFit.src.model.entity;

public class Usuario
{
    public int Id { get; set; }
    public string Username { get; set; } = "";
    public string Email { get; set; } = "";
    public string PasswordHash { get; set; } = "";
    public int TipoRolId { get; set; }
    public bool Activo { get; set; } = true;
    public DateTime CreadoEn { get; set; } = DateTime.Now;

    // relaciones
    public Cliente? Cliente { get; set; }
    public Instructor? Instructor { get; set; }

    public override string ToString()
    {
        return $"{Id}: {Username}, rol {TipoRolId}, activo {Activo}";
    }
}
