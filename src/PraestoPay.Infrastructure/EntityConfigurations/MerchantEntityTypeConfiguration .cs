using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PraestoPay.Domain.AggregatesModel.MerchantAggregate;

namespace PraestoPay.Infrastructure.EntityConfigurations;

public class MerchantEntityTypeConfiguration : IEntityTypeConfiguration<Merchant>
{
    public void Configure(EntityTypeBuilder<Merchant> builder)
    {
        builder
            .ToTable("merchants");

        builder.HasKey(m => m.Id);

        builder.Property(m => m.Id)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(m => m.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(m => m.Email)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(m => m.MerchantCode)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(m => m.CreatedAt)
            .IsRequired();

        builder.Property(m => m.UpdatedAt);

        builder.Property(m => m.IsDeleted)
            .IsRequired()
            .HasDefaultValue(false);

        builder.HasIndex(m => m.Email)
            .IsUnique();

        builder.HasIndex(m => m.MerchantCode)
            .IsUnique();

        builder.HasIndex(m => m.IsDeleted);

        builder.HasIndex(m => m.CreatedAt);
    }
}