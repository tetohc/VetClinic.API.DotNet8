using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Proyecto2_JerryHurtado.API.Models.Entities;

namespace Proyecto2_JerryHurtado.API.Database.Configuration
{
    public class CantonEntityConfiguration : IEntityTypeConfiguration<CantonEntity>
    {
        public void Configure(EntityTypeBuilder<CantonEntity> builder)
        {
            builder.ToTable("Canton");
            builder.HasKey(e => e.Id).HasName("PK__Canton__3214EC07AF8D38A2");

            builder.Property(e => e.Id).ValueGeneratedNever();
            builder.Property(e => e.Name).HasMaxLength(50);

            builder.HasOne(d => d.Province).WithMany(p => p.Canton)
                .HasForeignKey(d => d.ProvinceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Canton_Province");
        }
    }
}