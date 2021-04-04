using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SimpleInventoryAPI.DataAccess.MappingConfiguration
{
    public class ProductEntityConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(t => t.ProductName)
                    .IsRequired()
                    .HasMaxLength(50);
            builder.Property(t => t.Description)
                    .IsRequired()
                    .HasMaxLength(150);
            builder.Property(t => t.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);
            builder.Property(t => t.ModifiedBy)
                    .HasMaxLength(50);

            /* relationship */
            builder.HasMany(r => r.ProductComponents)
                .WithOne(r => r.Product)
                .HasForeignKey(k => k.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(r => r.Batches)
                .WithOne(r => r.Product)
                .HasForeignKey(k => k.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
