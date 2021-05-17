using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SimpleInventoryAPI.DataAccess.MappingConfiguration
{
    public class StockOpnameEntityConfiguration : IEntityTypeConfiguration<StockOpname>
    {
        public void Configure(EntityTypeBuilder<StockOpname> builder)
        {
            builder.Property(t => t.Remarks)
                   .IsRequired()
                   .HasMaxLength(250);
            builder.Property(t => t.CreatedBy)
                   .IsRequired()
                   .HasMaxLength(50);
            builder.Property(t => t.ModifiedBy)
                    .HasMaxLength(50);

            /* relationship */
            builder.HasMany(r => r.Products)
                .WithOne()
                .HasForeignKey(k => k.HeaderId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(r => r.Components)
                .WithOne()
                .HasForeignKey(k => k.HeaderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
