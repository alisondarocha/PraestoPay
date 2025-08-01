using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PraestoPay.Domain.AggregatesModel.UserAggregate;

namespace PraestoPay.Infrastructure.EntityConfigurations;

public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .ToTable("Users");

        builder
            .HasKey(p => p.Id);

        builder
            .Property(p => p.Id)
            .ValueGeneratedOnAdd();

        builder
           .Property(p => p.Name)
           .IsRequired()
           .HasMaxLength(50);

        builder
            .Property(p => p.LastName)
            .IsRequired()
            .HasMaxLength(80);

        builder
            .Property(p => p.Email)
            .IsRequired()
            .HasMaxLength(100);

        builder
            .Property(p => p.CreatedAt)
            .IsRequired();
    }
}
