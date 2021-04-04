using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SimpleInventoryAPI.DataAccess.MappingConfiguration
{
    public class ComponentEntityConfiguration : IEntityTypeConfiguration<Component>
    {
        public void Configure(EntityTypeBuilder<Component> builder)
        {
            builder.Property(t => t.PartNumber)
                    .IsRequired()
                    .HasMaxLength(50);
            builder.Property(t => t.PartDescription)
                    .IsRequired()
                    .HasMaxLength(150);
            builder.Property(t => t.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);
            builder.Property(t => t.ModifiedBy)
                    .HasMaxLength(50);
        }
    }
}
