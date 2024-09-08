using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using VieroCodeTest.Infra.Data.Persistence.Models;

namespace VieroCodeTest.Infra.Data.Persistence;

public class VieroCodeContext : DbContext
{
    private readonly IConfiguration _configuration;

    public DbSet<Alumno> Alumnos { get; set; }
    public DbSet<Profesor> Profesores { get; set; }
    public DbSet<Grado> Grados { get; set; }
    public DbSet<AlumnoGrado> AlumnoGrados { get; set; }

    public VieroCodeContext()
    {
    }
    
    public VieroCodeContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost;Database=VieroCodeDB;Trusted_Connection=True;TrustServerCertificate=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuración de Alumno
        modelBuilder.Entity<Alumno>(entity =>
        {
            entity.Property(a => a.Nombre)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(a => a.Apellidos)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(a => a.Genero)
                .IsRequired()
                .HasMaxLength(10);

            entity.Property(a => a.FechaNacimiento);
        });

        // Configuración de Profesor
        modelBuilder.Entity<Profesor>(entity =>
        {
            entity.Property(p => p.Nombre)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(p => p.Apellidos)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(p => p.Genero)
                .IsRequired()
                .HasMaxLength(10);
        });

        // Configuración de Grado
        modelBuilder.Entity<Grado>(entity =>
        {
            entity.Property(g => g.Nombre)
                .IsRequired()
                .HasMaxLength(50);
            
            entity.HasOne(g => g.Profesor)
                .WithMany(p => p.Grados)
                .HasForeignKey(g => g.ProfesorId)
                .OnDelete(DeleteBehavior.Restrict); 
        });

        // Configuración de AlumnoGrado (Tabla Intermedia)
        modelBuilder.Entity<AlumnoGrado>(entity =>
        {
            entity.HasOne(ag => ag.Alumno)
                .WithMany(a => a.AlumnoGrados)
                .HasForeignKey(ag => ag.AlumnoId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(ag => ag.Grado)
                .WithMany(g => g.AlumnoGrados)
                .HasForeignKey(ag => ag.GradoId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.Property(ag => ag.Seccion)
                .IsRequired()
                .HasMaxLength(20);
        });
    }
}