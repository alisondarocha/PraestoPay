using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PraestoPay.Domain.AggregatesModel.TransactionAggregate;

namespace PraestoPay.Infrastructure.EntityConfigurations;

public class TransactionEntityTypeConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable("transactions");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(t => t.Amount)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(t => t.Currency)
            .IsRequired()
            .HasMaxLength(3);

        builder.Property(t => t.PaymentMethod)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(t => t.Status)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(t => t.CreatedAt)
            .IsRequired();

        builder.Property(t => t.ProcessedAt);

        builder.Property(t => t.RefundedAt);

        builder.Property(t => t.UpdatedAt);

        builder.Property(t => t.IsDeleted)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(t => t.UserId)
            .IsRequired();

        builder.Property(t => t.MerchantId)
            .IsRequired();

        builder.HasOne(t => t.User)
            .WithMany()
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_Transactions_Users");

        builder.HasOne(t => t.Merchant)
            .WithMany()
            .HasForeignKey(t => t.MerchantId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_Transactions_Merchants");
    }
}