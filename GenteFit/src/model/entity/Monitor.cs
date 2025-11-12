using System.Collections.Generic;
namespace GenteFit.src.model.entity;


public class Monitor
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Apellido1 { get; set; }
    public string? Apellido2 { get; set; }
    public string? Email { get; set; }

    // relaciones logicas 
    public Usuario? Usuario { get; set; }


    public override string ToString()
    {
        return $"{Id}: {Nombre} {Apellido1} {(string.IsNullOrWhiteSpace(Apellido2) ? "" : Apellido2)}";
    }
}
