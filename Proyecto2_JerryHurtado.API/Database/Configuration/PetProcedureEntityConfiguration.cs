using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Proyecto2_JerryHurtado.API.Models.Entities;

namespace Proyecto2_JerryHurtado.API.Database.Configuration
{
    public class PetProcedureEntityConfiguration : IEntityTypeConfiguration<PetProcedureEntity>
    {
        public void Configure(EntityTypeBuilder<PetProcedureEntity> builder)
        {
            builder.ToTable("PetProcedure");
            builder.HasKey(e => e.Id).HasName("PK__PetProce__3214EC07888D8E3F");

            builder.Property(e => e.Id).ValueGeneratedNever();

            builder.HasOne(d => d.Customer).WithMany(p => p.PetProcedure)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PetProcedure_Customer");

            builder.HasOne(d => d.Pet).WithMany(p => p.PetProcedure)
                .HasForeignKey(d => d.PetId)
                .HasConstraintName("FK_PetProcedure_Pet");
        }
    }
}