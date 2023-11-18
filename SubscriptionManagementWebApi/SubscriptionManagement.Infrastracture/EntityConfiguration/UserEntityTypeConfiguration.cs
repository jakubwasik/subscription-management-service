using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SubscriptionManagement.Domain.Entities;

namespace SubscriptionManagement.Infrastructure.EntityConfiguration;

class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(x => x.Id);
        // One - to - one relationship between User and Subscription with shadow foreign key
        builder.HasOne<Subscription>()
            .WithOne()
            .HasForeignKey<Subscription>(subscription => subscription.Id)
            .IsRequired(false);
    }
}