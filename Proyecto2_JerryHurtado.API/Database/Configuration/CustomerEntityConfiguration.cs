using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Proyecto2_JerryHurtado.API.Models.Entities;

namespace Proyecto2_JerryHurtado.API.Database.Configuration
{
    public class CustomerEntityConfiguration : IEntityTypeConfiguration<CustomerEntity>
    {
        public void Configure(EntityTypeBuilder<CustomerEntity> builder)
        {
            builder.ToTable("Customer");
            builder.HasKey(e => e.Id).HasName("PK__Customer__3214EC074035C606");

            builder.Property(e => e.Id).ValueGeneratedNever();
            builder.Property(e => e.Address).HasMaxLength(100);
            builder.Property(e => e.Email).HasMaxLength(100);
            builder.Property(e => e.Name).HasMaxLength(100);
            builder.Property(e => e.PersonalIdNumber).HasMaxLength(50);
            builder.Property(e => e.PhoneNumber).HasMaxLength(20);

            builder.HasOne(d => d.Canton).WithMany(p => p.Customer)
                .HasForeignKey(d => d.CantonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Customer_Canton");

            builder.HasOne(d => d.District).WithMany(p => p.Customer)
                .HasForeignKey(d => d.DistrictId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Customer_District");

            builder.HasOne(d => d.Province).WithMany(p => p.Customer)
                .HasForeignKey(d => d.ProvinceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Customer_Province");
        }
    }
}