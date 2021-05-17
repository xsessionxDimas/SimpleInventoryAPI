using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SimpleInventoryAPI.DataAccess.MappingConfiguration
{
    public class StockOpnameComponentEntityConfiguration : IEntityTypeConfiguration<StockOpnameComponent>
    {
        public void Configure(EntityTypeBuilder<StockOpnameComponent> builder)
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
