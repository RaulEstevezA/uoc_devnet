using System;
using GenteFit.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var config = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .Build();

var services = new ServiceCollection();

services.AddDbContext<GenteFitDbContext>(opts =>
    opts.UseSqlServer(
        config.GetConnectionString("GenteFitDb")
        ?? "Server=localhost,1433;Database=GenteFit;User Id=sa;Password=Passw0rd!;TrustServerCertificate=False;"
    )
);

using var provider = services.BuildServiceProvider();

using (var scope = provider.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<GenteFitDbContext>();
    Console.WriteLine("GenteFit.Demo listo. EF Core configurado.");

    try
    {
        // Comprueba si EF Core puede establecer conexión con la base de datos
        // Abre conexión explícita y ejecuta una consulta simple
        db.Database.OpenConnection();
        var conn = db.Database.GetDbConnection();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = "SELECT 1";
        var scalar = cmd.ExecuteScalar();
        Console.WriteLine(scalar != null ? $"Prueba SQL: {scalar}" : "Prueba SQL: sin resultado");
        db.Database.CloseConnection();
        Console.WriteLine("Conexión a la BBDD: OK");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error al conectar a la BBDD: {ex.GetType().Name}: {ex.Message}");
        if (ex.InnerException != null)
            Console.WriteLine($"InnerException: {ex.InnerException.Message}");
    }
}