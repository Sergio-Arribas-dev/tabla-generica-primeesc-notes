using Microsoft.EntityFrameworkCore;
using Demo.Infrastructure.Entities;

namespace Demo.Infrastructure;

public sealed class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Employee> Employees { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Username).IsRequired().HasMaxLength(100);
            entity.Property(e => e.EmploymentStatus).IsRequired().HasMaxLength(50);
            entity.Property(e => e.StatusColorHex).HasMaxLength(7).HasDefaultValue("#999999");
            entity.HasIndex(e => e.Username);
            entity.HasIndex(e => e.EmploymentStatus);
        });
    }
}
