using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SimpleInventoryAPI.DataAccess.MappingConfiguration
{
    public class ProductBatchEntityConfiguration : IEntityTypeConfiguration<ProductBatch>
    {
        public void Configure(EntityTypeBuilder<ProductBatch> builder)
        {
            builder.Property(t => t.BatchNo)
                    .IsRequired()
                    .HasMaxLength(50);
            builder.Property(t => t.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);
            builder.Property(t => t.ModifiedBy)
                    .HasMaxLength(50);
        }
    }
}
