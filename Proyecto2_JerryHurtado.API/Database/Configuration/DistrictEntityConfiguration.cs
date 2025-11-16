using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Proyecto2_JerryHurtado.API.Models.Entities;

namespace Proyecto2_JerryHurtado.API.Database.Configuration
{
    public class DistrictEntityConfiguration : IEntityTypeConfiguration<DistrictEntity>
    {
        public void Configure(EntityTypeBuilder<DistrictEntity> builder)
        {
            builder.ToTable("District");
            builder.HasKey(e => e.Id).HasName("PK__District__3214EC07C21EFC21");

            builder.Property(e => e.Id).ValueGeneratedNever();
            builder.Property(e => e.Name).HasMaxLength(50);

            builder.HasOne(d => d.Canton).WithMany(p => p.District)
                .HasForeignKey(d => d.CantonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_District_Canton");
        }
    }
}