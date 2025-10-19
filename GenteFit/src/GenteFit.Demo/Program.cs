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
        ?? "Server=localhost,1433;Database=GenteFit;User Id=sa;Password=Passw0rd!;TrustServerCertificate=True;"
    )
);

using var provider = services.BuildServiceProvider();

using (var scope = provider.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<GenteFitDbContext>();
    Console.WriteLine("GenteFit.Demo listo. EF Core configurado.");
}