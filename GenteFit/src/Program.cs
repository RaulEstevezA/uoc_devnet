using GenteFit.src.model.GestionModelo;
using GenteFit.src.model.entity;
using GenteFit.src.DAO;
using GenteFit.src.util;

namespace GenteFit.src
{
    class Program
    {
        static void Main()
        {
            bool salir = false;

            while (!salir)
            {
                Console.Clear();
                Console.WriteLine("==== MENÚ PRINCIPAL ====");
                Console.WriteLine("1. Ver todas las salas");
                Console.WriteLine("2. Agregar nueva sala");
                Console.WriteLine("3. Actualizar sala");
                Console.WriteLine("4. Eliminar sala");
                Console.WriteLine("5. Leer salas desde archivo XML");
                Console.WriteLine("6. Guardar salas en archivo XML");
                Console.WriteLine("7. Probar conexión a la BBDD");
                Console.WriteLine("0. Salir");
                Console.Write("Selecciona una opción: ");

                string? opcion = Console.ReadLine();
                Console.Clear();

                switch (opcion)
                {
                    case "0":
                        salir = true;
                        break;

                    case "1":
                        var salas = GestionSala.ObtenerSalas();
                        Console.WriteLine("Salas en BBDD:");
                        foreach (var sala in salas)
                            Console.WriteLine(sala);
                        break;

                    case "2":
                        var nuevaSala = new Sala
                        {
                            Nombre = "Sala de prueba",
                            AforoMax = 15,
                            Disponible = true
                        };
                        GestionSala.AgregarSala(nuevaSala);
                        Console.WriteLine("Sala añadida correctamente.");
                        break;

                    case "3":
                        Console.Write("Introduce ID de la sala a actualizar: ");
                        if (int.TryParse(Console.ReadLine(), out int idActualizar))
                        {
                            var salaActualizar = GestionSala.ObtenerSalaPorId(idActualizar);
                            if (salaActualizar != null)
                            {
                                salaActualizar.AforoMax += 5;  // Simple ejemplo
                                GestionSala.ActualizarSala(salaActualizar);
                                Console.WriteLine("Sala actualizada.");
                            }
                            else
                            {
                                Console.WriteLine("Sala no encontrada.");
                            }
                        }
                        break;

                    case "4":
                        Console.Write("Introduce ID de la sala a eliminar: ");
                        if (int.TryParse(Console.ReadLine(), out int idEliminar))
                        {
                            var salaEliminar = GestionSala.ObtenerSalaPorId(idEliminar);
                            if (salaEliminar != null)
                            {
                                GestionSala.EliminarSala(salaEliminar);
                                Console.WriteLine("Sala eliminada.");
                            }
                            else
                            {
                                Console.WriteLine("Sala no encontrada.");
                            }
                        }
                        break;

                    case "5":
                        try
                        {
                            var salasDesdeXml = SalaXML.LeerSalasDesdeXml("xml_data/salas.xml");

                            if (salasDesdeXml.Count == 0)
                            {
                                Console.WriteLine("No se encontraron salas en el archivo XML.");
                            }
                            else
                            {
                                Console.WriteLine("Salas leídas desde XML:");
                                foreach (var sala in salasDesdeXml)
                                    Console.WriteLine(sala);

                                Console.Write("\n¿Deseas importar estas salas a la base de datos? (s/n): ");
                                var respuesta = Console.ReadLine();
                                if (respuesta?.Trim().ToLower() == "s")
                                {
                                    SalaXML.ImportarSalasDesdeXml("xml_data/salas.xml");
                                    Console.WriteLine("Salas importadas correctamente (se han ignorado las duplicadas).");
                                }
                                else
                                {
                                    Console.WriteLine("Operación cancelada.");
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error al acceder al archivo XML: {ex.Message}");
                        }
                        break;

                    case "6":
                        var salasActuales = GestionSala.ObtenerSalas().ToList();
                        if (salasActuales.Count == 0)
                        {
                            Console.WriteLine("No hay salas para exportar.");
                        }
                        else
                        {
                            Directory.CreateDirectory("xml_data");
                            SalaXML.GuardarSalasEnXml(salasActuales, "xml_data/salas_exportadas.xml");
                            Console.WriteLine("Salas exportadas correctamente a xml_data/salas_exportadas.xml");
                        }
                        break;


                    case "7":
                        Console.WriteLine("Probando conexión con la base de datos...");
                        TestConexion.ProbarConexion();
                        break;

                }

                if (!salir)
                {
                    Console.WriteLine("\nPresiona cualquier tecla para continuar...");
                    Console.ReadKey();
                }
            }
        }
    }
}
