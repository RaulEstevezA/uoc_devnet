
/* namespace GenteFit
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }
    }
}
*/


using GenteFit.Database;

namespace GenteFit.src
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Prueba de conexión a SQL Server");

            // Cada uno debería configurar su propia cadena de conexión en su entorno
            var envConn = Environment.GetEnvironmentVariable("SQL_CONN");
            var connectionString = !string.IsNullOrWhiteSpace(envConn)
                ? envConn
                : "Server=ROG;Database=GenteFit;User Id=sa;Password=Passw0rd!;Encrypt=True;TrustServerCertificate=True"; // Cambia según el entorno

            Console.WriteLine(envConn != null
                ? "Usando cadena de conexión desde variable de entorno SQL_CONN"
                : $"Usando cadena de conexión por defecto: {connectionString}");

            var ok = await SqlServerHelper.TestConnectionAsync(connectionString);

            Console.WriteLine(ok ? "Conexión correcta." : "Fallo en la conexión.");
        }
    }
}