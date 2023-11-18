using Microsoft.EntityFrameworkCore;
using SubscriptionManagement.Domain.UserAggregate;

namespace SubscriptionManagement.Infrastructure.EntityConfiguration;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Subscription> Subscriptions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Using Fluent Api to keep the domain entities clean (POCO)
        modelBuilder.ApplyConfiguration(new SubscriptionEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
    }
}