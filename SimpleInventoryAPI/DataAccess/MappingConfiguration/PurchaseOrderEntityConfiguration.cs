using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SimpleInventoryAPI.DataAccess.MappingConfiguration
{
    public class PurchaseOrderEntityConfiguration : IEntityTypeConfiguration<PurchaseOrder>
    {
        public void Configure(EntityTypeBuilder<PurchaseOrder> builder)
        {
            builder.Property(t => t.PurchaseOrderNumber)
                    .IsRequired()
                    .HasMaxLength(50);
            builder.Property(t => t.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);
            builder.Property(t => t.ModifiedBy)
                    .HasMaxLength(50);

            /* relationship */
            builder.HasMany(r => r.Items)
                .WithOne()
                .HasForeignKey(k => k.PurchaseOrderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
