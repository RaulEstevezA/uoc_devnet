using System;
using Microsoft.Data.SqlClient;

namespace GenteFit.src.util
{
    public static class TestConexion
    {
        public static void ProbarConexion()
        {
            Console.WriteLine("Probando conexión con la base de datos...");

            string connectionString = "Server=ROG;Database=GenteFit;User Id=sa;Password=Passw0rd!;Encrypt=True;TrustServerCertificate=True";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    Console.WriteLine("✅ Conexión exitosa con la base de datos.");
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("❌Error al conectar con la base de datos:");
                Console.WriteLine($"Mensaje: {ex.Message}");
                Console.WriteLine($"Código de error: {ex.Number}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Error inesperado:");
                Console.WriteLine(ex.Message);
            }
        }
    }
}



