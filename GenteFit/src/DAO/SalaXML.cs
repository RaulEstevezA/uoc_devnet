using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using GenteFit.src.model.entity;
using GenteFit.src.DAO;

namespace GenteFit.src.DAO
{
    public class SalaXML
    {
        // Lee las salas desde un archivo XML y devuelve la lista
        public static List<Sala> LeerSalasDesdeXml(string ruta)
        {
            var doc = XDocument.Load(ruta);

            return doc.Root?
                .Elements("Sala")
                .Select(x => new Sala
                {
                    Nombre = x.Element("Nombre")?.Value ?? "",
                    AforoMax = int.Parse(x.Element("AforoMax")?.Value ?? "0"),
                    Disponible = bool.Parse(x.Element("Disponible")?.Value ?? "true")
                })
                .ToList() ?? new List<Sala>();
        }

        // Importa las salas a la base de datos usando SalaDAO
        public static void ImportarSalasDesdeXml(string ruta)
        {
            var salas = LeerSalasDesdeXml(ruta);
            var dao = new SalaDAO();

            foreach (var sala in salas)
            {
                // Comprobar si ya existe una sala con el mismo nombre (ignora mayúsculas/minúsculas)
                bool existe = dao.GetAll().Any(s => s.Nombre.Equals(sala.Nombre, StringComparison.OrdinalIgnoreCase));

                if (!existe)
                {
                    dao.Save(sala);
                }
            }
        }

    }
}
