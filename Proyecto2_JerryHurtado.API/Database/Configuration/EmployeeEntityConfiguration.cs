using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Proyecto2_JerryHurtado.API.Models.Entities;

namespace Proyecto2_JerryHurtado.API.Database.Configuration
{
    public class EmployeeEntityConfiguration : IEntityTypeConfiguration<EmployeeEntity>
    {
        public void Configure(EntityTypeBuilder<EmployeeEntity> builder)
        {
            builder.ToTable("Employee");
            builder.HasKey(e => e.Id).HasName("PK__Employee__3214EC072BAD22DA");

            builder.Property(e => e.Id).ValueGeneratedNever();
            builder.Property(e => e.DailySalary).HasColumnType("decimal(18, 2)");
            builder.Property(e => e.PersonalIdNumber).HasMaxLength(50);
        }
    }
}
