using System;
using GenteFit.src.model.enums;
namespace GenteFit.src.model.entity;

public class Usuario
{
    public int Id { get; set; }
    public string Username { get; set; } = "";
    public string Email { get; set; } = "";
    public string PasswordHash { get; set; } = "";
    public string TipoRol { get; set; } = "CLIENTE";
    public bool Activo { get; set; } = true;
    public DateTime CreadoEn { get; set; } = DateTime.Now;

    // relaciones
    public Cliente? Cliente { get; set; }
    public Monitor? Monitor { get; set; }

    public override string ToString()
    {
        return $"{Id}: {Username}, rol {TipoRol}, activo {Activo}";
    }
}
