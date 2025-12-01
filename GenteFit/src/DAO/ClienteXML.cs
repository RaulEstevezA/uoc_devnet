using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using GenteFit.src.model.entity;
using GenteFit.src.DAO;

namespace GenteFit.src.DAO
{
    public class ClienteXML
    {
        // Lee los clientes desde un archivo XML y devuelve la lista
        public static List<Cliente> LeerClientesDesdeXml(string ruta)
        {
            var doc = XDocument.Load(ruta);

            return doc.Root?
                .Elements("Cliente")
                .Select(x => new Cliente
                {
                    Id = int.Parse(x.Element("Id")?.Value ?? "0"),
                    Dni = x.Element("Dni")?.Value ?? "",
                    Nombre = x.Element("Nombre")?.Value ?? "",
                    Apellido1 = x.Element("Apellido1")?.Value ?? "",
                    Apellido2 = x.Element("Apellido2")?.Value,
                    Email = x.Element("Email")?.Value
                })
                .ToList() ?? new List<Cliente>();
        }

        // Importa los clientes a la base de datos usando ClienteDAO
        public static void ImportarClientesDesdeXml(string ruta)
        {
            var clientes = LeerClientesDesdeXml(ruta);
            var dao = new ClienteDAO();

            foreach (var cliente in clientes)
            {
                // Comprobar si ya existe un cliente con el mismo DNI
                bool existe = dao.GetAll()
                    .Any(c => (c.Dni ?? "").Equals(cliente.Dni ?? "", StringComparison.OrdinalIgnoreCase));

                if (!existe)
                {
                    dao.Save(cliente);
                }
            }
        }

        // Exporta los clientes a un archivo XML
        public static void GuardarClientesEnXml(List<Cliente> clientes, string rutaArchivo)
        {
            XElement root = new XElement("Clientes",
                clientes.Select(c => new XElement("Cliente",
                    new XElement("Id", c.Id),
                    new XElement("Dni", c.Dni),
                    new XElement("Nombre", c.Nombre),
                    new XElement("Apellido1", c.Apellido1),
                    new XElement("Apellido2", c.Apellido2 ?? ""),
                    new XElement("Email", c.Email ?? "")
                ))
            );

            root.Save(rutaArchivo);
            Console.WriteLine($"Se han guardado {clientes.Count} clientes en el archivo {rutaArchivo}");
        }
    }
}