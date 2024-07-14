using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace Infrastructure.Data.Configurations;

public class ItemConfiguration : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> builder)
    {
        builder.Property(t => t.Name)
            .HasMaxLength(50)
            .IsRequired();

        //builder.Property(t => t.Category)
        //    .IsRequired();

        //builder.Property(t => t.Price)
        //    .IsRequired();

        builder
         .OwnsOne(b => b.Image);

        builder
         .OwnsOne(b => b.Price);
    }
}
