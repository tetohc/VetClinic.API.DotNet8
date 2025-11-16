using Microsoft.EntityFrameworkCore;
using Proyecto2_JerryHurtado.API.Models.Entities;
using System.Reflection;

namespace Proyecto2_JerryHurtado.API.Database;

public partial class DatabaseContext : DbContext
{
    public DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CantonEntity> Canton { get; set; }
    public virtual DbSet<CustomerEntity> Customer { get; set; }
    public virtual DbSet<DistrictEntity> District { get; set; }
    public virtual DbSet<EmployeeEntity> Employee { get; set; }
    public virtual DbSet<PetEntity> Pet { get; set; }
    public virtual DbSet<PetProcedureEntity> PetProcedure { get; set; }
    public virtual DbSet<ProvinceEntity> Province { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}