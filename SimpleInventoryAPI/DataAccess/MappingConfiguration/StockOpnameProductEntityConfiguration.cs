using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SimpleInventoryAPI.DataAccess.MappingConfiguration
{
    public class StockOpnameProductEntityConfiguration : IEntityTypeConfiguration<StockOpnameProduct>
    {
        public void Configure(EntityTypeBuilder<StockOpnameProduct> builder)
        {
            builder.Property(t => t.Remarks)
                   .IsRequired()
                   .HasMaxLength(250);
            builder.Property(t => t.CreatedBy)
                   .IsRequired()
                   .HasMaxLength(50);
            builder.Property(t => t.ModifiedBy)
                    .HasMaxLength(50);
        }
    }
}
