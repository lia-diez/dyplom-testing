using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<ApiKey> ApiKeys { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ApiKey>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.KeyValue).IsRequired().HasMaxLength(255);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.HasIndex(e => e.KeyValue).IsUnique();
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            
            // Seed data
            entity.HasData(
                new ApiKey { Id = 1, KeyValue = "1337", Name = "Development Key", CreatedAt = DateTime.UtcNow },
                new ApiKey { Id = 2, KeyValue = "prod-key-12345", Name = "Production Key", CreatedAt = DateTime.UtcNow },
                new ApiKey { Id = 3, KeyValue = "test-key-67890", Name = "Test Key", CreatedAt = DateTime.UtcNow }
            );
        });
    }
}