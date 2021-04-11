using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SimpleInventoryAPI.DataAccess.MappingConfiguration
{
    public class PurchaseOrderItemEntityConfiguration : IEntityTypeConfiguration<PurchaseOrderItem>
    {
        public void Configure(EntityTypeBuilder<PurchaseOrderItem> builder)
        {
            builder.Property(t => t.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);
            builder.Property(t => t.ModifiedBy)
                    .HasMaxLength(50);

            /* relationship */
            builder.HasOne(r => r.Component)
                .WithMany()
                .HasForeignKey(k => k.ComponentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
