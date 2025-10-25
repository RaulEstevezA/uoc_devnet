using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using GenteFit.src.model.entity;
using GenteFit.src.model.dao;

public class SalaXml
{
    public static List<Sala> LeerSalasDesdeXml(string ruta)
    {
        var doc = XDocument.Load(ruta);

        return doc.Root?
            .Elements("Sala")
            .Select(x => new Sala
            {
                Id = int.Parse(x.Element("Id")?.Value ?? "0"),
                Nombre = x.Element("Nombre")?.Value ?? "",
                AforoMax = int.Parse(x.Element("AforoMax")?.Value ?? "0"),
                Disponible = bool.Parse(x.Element("Disponible")?.Value ?? "true")
            })
            .ToList() ?? new List<Sala>();
    }

    public static void ImportarSalasDesdeXml(string ruta, GenteFitDbContext context)
    {
        var salas = LeerSalasDesdeXml(ruta);

        // Evitar duplicados si lo deseas (ejemplo por Nombre)
        foreach (var sala in salas)
        {
            if (!context.Salas.Any(s => s.Id == sala.Id))
            {
                context.Salas.Add(sala);
            }
        }

        context.SaveChanges();
    }
}

