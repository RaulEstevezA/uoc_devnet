using GenteFit.Domain;
using Microsoft.EntityFrameworkCore;
using DomainMonitor = GenteFit.Domain.Monitor;

namespace GenteFit.Infrastructure;

public class GenteFitDbContext : DbContext
{
    public GenteFitDbContext(DbContextOptions<GenteFitDbContext> options) : base(options) { }

    public DbSet<Cliente> Clientes => Set<Cliente>();
    public DbSet<DomainMonitor> Monitores => Set<DomainMonitor>();  // <-- alias aquí
    public DbSet<Sala> Salas => Set<Sala>();
    public DbSet<Actividad> Actividades => Set<Actividad>();
    public DbSet<Sesion> Sesiones => Set<Sesion>();
    public DbSet<Reserva> Reservas => Set<Reserva>();
    public DbSet<ListaEspera> ListasEspera => Set<ListaEspera>();

    protected override void OnModelCreating(ModelBuilder b)
    {
        b.Entity<Sala>().HasIndex(x => x.Nombre).IsUnique();

        b.Entity<Actividad>()
            .HasIndex(a => a.Nombre).IsUnique();

        b.Entity<Actividad>()
            .HasOne(a => a.Sala)
            .WithMany(s => s.Actividades)
            .HasForeignKey(a => a.SalaId)
            .OnDelete(DeleteBehavior.Restrict);

        b.Entity<Sesion>()
            .HasOne(s => s.Actividad).WithMany(a => a.Sesiones)
            .HasForeignKey(s => s.ActividadId);

        b.Entity<Sesion>()
            .HasOne(s => s.Monitor).WithMany(m => m.Sesiones)
            .HasForeignKey(s => s.MonitorId)
            .OnDelete(DeleteBehavior.Restrict);

        b.Entity<Sesion>()
            .HasIndex(s => new { s.ActividadId, s.Inicio }).IsUnique();

        b.Entity<Reserva>()
            .HasIndex(r => new { r.SesionId, r.ClienteId }).IsUnique();

        b.Entity<ListaEspera>()
            .HasIndex(e => new { e.SesionId, e.Posicion });

        b.Entity<ListaEspera>()
            .HasIndex(e => new { e.SesionId, e.ClienteId }).IsUnique();

        // Si tenías esta línea, usa el alias:
        // b.Entity<Monitor>();
        b.Entity<DomainMonitor>();
    }
}