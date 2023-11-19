using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SubscriptionManagement.Domain;
using SubscriptionManagement.Domain.UserAggregate;

namespace SubscriptionManagement.Infrastructure.EntityConfiguration;

class SubscriptionEntityTypeConfiguration : IEntityTypeConfiguration<Subscription>
{
    public void Configure(EntityTypeBuilder<Subscription> builder)
    {
        builder.ToTable("Subscriptions");
        builder.HasKey(x => x.Id);
        builder.Property(s => s.SubscriptionType)
            .HasConversion(new EnumToStringConverter<SubscriptionType>());
        builder.Ignore(subscription => subscription.IsActive);
        // One-to-one relationship between User and Subscription with shadow foreign key
        builder.HasOne<User>()
            .WithOne(user => user.Subscription)
            .HasForeignKey<Subscription>("UserId").IsRequired();
    }
}