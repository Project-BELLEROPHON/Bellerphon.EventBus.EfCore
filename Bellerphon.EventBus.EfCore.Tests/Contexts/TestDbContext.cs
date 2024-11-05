using Bellerphon.EventBus.EfCore.Tests.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bellerphon.EventBus.EfCore.Tests.Contexts;

public class TestDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public TestDbContext(DbContextOptions<TestDbContext> options) : base(options) { }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(e =>
        {
            e.HasKey(u => u.Id);
            e.Property(u => u.Email).IsRequired();
            e.Property(u => u.Name).IsRequired();
        });
    }
}