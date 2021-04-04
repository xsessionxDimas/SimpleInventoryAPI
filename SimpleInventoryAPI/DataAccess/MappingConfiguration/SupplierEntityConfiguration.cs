using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace SimpleInventoryAPI.DataAccess.MappingConfiguration
{
    public class SupplierEntityConfiguration : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.Property(t => t.SupplierName)
                    .IsRequired()
                    .HasMaxLength(50);
            builder.Property(t => t.Address)
                    .IsRequired()
                    .HasMaxLength(150);
            builder.Property(t => t.Phone)
                    .IsRequired()
                    .HasMaxLength(15);
            builder.Property(t => t.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);
            builder.Property(t => t.ModifiedBy)
                    .HasMaxLength(50);
        }
    }
}
