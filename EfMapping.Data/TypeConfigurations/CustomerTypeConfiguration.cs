using EFMapping.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EfMapping.Data.TypeConfigurations
{
    public class CustomerTypeConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(prop => prop.Id);

            builder.Property(prop => prop.Name)
                   .HasMaxLength(80)
                   .IsRequired();

            builder.Property(prop => prop.Email)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(prop => prop.NationalId)
                   .HasMaxLength(11);

            builder.Property(prop => prop.Birthday)
                   .HasColumnType("Date");

            builder.HasIndex(prop => prop.Email)
                   .IsUnique();
        }
    }
}
