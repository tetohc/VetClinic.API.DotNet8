using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Proyecto2_JerryHurtado.API.Models.Entities;

namespace Proyecto2_JerryHurtado.API.Database.Configuration
{
    public class PetEntityConfiguration : IEntityTypeConfiguration<PetEntity>
    {
        public void Configure(EntityTypeBuilder<PetEntity> builder)
        {
            builder.ToTable("Pet");
            builder.HasKey(e => e.Id).HasName("PK__Pet__3214EC0703F35096");

            builder.Property(e => e.Id).ValueGeneratedNever();
            builder.Property(e => e.Color).HasMaxLength(50);
            builder.Property(e => e.Name).HasMaxLength(50);
            builder.Property(e => e.Race).HasMaxLength(50);

            builder.HasOne(d => d.Customer).WithMany(p => p.Pet)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK_Pet_Customer");
        }
    }
}