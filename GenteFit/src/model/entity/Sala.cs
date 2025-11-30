using System.Collections.Generic;
namespace GenteFit.src.model.entity;

public class Sala
{
    public int Id { get; set; }
    public string Nombre { get; set; } = "";
    public int AforoMax { get; set; }
    public bool Disponible { get; set; } = true;


public override string ToString()
    {
        return $"{Id}: {Nombre}, aforo {AforoMax}, disponible {Disponible}";
    }
}
